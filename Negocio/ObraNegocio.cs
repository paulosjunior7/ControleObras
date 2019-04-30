using ControleObras.Entidades;
using ControleObras.Persistencia;
using System.Data;

namespace ControleObras.Negocio
{
    class ObraNegocio
    {
        private Obra obra;

        public ObraNegocio(Obra obra)
        {
            this.obra = obra;
        }

        public void Gravar()
        {



            BancodeDados conn = new BancodeDados();

            string comando = string.Format("INSERT INTO OBRASC (CODIGOOBRA, CODCLIENTE, DATACADASTRO, STATUS, CEP, LOGRADOURO," +
                " UNIDADES, AREAUNIDADE, AREALOTE, AREATOTAL, INSCRICAOMUNICIPAL, NUMEROALVARA, USODESOLO, MATRICULAMAE, ART,  VALORTOTAL) VALUES" +
                "({0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15})",
                obra.CodigoObra,
                obra.Pessoa.Codigo,
                "sysdate",
                obra.StatusObra.ToString(),
                obra.Cep,
                obra.Logradouro,
                obra.Unidades,
                obra.AreaUnidade,
                obra.AreaLote,
                obra.AreaTotal,
                obra.InscricaoMunicipal,
                obra.NumeroAlvara,
                obra.Usodesolo,
                obra.MatriculaMae,
                obra.Art,
                obra.ValorTotal
                );

            conn.ExecutaComando(comando);

            foreach (MaterialObra item in obra.ListamaterialObras)
            {
                comando = null;
                comando = string.Format("INSERT INTO MATERIALOBRA (" +
                    "CODIGOOBRA, " +
                    "CODMEMORIAL, " +
                    "UNIDADE, " +
                    "QUANTIDADE, " +
                    "PRECOUNITARIO, " +
                    "DATACOMPRA, " +
                    "CODIGOFORNECEDOR, " +
                    "FORMAPAGAMENTO, " +
                    "VALORTOTALITEM) VALUES ('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}',{8})",
                    item.CodigoObra,
                    item.Produto.Codigo,
                    item.Unidade,
                    item.Quantidade,
                    item.PrecoUnitario,
                    "sysdate",
                    item.Fornecedor.Codigo,
                    item.FormaPagamento,
                    item.ValorTotalItem);

                conn.ExecutaComando(comando);
            }
        }

        public DataTable ProximoCodigo()
        {
            BancodeDados conn = new BancodeDados();

            return conn.ExecutaComando("SELECT SEQ_CODIGOOBRA.NEXTVAL CODIGO FROM DUAL");
        }
    }
}
