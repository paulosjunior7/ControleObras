using System;
using System.Collections.Generic;
using System.Web;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace ControleObras.Persistencia
{
    class BancodeDados
    {
        private OracleConnection GetConnection()
        {
            string conexao = "";
          //  DataTable dtConf = new DataTable();
          //  string connection = "Password= local;Persist Security Info=True;User ID=local;Data Source=local" + server
             // conexao = Descriptografar(connection, true);
            //conexao = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            return new OracleConnection("Password= local;Persist Security Info=True;User ID=local;Data Source=xe");
        }

        public DataTable ExecutaComando(string comando)
        {
            OracleConnection cn = new OracleConnection();
            OracleCommand dbCommand = cn.CreateCommand();
            DataTable oDt = new DataTable();

            cn = GetConnection();
            cn.Open();

            dbCommand.Connection = cn;

            dbCommand.CommandText = "ALTER SESSION SET NLS_NUMERIC_CHARACTERS=',.'";
            dbCommand.CommandType = CommandType.Text;

            dbCommand.ExecuteNonQuery();

            dbCommand.CommandText = comando;
            dbCommand.CommandType = CommandType.Text;

            //Criar o tratamento para 
            try
            {


                //Criar um objeto Oracle Data Adapter
                OracleDataAdapter oDa = new OracleDataAdapter(dbCommand);

                //Preenchendo o DataTable
                oDa.Fill(oDt);

                //Resultado da Função
                return oDt;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();

            }

        }

        public void ExecutaComandoNonQuery(string comando)
        {
            OracleConnection cn = new OracleConnection();
            OracleCommand dbCommand = cn.CreateCommand();
            DataTable oDt = new DataTable();

            cn = GetConnection();
            cn.Open();

            dbCommand.Connection = cn;

            dbCommand.CommandText = "ALTER SESSION SET NLS_NUMERIC_CHARACTERS=',.'";
            dbCommand.CommandType = CommandType.Text;

            dbCommand.ExecuteNonQuery();

            dbCommand.CommandText = comando;
            dbCommand.CommandType = CommandType.Text;

            //Criar o tratamento para 
            try
            {
                dbCommand.Connection = cn;
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();
            }
        }



        public static string Descriptografar(string texto, bool usaHash)
        {
            const string key = "5A14B23C32D41E5";
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(texto);


            if (usaHash)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public Dictionary<string, string> ExecutaProcedure(string nomeProc, List<ParamProcedure> parametros)
        {
            OracleConnection cn = new OracleConnection();
            OracleCommand dbCommand = cn.CreateCommand();

            cn = GetConnection();

            dbCommand.CommandText = nomeProc;
            dbCommand.CommandType = CommandType.StoredProcedure;

            // Parâmetros
            foreach (ParamProcedure item in parametros)
            {
                if (item.direcao == ParameterDirection.Output)
                {
                    dbCommand.Parameters.Add(item.nome, item.tipo, 2000).Direction = ParameterDirection.Output;
                }
                else
                {
                    if (item.tipo == OracleDbType.Double || item.tipo == OracleDbType.Decimal)
                    {
                        dbCommand.Parameters.Add(item.nome, item.tipo).Value = Convert.ToDecimal(item.valor);
                    }
                    else if (item.tipo == OracleDbType.Date)
                    {
                        DateTime dt = DateTime.ParseExact(item.valor, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        dbCommand.Parameters.Add(item.nome, item.tipo).Value = dt;
                    }
                    else if (item.tipo == OracleDbType.Int16 || item.tipo == OracleDbType.Int32)
                    {
                        dbCommand.Parameters.Add(item.nome, item.tipo).Value = Convert.ToInt32(item.valor);
                    }
                    else
                    {
                        dbCommand.Parameters.Add(item.nome, item.tipo).Value = item.valor;
                    }
                }

                //Uteis.Log("ExecutaProcedure", "Parametro: " + item.nome.ToString() + " valor: " + item.valor.ToString() + " direcao: " + item.direcao);

            }

            //Uteis.Log("ExecutaProcedure", "Parametros count: " + dbCommand.Parameters.Count.ToString());

            Dictionary<string, string> ret = new Dictionary<string, string>();

            //Criar o tratamento para 
            try
            {
                dbCommand.Connection = cn;
                cn.Open();
                dbCommand.ExecuteNonQuery();

                // Parâmetros de retorno
                foreach (ParamProcedure item in parametros)
                {
                    if (item.direcao == ParameterDirection.Output)
                    {
                        ret.Add(item.nome, dbCommand.Parameters[item.nome].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                dbCommand.Dispose();
                cn.Dispose();
            }

            return ret;
        }

       /* public void gravarConexao(string comando, string divisor)
        {
            //Uteis.Log("GravandoConexao", "Gravando conexão com o banco de dados...");

            Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection dbConnString = webConfig.ConnectionStrings;
            // Alterando
            dbConnString.ConnectionStrings["Conex"].ConnectionString = comando;
            // Gravando Alteração
            webConfig.Save();

        }*/

    }

    public class ParamProcedure
    {
        public string nome { get; set; }

        public string valor { get; set; }

        public OracleDbType tipo { get; set; }

        public ParameterDirection direcao { get; set; }
    }

}