using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControleObras.Entidades;
using ControleObras.Negocio;

namespace ControleObras.Formularios
{
    public partial class frmCadastroMemorialDescritivo : Form
    {
        public frmCadastroMemorialDescritivo()
        {
            InitializeComponent();
        }

        private void btnincluir_Click(object sender, EventArgs e)
        {
            
            TreeNode node = new TreeNode(comboBox1.SelectedItem.ToString());



            if (lista.Nodes.Contains(node))
            {
                MessageBox.Show("existe!");
            }

                lista.Nodes.Add(node);
            lista.SelectedNode = node;

           TreeNode Itemnode = new TreeNode(edtcodgrupo.Text + " - " + edtDescricaoGrupo.Text);
            lista.SelectedNode.Nodes.Add(Itemnode);
        }

        private void frmCadastroMemorialDescritivo_Load(object sender, EventArgs e)
        {
            try
            {
                GruposNegocio gruposNegocio = new GruposNegocio(null);
                foreach (DataRow item in gruposNegocio.Pesquisar().Rows)
                {
                    comboBox1.Items.Add(item["CODIGO"].ToString() + " - "  + item["DESCRICAO"]);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
