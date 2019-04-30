using ControleObras.Formularios;
using ControleObras.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ControleObras
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmListagemPessoas frmListagemPessoas = new frmListagemPessoas(StatusTela.Visualizar, TipoCadastro.Todos);
            frmListagemPessoas.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmListagemMemorialDescritivo frmListagemMemorialDescritivo = new frmListagemMemorialDescritivo();
            frmListagemMemorialDescritivo.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCadastrodeObras frmCadastrodeObras = new frmCadastrodeObras(StatusTela.Incluir, null);
            frmCadastrodeObras.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            lblVersao.Text = version.ToString();
        }
    }
}
