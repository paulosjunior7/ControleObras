using ControleObras.Entidades;
using ControleObras.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleObras.Negocio
{
    class ProdutoNegocio
    {

        private Produto produto;
        public ProdutoNegocio(Produto produto)
        {
            this.produto = produto;
        }

        public DataTable Pesquisar()
        {
            BancodeDados conn = new BancodeDados();
            string comando = string.Format(
            "   SELECT   m.codigo,                         " +
            "            m.descricao,                      " +
            "            m.codgrupo,                       " +
            "            g.descricao descricaogrupo        " +
            "     FROM   memorialdescritivo m, grupos g    " +
            "    WHERE   m.codigo = g.codigo               ");

            if (produto.Codigo > 0)
            {
                comando += string.Format(@" and m.codigo = {0} ", produto.Codigo.ToString());
            }

            return conn.ExecutaComando(comando);
        }
    }
}
