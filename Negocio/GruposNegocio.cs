using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControleObras.Entidades;
using ControleObras.Persistencia;
using System;
using System.Data;
using System.Windows.Forms;

namespace ControleObras.Negocio
{
    class GruposNegocio
    {
        private Grupos grupos;

        public GruposNegocio(Grupos grupos)
        {
            this.grupos = grupos;
        }

        public DataTable Pesquisar()
        {
            BancodeDados conn = new BancodeDados();
            string comando = "SELECT * FROM GRUPOS";

            return conn.ExecutaComando(comando);
        }
    }
}
