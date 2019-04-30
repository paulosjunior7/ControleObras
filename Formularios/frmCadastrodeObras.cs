using ControleObras.Entidades;
using ControleObras.Negocio;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ControleObras.Formularios
{
    public partial class frmCadastrodeObras : Form
    {
        public frmCadastrodeObras(StatusTela statusTela, Obra obra)
        {
            InitializeComponent();
            gStatusTela = statusTela;
        }

        public Obra obra = new Obra();
        public Pessoa _funcionario = new Pessoa();
        StatusTela gStatusTela;

        private void frmCadastrodeObras_Load(object sender, EventArgs e)
        {
            cbFormaPgtoMemorial.SelectedIndex = 0;
            cbQuantidade.SelectedIndex = 0;
            edtDataCadastro.Text = DateTime.Now.ToShortDateString();
            cbStatus.SelectedIndex = 0;
            cbUnidade.SelectedIndex = 0;
            //edtPeriodoFinal.Value = (edtPeriodoInial.Value.AddMonths(+1));

            Version version = Assembly.GetEntryAssembly().GetName().Version;
            toolStripStatusLabel1.Text = "Versão: " + version.ToString();

            /*
            MemorialDescritivoNegocio memorialDescritivoNegocio = new MemorialDescritivoNegocio();
            List<Produto> listagemMemorial = new List<Produto>();

            foreach (DataRow item in memorialDescritivoNegocio.Pesquisar().Rows)
            {
                Produto memorialDescritivo = new Produto();
                memorialDescritivo.Codigo = Convert.ToInt16(item["CODIGO"]);
                memorialDescritivo.Descricao = item["DESCRICAO"].ToString();
                memorialDescritivo.Codgrupo = Convert.ToInt16(item["CODGRUPO"]);

                listagemMemorial.Add(memorialDescritivo);
                edtdescricaoMemorial.AutoCompleteCustomSource.Add(memorialDescritivo.Descricao);
            }

            Pessoa pessoa = new Pessoa();
            pessoa.TipoCadastro = TipoCadastro.Cliente;
            PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoa);
            foreach(DataRow item in pessoaNegocio.Pesquisar().Rows)
            {
                edtDescricaoCliente.AutoCompleteCustomSource.Add(item["NOMEFANTASIA"].ToString());
            }*/

        }

        public void CalcularValorTotal()
        {
            var soma = obra.ListamaterialObras.Sum(item => item.ValorTotalItem);
            edtValorTotal.Text = soma.ToString();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private DataTable ProximoCodigoObra()
        {
            ObraNegocio obraNegocio = new ObraNegocio(null);
            return obraNegocio.ProximoCodigo();
        }

        public void Salvar()
        {
            if (validarcamposObrigatoriosMaterial())
            {
                return;
            }

            Pessoa pessoa = new Pessoa();

            obra.Pessoa = pessoa;
            obra.DataCadastro = DateTime.Now;

            switch (cbStatus.SelectedIndex)
            {
                case 0: obra.StatusObra = StatusObra.EmAndamento; break;
                case 1: obra.StatusObra = StatusObra.EmPlanejamento; break;
                case 2: obra.StatusObra = StatusObra.Finalizada; break;
                case 3: obra.StatusObra = StatusObra.Vendida; break;
                default: MessageBox.Show("Informe o Status da Obra"); break;
            }

            obra.Cep = edtCEP.Text;
            obra.Logradouro = edtLogradouro.Text;

            switch (cbQuantidade.SelectedIndex)
            {
                case 0: obra.Unidades = "1"; break;
                case 1: obra.Unidades = "2"; break;
                case 2: obra.Unidades = "3"; break;
                case 3: obra.Unidades = "4"; break;
                default: MessageBox.Show("Informe a Unidade da Obra"); break;
            }

            obra.AreaUnidade = Convert.ToDouble(edtM2.Text);
            obra.AreaTotal = Convert.ToDouble(edtAreaTotal.Text);
            obra.InscricaoMunicipal = edtInscricaoMunicipal.Text;
            obra.NumeroAlvara = edtAlvara.Text;
            obra.Usodesolo = edtUsodeSolo.Text;
            obra.MatriculaMae = edtMatricula.Text;
            obra.Art = edtART.Text;

            ObraNegocio obraNegocio = new ObraNegocio(obra);

            try
            {
                if (gStatusTela == StatusTela.Incluir)
                {
                    foreach (DataRow item in ProximoCodigoObra().Rows)
                    {
                        obra.CodigoObra = Convert.ToInt16(item["CODIGO"].ToString());
                    }
                    obraNegocio.Gravar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao Gravar! " + ex.Message);
            }
        }

        public void LimparEditMaterial()
        {
            edtCodigoMemorial.Text = "";
            edtdescricaoMemorial.Text = "";
            cbUnidade.SelectedIndex = -1;
            edtQuantidadeMemorial.Text = "";
            edtPrecoUnitarioMemorial.Text = "";
            edtCodigoFornecMemorial.Text = "";
            edtNomeFornecedor.Text = "";
            cbFormaPgtoMemorial.SelectedIndex = 0;
            cbUnidade.SelectedIndex = 0;
            edtDataCompraMemorial.Text = "";
            edtCodigoMemorial.Focus();
        }

        public void LimpaEditEquipe()
        {
            edtCodFuncEquipe.Text = "";
            edtdescFuncEquipe.Text = "";
            edtTempoEquipe.Text = "";
        }

        public void LimpaEditTerceirizado()
        {
            edtCodigoTerceirizado.Text = "";
            edtNomeTerceirizado.Text = "";
        }

        public void IncluirMaterial()
        {
            MaterialObra materialObra = new MaterialObra();
            Produto produto = new Produto();
            Pessoa fornecedor = new Pessoa();

            if (validarcamposObrigatoriosMaterial())
            {
                return;
            }

            produto.Codigo = Convert.ToInt16(edtCodigoMemorial.Text);
            produto.Descricao = edtdescricaoMemorial.Text;
            materialObra.Produto = produto;

            fornecedor.Codigo = Convert.ToInt16(edtCodigoFornecMemorial.Text);
            fornecedor.Nome = edtNomeFornecedor.Text;
            materialObra.Fornecedor = fornecedor;

            materialObra.CodigoObra = Convert.ToInt16(edtCodigoMemorial.Text);

            switch (cbUnidade.SelectedIndex)
            {
                case 0: materialObra.Unidade = "UN"; break;
                case 1: materialObra.Unidade = "LT"; break;
                case 2: materialObra.Unidade = "MT"; break;
                case 3: materialObra.Unidade = "MT2"; break;
                case 4: materialObra.Unidade = "MT3"; break;
                case 5: materialObra.Unidade = "PC"; break;
                case 6: materialObra.Unidade = "SC"; break;
                case 7: materialObra.Unidade = "KG"; break;

                default: MessageBox.Show("Informe a Unidade da Obra"); break;
            }

            materialObra.Quantidade = Convert.ToDouble(edtQuantidadeMemorial.Text);
            materialObra.PrecoUnitario = Convert.ToDouble(edtPrecoUnitarioMemorial.Text);
            materialObra.DataCompra = Convert.ToDateTime(edtDataCompraMemorial.Text);
            materialObra.ValorTotalItem = (materialObra.PrecoUnitario * materialObra.Quantidade);

            switch (cbFormaPgtoMemorial.SelectedIndex)
            {
                case 0: materialObra.FormaPagamento = "A Vista"; break;
                case 1: materialObra.FormaPagamento = "Parcelado"; break;
                case 2: materialObra.FormaPagamento = "A Prazo"; break; ;
                default: MessageBox.Show("Informe a Forma de Pagamento"); break;
            }

            obra.AdicionarMaterial(materialObra);
            LimparEditMaterial();
        }

        public void PreencherGridMaterial()
        {
            dtGridMaterial.DataSource = null;
            dtGridMaterial.DataSource = obra.ListamaterialObras;
            // dtGridMaterial.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

        private void dtGridMaterial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dtGridMaterial.Rows[e.RowIndex].DataBoundItem != null) && (dtGridMaterial.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(dtGridMaterial.Rows[e.RowIndex].DataBoundItem, dtGridMaterial.Columns[e.ColumnIndex].DataPropertyName);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            IncluirMaterial();
            PreencherGridMaterial();
        }

        private void PreencherGridEquipe()
        {
            dtGridEquipeObra2.DataSource = null;
            dtGridEquipeObra2.DataSource = obra.ListaEquipeObra;
        }

        private void PreencherGridTerceirizado()
        {
            dtGridTerceirizado.DataSource = null;
            dtGridTerceirizado.DataSource = obra.ListaEquipeTerceirizado;
        }


        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void RemoverMaterial()
        {
            obra.RemoverMaterial(dtGridMaterial.SelectedRows[0].DataBoundItem as MaterialObra);
        }
        private void RemoverEquipeObra()
        {
            obra.RemoverEquipeObra(dtGridEquipeObra2.SelectedRows[0].DataBoundItem as EquipeObra);
        }
        private void RemoverTerceirizado()
        {
            obra.RemoverTerceirizado(dtGridTerceirizado.SelectedRows[0].DataBoundItem as Terceirizado);
        }
       
        public Boolean validarcamposObrigatoriosMaterial()
        {
            if (dtGridMaterial.DataSource == null)
            {
                if (edtCodigoMemorial.Text == "")
                {
                    MessageBox.Show("Informe o código do Produto!");
                    edtCodigoMemorial.Focus();
                    return true;
                }

                if (cbUnidade.SelectedIndex == -1)
                {
                    MessageBox.Show("Informe a Unidade!");
                    cbUnidade.Focus();
                    return true;
                }
                if (edtQuantidadeMemorial.Text == "")
                {
                    MessageBox.Show("Informe a Quantidade!");
                    edtQuantidadeMemorial.Focus();
                    return true;
                }
                if (edtPrecoUnitarioMemorial.Text == "")
                {
                    MessageBox.Show("Informe o Preço de Custo!");
                    edtPrecoUnitarioMemorial.Focus();
                    return true;
                }
                if (edtDataCompraMemorial.Text == "")
                {
                    MessageBox.Show("Informe a Data da Compra!");
                    edtDataCompraMemorial.Focus();
                    return true;
                }
                if (edtCodigoFornecMemorial.Text == "")
                {
                    MessageBox.Show("Informe o Código do Fornecedor!");
                    edtCodigoFornecMemorial.Focus();
                    return true;
                }
            }
            return false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CalcularValorTotal();
        }

        private void btnIncluirEquipe_Click(object sender, EventArgs e)
        {
            IncluirEquipe();
            PreencherGridEquipe();
        }

        private void IncluirEquipe()
        {
            EquipeObra equipeObra = new EquipeObra();

            if (validaCamposObrigatoriosEquipe())
            {
                return;
            }

            _funcionario.Tempo = Convert.ToDouble(edtTempoEquipe.Text);

            equipeObra.Funcionario = _funcionario;
            equipeObra.AtualizaCusto();

            obra.ListaEquipeObra.Add(equipeObra);
            LimpaEditEquipe();
            _funcionario = new Pessoa();
        }

        private bool validaCamposObrigatoriosEquipe()
        {
            if (dtGridEquipeObra2.DataSource == null)
            {
                if (edtCodFuncEquipe.Text == "")
                {
                    MessageBox.Show("Informe o código do Funcionário!");
                    edtCodFuncEquipe.Focus();
                    return true;
                }
                if (edtTempoEquipe.Text == "")
                {
                    MessageBox.Show("Informe o Tempo(dias) de trabalho!");
                    edtTempoEquipe.Focus();
                    return true;
                }
                //edtdescFuncEquipe
            }
            return false;
        }

        private bool validaCamposObrigatoriosTerceirizado()
        {
            if (dtGridTerceirizado.DataSource == null)
            {
                if (edtCodigoTerceirizado.Text == "")
                {
                    MessageBox.Show("Informe o código do Funcionário Terceirizado!");
                    edtCodFuncEquipe.Focus();
                    return true;
                }
            }
            return false;
        }

        private void btnIncluirFuncionario_Click(object sender, EventArgs e)
        {
            _funcionario = new Pessoa();
            frmListagemPessoas frmListagemPessoas = new frmListagemPessoas(StatusTela.Pesquisa, TipoCadastro.Funcionario);
            frmListagemPessoas.ShowDialog();
            if (frmListagemPessoas.gPessoaSelecionada != null)
            {
                edtCodFuncEquipe.Text = frmListagemPessoas.gPessoaSelecionada.Codigo.ToString();
                edtdescFuncEquipe.Text = frmListagemPessoas.gPessoaSelecionada.Nome.ToString();
            }
            _funcionario = frmListagemPessoas.gPessoaSelecionada;
        }

        private void btnAddCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnIncluirProduto_Click(object sender, EventArgs e)
        {
            frmListagemMemorialDescritivo frmListagemProdutos = new frmListagemMemorialDescritivo();
            frmListagemProdutos.ShowDialog();
            if (frmListagemProdutos.gProdutoSelecionado != null)
            {
                edtCodigoMemorial.Text = frmListagemProdutos.gProdutoSelecionado.Codigo.ToString();
                edtdescricaoMemorial.Text = frmListagemProdutos.gProdutoSelecionado.Descricao.ToString();
            }
        }

        private void btnIncluirFornecedor_Click(object sender, EventArgs e)
        {
            frmListagemPessoas frmListagemPessoas = new frmListagemPessoas(StatusTela.Pesquisa, TipoCadastro.Fornecedor);
            frmListagemPessoas.ShowDialog();
            if (frmListagemPessoas.gPessoaSelecionada != null)
            {
                edtCodigoFornecMemorial.Text = frmListagemPessoas.gPessoaSelecionada.Codigo.ToString();
                edtNomeFornecedor.Text = frmListagemPessoas.gPessoaSelecionada.Nome.ToString();
            }
        }

        private void edtCodigoCliente_Leave(object sender, EventArgs e)
        {
            if (edtCodigoCliente.Text != "" && edtDescricaoCliente.Text == "")
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Codigo = Convert.ToInt32(object.Equals(edtCodigoCliente.Text, 0));
                PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoa);

                foreach (DataRow item in pessoaNegocio.Pesquisar().Rows)
                {
                    edtCodigoCliente.Text = item["CODIGO"].ToString();
                    edtDescricaoCliente.Text = item["NOMEFANTASIA"].ToString();
                    return;
                }

                MessageBox.Show("Código Invalido!");
                edtDescricaoCliente.Text = "";
                edtCodigoCliente.Text = "";
                edtCodigoCliente.Focus();
            }
        }

        private void edtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void edtM2_TextChanged(object sender, EventArgs e)
        {

        }

        private void edtCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            int asciiCode = Convert.ToInt32(e.KeyValue);
            if ((asciiCode >= 48 && asciiCode <= 57) || (asciiCode == 106))
            {
                frmListagemPessoas frmlistagemPessoas = new frmListagemPessoas(StatusTela.Pesquisa, TipoCadastro.Cliente);
                edtCodigoCliente.Text = "";
                frmlistagemPessoas.ShowDialog();

                if (frmlistagemPessoas.gPessoaSelecionada != null)
                {
                    edtCodigoCliente.Text = frmlistagemPessoas.gPessoaSelecionada.Codigo.ToString();
                    edtDescricaoCliente.Text = frmlistagemPessoas.gPessoaSelecionada.Nome;
                }
            }
            else
            {

            }
        }

        private void edtM2_Leave(object sender, EventArgs e)
        {
            if (edtM2.Text != "")
            {
                double quantidade = Convert.ToDouble(cbQuantidade.SelectedItem.ToString());
                double m2 = Convert.ToDouble(edtM2.Text);


                edtAreaTotal.Text = (quantidade * m2).ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmListagemPessoas frmListagemPessoas = new frmListagemPessoas(StatusTela.Pesquisa, TipoCadastro.Terceirizado);
            frmListagemPessoas.ShowDialog();
            if (frmListagemPessoas.gPessoaSelecionada != null)
            {
                edtCodigoTerceirizado.Text = frmListagemPessoas.gPessoaSelecionada.Codigo.ToString();
                edtNomeTerceirizado.Text = frmListagemPessoas.gPessoaSelecionada.Nome.ToString();
            }
            _funcionario = frmListagemPessoas.gPessoaSelecionada;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void dtGridEquipeObra2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dtGridEquipeObra2.Rows[e.RowIndex].DataBoundItem != null) && (dtGridEquipeObra2.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(dtGridEquipeObra2.Rows[e.RowIndex].DataBoundItem, dtGridEquipeObra2.Columns[e.ColumnIndex].DataPropertyName);
            }
        }

        private void btnIncluiTerceirizado_Click(object sender, EventArgs e)
        {
            IncluirTerceirizado();
            PreencherGridTerceirizado();
        }

        private void IncluirTerceirizado()
        {
            Terceirizado terceirizado = new Terceirizado();

            if (validaCamposObrigatoriosTerceirizado())
            {
                return;
            }

            _funcionario.Tempo = Convert.ToDouble(edtTempoEquipe.Text);

            terceirizado.FuncTerceirizado = _funcionario;
            terceirizado.AtualizaCusto(Convert.ToDouble(edtM2.Text));

            obra.ListaEquipeTerceirizado.Add(terceirizado);
            LimpaEditTerceirizado();
            _funcionario = new Pessoa();
        }

        private void dtGridTerceirizado_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dtGridTerceirizado.Rows[e.RowIndex].DataBoundItem != null) && (dtGridTerceirizado.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(dtGridTerceirizado.Rows[e.RowIndex].DataBoundItem, dtGridTerceirizado.Columns[e.ColumnIndex].DataPropertyName);
            }
        }

        private void DtGridTerceirizado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox32_TextChanged(object sender, EventArgs e)
        {

        }

        private void EdtCodigoMemorial_Leave(object sender, EventArgs e)
        {
            if (edtCodigoMemorial.Text != "" && edtdescricaoMemorial.Text == "")
            {
                Produto produto = new Produto();
                produto.Codigo = Convert.ToInt32(edtCodigoMemorial.Text);
                ProdutoNegocio produtoNegocio = new ProdutoNegocio(produto);

                foreach (DataRow item in produtoNegocio.Pesquisar().Rows)
                {
                    edtCodigoMemorial.Text = item["CODIGO"].ToString();
                    edtdescricaoMemorial.Text = item["DESCRICAO"].ToString();
                    return;
                }

                MessageBox.Show("Código Invalido!");
                edtCodigoMemorial.Text = "";
                edtdescricaoMemorial.Text = "";
                edtCodigoMemorial.Focus();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridTerceirizado();
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (dtGridMaterial.SelectedRows.Count != 0)
            {
                RemoverMaterial();
                PreencherGridMaterial();
            }
        }

        private void BtnIncluiTerceirizado_Click_1(object sender, EventArgs e)
        {
            IncluirTerceirizado();
            PreencherGridTerceirizado();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            _funcionario = new Pessoa();
            frmListagemPessoas frmListagemPessoas = new frmListagemPessoas(StatusTela.Pesquisa, TipoCadastro.Terceirizado);
            frmListagemPessoas.ShowDialog();
            if (frmListagemPessoas.gPessoaSelecionada != null)
            {
                edtCodigoTerceirizado.Text = frmListagemPessoas.gPessoaSelecionada.Codigo.ToString();
                edtNomeTerceirizado.Text = frmListagemPessoas.gPessoaSelecionada.Nome.ToString();
                edtTipoRemuneracao.Text = frmListagemPessoas.gPessoaSelecionada.TipoRemuneracao.ToString();
            }
            _funcionario = frmListagemPessoas.gPessoaSelecionada;
            if (frmListagemPessoas.gPessoaSelecionada.TipoRemuneracao.ToString()  == "Dia")
            {
                edtTempodiasT.Enabled = true;
            }
        }

        private void btnRemoverTerceirizado_Click(object sender, EventArgs e)
        {
            RemoverTerceirizado();
            PreencherGridTerceirizado();
        }

        private void Label18_Click(object sender, EventArgs e)
        {

        }

        private void EdtCodFuncEquipe_Leave(object sender, EventArgs e)
        {
            edtdescFuncEquipe.Text = "";
            if (edtCodFuncEquipe.Text != "" && edtdescFuncEquipe.Text == "")
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Codigo = Convert.ToInt32(edtCodFuncEquipe.Text);
                pessoa.TipoCadastro = TipoCadastro.Funcionario;
                PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoa);

                foreach (DataRow item in pessoaNegocio.Pesquisar().Rows)
                {
                    edtCodFuncEquipe.Text = item["CODIGO"].ToString();
                    edtdescFuncEquipe.Text = item["NOMEFANTASIA"].ToString();
                    return;
                }

                MessageBox.Show("Código Invalido!");
                edtCodFuncEquipe.Text = "";
                edtdescFuncEquipe.Text = "";
                edtCodFuncEquipe.Focus();
            }
        }
    }
}
