using ControleObras.Entidades;
using ControleObras.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ControleObras.Formularios
{
    public partial class frmListagemPessoas : Form
    {
        StatusTela gStatusTela;
        public frmListagemPessoas(StatusTela statusTela, TipoCadastro tipoCadastro)
        {
            InitializeComponent();
            cbTipoCadastro.SelectedIndex = 3;
            gStatusTela = statusTela;

            if (tipoCadastro == TipoCadastro.Funcionario)
            {
                cbTipoCadastro.SelectedIndex = 2;
                cbTipoCadastro.Enabled = false;
                this.Text = "Pesquisar Funcionário";
            }
            if (tipoCadastro == TipoCadastro.Todos)
            {
                cbTipoCadastro.SelectedIndex = 4;
                cbTipoCadastro.Enabled = false;
                
            }
            if (tipoCadastro == TipoCadastro.Fornecedor)
            {
                cbTipoCadastro.SelectedIndex = 1;
                cbTipoCadastro.Enabled = true;
                this.Text = "Pesquisar Fornecedor";
            }
            if (tipoCadastro == TipoCadastro.Cliente)
            {
                cbTipoCadastro.SelectedIndex = 0;
                cbTipoCadastro.Enabled = false;
                this.Text = "Pesquisar Cliente";
            }
            if (tipoCadastro == TipoCadastro.Terceirizado)
            {
                cbTipoCadastro.SelectedIndex = 3;
                cbTipoCadastro.Enabled = false;
                this.Text = "Pesquisar Funcionário Terceirizado";
            }

        }
        public Pessoa gPessoaSelecionada;
        public List<Pessoa> _ListaPessoa = new List<Pessoa>();

        public void Pesquisar()
        {
            Pessoa pessoa = new Pessoa();
            _ListaPessoa = new List<Pessoa>();

            if (!String.IsNullOrEmpty(edtCodigo.Text))
                pessoa.Codigo = Convert.ToInt16(edtCodigo.Text);
            if (!string.IsNullOrEmpty(edtNome.Text))
                pessoa.Nome = edtNome.Text;
            if (edtCPF.Text.Where(c => char.IsNumber(c)).Count() > 0)
            {
                pessoa.CpfCnpj = edtCPF.Text;
            }

            if (cbTipoCadastro.SelectedItem.ToString() == "Todos")
            {
                pessoa.TipoCadastro = TipoCadastro.Todos;
            }
            else if (cbTipoCadastro.SelectedItem.ToString() == "Cliente")
            {
                pessoa.TipoCadastro = TipoCadastro.Cliente;
            }
            else if (cbTipoCadastro.SelectedItem.ToString() == "Fornecedor")
            {
                pessoa.TipoCadastro = TipoCadastro.Fornecedor;
            }
            else if (cbTipoCadastro.SelectedItem.ToString() == "Funcionario")
            {
                pessoa.TipoCadastro = TipoCadastro.Funcionario;
            }

            PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoa);
            

            foreach (DataRow item in pessoaNegocio.Pesquisar().Rows)
            {
                pessoa = new Pessoa();
                pessoa.Codigo = Convert.ToInt16(item["CODIGO"]);
                pessoa.Nome = item["NOMEFANTASIA"].ToString();
                pessoa.DataCadastro = Convert.ToDateTime(item["DATACADASTRO"]);
                pessoa.RazaoSocial = item["RAZAOSOCIAL"].ToString();
                pessoa.Rg = item["RG_IE"].ToString();
                pessoa.CpfCnpj = item["CPF_CNPJ"].ToString();
                pessoa.Telefone = item["TELEFONE"].ToString();
                pessoa.Celular = item["CELULAR"].ToString();
                pessoa.Email = item["EMAIL"].ToString();
                pessoa.Cep = item["CEP"].ToString();
                pessoa.Logradouro = item["LOGRADOURO"].ToString();
                pessoa.Bairro = item["BAIRRO"].ToString();
                pessoa.Cidade = item["CIDADE"].ToString();

                switch (item["TIPOCADASTRO"].ToString())
                {
                    case "Cliente": pessoa.TipoCadastro = TipoCadastro.Cliente; break;
                    case "Fornecedor": pessoa.TipoCadastro = TipoCadastro.Fornecedor; break;
                    case "Funcionario": pessoa.TipoCadastro = TipoCadastro.Funcionario; break;
                    case "Terceirizado": pessoa.TipoCadastro = TipoCadastro.Terceirizado; break;
                }

                switch (item["TIPOPESSOA"].ToString())
                {
                    case "Fisica": pessoa.Tipopessoa = TipoPessoa.Fisica; break;
                    case "Juridica": pessoa.Tipopessoa = TipoPessoa.Juridica; break;
                }

                switch (item["TIPOREMUNERACAO"].ToString())
                {
                    case "Fixa": pessoa.TipoRemuneracao = TipoRemuneracao.Fixa; break;
                    case "Obra": pessoa.TipoRemuneracao = TipoRemuneracao.Obra; break;
                    case "Metro2": pessoa.TipoRemuneracao = TipoRemuneracao.Metro2; break;
                    case "Dia": pessoa.TipoRemuneracao = TipoRemuneracao.Dia; break;

                }

                switch (item["CARGO"].ToString())
                {
                    case "EncarregadoGeral": pessoa.Cargo = Cargo.EncarregadoGeral; break;
                    case "Encarregado": pessoa.Cargo = Cargo.Encarregado; break;
                    case "Pedreiro": pessoa.Cargo = Cargo.Pedreiro; break;
                    case "Servente": pessoa.Cargo = Cargo.Servente; break;
                    case "Pintor": pessoa.Cargo = Cargo.Pintor; break;
                    case "AssentadorPiso": pessoa.Cargo = Cargo.AssentadorPiso; break;
                    case "Calheiro": pessoa.Cargo = Cargo.Calheiro; break;
                    case "Gesseiro": pessoa.Cargo = Cargo.Gesseiro; break;
                    case "FuradorFossa": pessoa.Cargo = Cargo.FuradorFossa; break;
                    case "Motorista": pessoa.Cargo = Cargo.Motorista; break;
                    case "Encanador": pessoa.Cargo = Cargo.Encanador; break;
                    case "AuxiliarEncanador": pessoa.Cargo = Cargo.AuxiliarEncanador; break;
                    case "Secretaria": pessoa.Cargo = Cargo.Secretaria; break;
                    case "Estagiaria": pessoa.Cargo = Cargo.Estagiaria; break;
                }

                pessoa.Observacao = item["OBSERVACAO"].ToString();
                pessoa.Inativo = item["INATIVO"].ToString();
                pessoa.Remuneracao = Convert.ToDouble(item["REMUNERACAO"].ToString());
                _ListaPessoa.Add(pessoa);
            }

            gridListagem.DataSource = null;
            gridListagem.DataSource = _ListaPessoa;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroPessoa frmCadastro = new frmCadastroPessoa(StatusTela.Incluir, null);
            DialogResult dialog = frmCadastro.ShowDialog();
            if (dialog == DialogResult.Yes)
            {
                Pesquisar();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridListagem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Pessoa pessoaSelecionada = new Pessoa();
                pessoaSelecionada = (gridListagem.SelectedRows[0].DataBoundItem as Pessoa);

                frmCadastroPessoa frmCadastroPessoa = new frmCadastroPessoa(StatusTela.Editar, pessoaSelecionada);
                DialogResult dialog = frmCadastroPessoa.ShowDialog();
                if (dialog == DialogResult.Yes)
                {
                    Pesquisar();
                }
            }
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (gridListagem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Pessoa pessoaSelecionada = new Pessoa();
                pessoaSelecionada = (gridListagem.SelectedRows[0].DataBoundItem as Pessoa);

                frmCadastroPessoa frmCadastroPessoa = new frmCadastroPessoa(StatusTela.Visualizar, pessoaSelecionada);
                DialogResult dialog = frmCadastroPessoa.ShowDialog();
                if (dialog == DialogResult.Yes)
                {
                    Pesquisar();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridListagem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Deseja excluir o registro selecionado?", "Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        Pessoa pessoaSelecionada = new Pessoa();
                        pessoaSelecionada = (gridListagem.SelectedRows[0].DataBoundItem as Pessoa);

                        PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoaSelecionada);
                        pessoaNegocio.Excluir();

                        MessageBox.Show("Registro excluído com sucesso!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Pesquisar();
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível Excluir.Detalhes: ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmListagemPessoas_Load(object sender, EventArgs e)
        {

        }

        private void gridListagem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void gridListagem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (gStatusTela == StatusTela.Pesquisa)
            {
                gPessoaSelecionada = new Pessoa();
                gPessoaSelecionada = (gridListagem.SelectedRows[0].DataBoundItem as Pessoa);

                Close();
            }
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            Pesquisar();
        }
    }
}
