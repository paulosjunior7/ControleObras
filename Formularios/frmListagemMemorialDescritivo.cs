using ControleObras.Entidades;
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

namespace ControleObras.Formularios
{
    public partial class frmListagemMemorialDescritivo : Form
    {
        public frmListagemMemorialDescritivo()
        {
            InitializeComponent();
        }

        public Produto gProdutoSelecionado;

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void Pesquisar()
        {
            Produto produto = new Produto();
            ProdutoNegocio produtoNegocio = new ProdutoNegocio(produto);
            Grupos grupo = new Grupos();
            List<Produto> listagemMemorial = new List<Produto>();

            foreach (DataRow item in produtoNegocio.Pesquisar().Rows)
            {
                grupo = new Grupos();
                Produto memorialDescritivo = new Produto();
                memorialDescritivo.Codigo = Convert.ToInt16(item["CODIGO"]);
                memorialDescritivo.Descricao = item["DESCRICAO"].ToString();
                grupo.Codigo = Convert.ToInt16(item["CODGRUPO"]);
                grupo.Descricao = item["DESCRICAOGRUPO"].ToString();
                memorialDescritivo.Grupo = grupo;

                listagemMemorial.Add(memorialDescritivo);
            }
            gridListagem.DataSource = null;
            gridListagem.DataSource = listagemMemorial;

        }

        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";
            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;
                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;
                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }
            return retValue;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroMemorialDescritivo frmCadastroMemorialDescritivo = new frmCadastroMemorialDescritivo();
            frmCadastroMemorialDescritivo.ShowDialog();
        }

        private void gridListagem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gProdutoSelecionado = new Produto();
            gProdutoSelecionado = (gridListagem.SelectedRows[0].DataBoundItem as Produto);
            Close();
        }

        private void gridListagem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((gridListagem.Rows[e.RowIndex].DataBoundItem != null) && (gridListagem.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(gridListagem.Rows[e.RowIndex].DataBoundItem, gridListagem.Columns[e.ColumnIndex].DataPropertyName);
            }
        }
    }
}

