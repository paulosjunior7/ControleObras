using ControleObras.Entidades;
using ControleObras.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ControleObras.Negocio
{
    class PessoaNegocio
    {
        private Pessoa pessoa;

        public PessoaNegocio(Pessoa pessoa)
        {
            this.pessoa = pessoa;
        }

        public void Gravar()
        {
            BancodeDados conn = new BancodeDados();
            string cargo = "";
            string tipoRemuneracao = "";
            string remuneracao = "";

            if (String.IsNullOrEmpty(pessoa.Nome))
            {
                MessageBox.Show("Informar o Nome: ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pessoa.TipoCadastro == TipoCadastro.Cliente || pessoa.TipoCadastro == TipoCadastro.Fornecedor)
            {
                cargo = "";
                tipoRemuneracao = "";
                remuneracao = "";
            }
            else
            {
                cargo = pessoa.Cargo.ToString();
                tipoRemuneracao = pessoa.TipoRemuneracao.ToString();
                remuneracao = pessoa.TipoRemuneracao.ToString();
            }


            try
            {

                string comando = string.Format("INSERT INTO PESSOA (" +
                    " CODIGO            " +      //0 
                    ",NOMEFANTASIA      " +      //1
                    ",DATACADASTRO      " +      //2
                    ",RAZAOSOCIAL       " +      //3
                    ",RG_IE             " +      //4
                    ",CPF_CNPJ          " +      //5
                    ",TELEFONE          " +      //6
                    ",CELULAR           " +      //7
                    ",TIPOCADASTRO      " +      //8
                    ",TIPOPESSOA        " +      //9
                    ",OBSERVACAO        " +      //10
                    ",INATIVO           " +      //11
                    ",EMAIL             " +      //12
                    ",CEP               " +      //13
                    ",LOGRADOURO        " +      //14
                    ",BAIRRO            " +      //15
                    ",CIDADE            " +      //16
                    ",UF                " +      //17
                    ",TIPOREMUNERACAO   " +      //18
                    ",CARGO             " +      //19
                    ",REMUNERACAO       " +      //20
                    ") VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                    pessoa.Codigo,                    //0    
                    pessoa.Nome.ToUpper(),            //1
                    "sysdate",                        //2
                    pessoa.RazaoSocial.ToUpper(),     //3
                    pessoa.Rg,                        //4
                    pessoa.CpfCnpj,                   //5
                    pessoa.Telefone,                  //6
                    pessoa.Celular,                   //7
                    pessoa.TipoCadastro,              //8
                    pessoa.Tipopessoa,                //9
                    pessoa.Observacao,                //10
                    'N',                              //11
                    pessoa.Email.ToUpper(),           //12
                    pessoa.Cep,                       //13
                    pessoa.Logradouro.ToUpper(),      //14
                    pessoa.Bairro.ToUpper(),          //15
                    pessoa.Cidade.ToUpper(),          //16
                    "GO",                             //17
                    tipoRemuneracao,           //18
                    cargo,                     //19
                    remuneracao                //20
                    );

                conn.ExecutaComando(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao Gravar! " + ex.Message);
            }
        }

        public DataTable Pesquisar()
        {
            BancodeDados conn = new BancodeDados();
            string comando = "SELECT * FROM PESSOA WHERE INATIVO <> 'S' ";

            if ((!String.IsNullOrEmpty(pessoa.Codigo.ToString())) && (pessoa.Codigo != 0))
            {
                comando += string.Format("AND CODIGO = {0}", pessoa.Codigo);
            }

            if (!string.IsNullOrEmpty(pessoa.Nome))
            {
                comando += string.Format("AND NOMEFANTASIA LIKE '%{0}%'", pessoa.Nome.ToUpper());
            }

            if (!string.IsNullOrEmpty(pessoa.CpfCnpj))
            {
                comando += string.Format("AND CPF_CNPJ LIKE '%{0}%'", pessoa.CpfCnpj);
            }
            if (pessoa.TipoCadastro.ToString() != null && pessoa.TipoCadastro.ToString() != "Todos")
            {
                comando += string.Format("AND tipocadastro LIKE '%{0}%'", pessoa.TipoCadastro.ToString());
            }

            return conn.ExecutaComando(comando);
        }

        public void Alterar()
        {
            try
            {
                BancodeDados conn = new BancodeDados();
                string comando = string.Format("UPDATE PESSOA SET " +
                    "NOMEFANTASIA = '{0}', " +
                    "RAZAOSOCIAL = '{1}',  " +
                    "RG_IE = '{2}',        " +
                    "CPF_CNPJ = '{3}',     " +
                    "TELEFONE = '{4}',     " +
                    " CELULAR = '{5}' ,    " +
                    "TIPOCADASTRO = '{6}' ," +
                    "OBSERVACAO = '{7}' ,  " +
                    "INATIVO = '{8}',     " +
                    "CEP = '{9}',         " +
                    "LOGRADOURO = '{10}' , " +
                    "BAIRRO = '{11}' ,     " +
                    "CIDADE = '{12}' ,    " +
                    "UF = '{13}' ,      " +
                    "TIPOPESSOA = '{14}',       " +
                    "CARGO = '{15}'  ,     " +
                    "TIPOREMUNERACAO = '{16}',       " +
                    "REMUNERACAO = '{17}',       " +
                    "EMAIL = '{18}'       " +
                    "WHERE CODIGO = {19}  ",

                    pessoa.Nome,            //0
                    pessoa.RazaoSocial,     //1
                    pessoa.Rg,              //2
                    pessoa.CpfCnpj,         //3
                    pessoa.Telefone,        //4
                    pessoa.Celular,         //5
                    pessoa.TipoCadastro,    //6
                    pessoa.Observacao,      //7
                    pessoa.Inativo,         //8
                    pessoa.Cep,             //9
                    pessoa.Logradouro,      //10
                    pessoa.Bairro,          //11
                    pessoa.Cidade,          //12
                    pessoa.Estado,          //13
                    pessoa.Tipopessoa,      //14
                    pessoa.Cargo,           //15
                    pessoa.TipoRemuneracao, //16
                    pessoa.Remuneracao,    //17
                    pessoa.Email,          //18
                    pessoa.Codigo);         //19

                conn.ExecutaComando(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha!" + Environment.NewLine + ex.Message);
            }
        }

        public void Excluir()
        {
            try
            {
                BancodeDados conn = new BancodeDados();
                string comando = string.Format("UPDATE PESSOA SET INATIVO = 'S' WHERE CODIGO = {0}", pessoa.Codigo);
                conn.ExecutaComando(comando);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir registro!" + Environment.NewLine + ex.Message);
            }
        }

        public DataTable ProximoCodigo()
        {
            BancodeDados conn = new BancodeDados();

            return conn.ExecutaComando("SELECT PESSOA_SEQ.NEXTVAL CODIGO FROM DUAL");
        }
    }
}
