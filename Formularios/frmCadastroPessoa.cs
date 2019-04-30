using ControleObras.Entidades;
using ControleObras.Negocio;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace ControleObras.Formularios
{

    public partial class frmCadastroPessoa : Form
    {

        StatusTela gStatusTela;

        public frmCadastroPessoa(StatusTela statusTela, Pessoa pessoa)
        {
            InitializeComponent();
            this.gStatusTela = statusTela;

            if (statusTela == StatusTela.Incluir)
            {
                this.Text = "Inserir Pessoa";
                btnExcluir.Enabled = false;
                edtDataCadastro.Text = DateTime.Now.ToShortDateString();
                edtDataCadastro.ReadOnly = true;
                edtCodigo.ReadOnly = true;
                edtRazao.Enabled = false;
                comboTipoCadastro.SelectedIndex = 0;
                edtNome.Select();
                cbCargo.Enabled = false;
                cbTipoRemuneracao.Enabled = false;
                edtValorRemuneracao.Enabled = false;
            }
            else if (statusTela == StatusTela.Editar)
            {
                this.Text = "Alterar Pessoa";
                PreencherDados(pessoa);
            }
            else if (statusTela == StatusTela.Visualizar)
            {
                this.Text = "Visualizar Pessoa";
                visualizacao(pessoa, true);
                gStatusTela = StatusTela.Visualizar;
            }
        }

        public void visualizacao(Pessoa pessoa, Boolean cnd)
        {
            PreencherDados(pessoa);

            edtCodigo.ReadOnly = cnd;
            edtNome.ReadOnly = cnd;
            edtRazao.ReadOnly = cnd;
            edtDataCadastro.ReadOnly = cnd;
            edtRG.ReadOnly = cnd;
            edtCPF.ReadOnly = cnd;
            edtTelefone.ReadOnly = cnd;
            edtCelular.ReadOnly = cnd;
            edtEmail.ReadOnly = cnd;
            edtCEP.ReadOnly = cnd;
            edtLogradouro.ReadOnly = cnd;
            edtBairro.ReadOnly = cnd;
            edtCidade.ReadOnly = cnd;
            edtValorRemuneracao.ReadOnly = cnd;
            cbTipoPessoa.Enabled = !cnd;
            comboTipoCadastro.Enabled = !cnd;
            cbCargo.Enabled = !cnd;
            cbTipoRemuneracao.Enabled = !cnd;

            btnSalvar.Enabled = !cnd;
            btnFechar.Text = "Fechar";
            btnFechar.Focus();
        }

        private void PreencherDados(Pessoa pessoa)
        {
            edtCodigo.Text = pessoa.Codigo.ToString();
            edtNome.Text = pessoa.Nome;
            edtRazao.Text = pessoa.RazaoSocial;
            edtDataCadastro.Text = pessoa.DataCadastro.ToShortDateString();
            edtRG.Text = pessoa.Rg;
            edtCPF.Text = pessoa.CpfCnpj;
            edtTelefone.Text = pessoa.Telefone;
            edtCelular.Text = pessoa.Celular;
            edtEmail.Text = pessoa.Email;
            edtCEP.Text = pessoa.Cep;
            edtLogradouro.Text = pessoa.Logradouro;
            edtBairro.Text = pessoa.Bairro;
            edtCidade.Text = pessoa.Cidade;

            switch (pessoa.TipoCadastro)
            {
                case TipoCadastro.Cliente: comboTipoCadastro.SelectedIndex = 0; break;
                case TipoCadastro.Fornecedor: comboTipoCadastro.SelectedIndex = 1; break;
                case TipoCadastro.Funcionario: comboTipoCadastro.SelectedIndex = 2; break;
                case TipoCadastro.Terceirizado: comboTipoCadastro.SelectedIndex = 3; break;
            }

            switch (pessoa.Tipopessoa)
            {
                case TipoPessoa.Fisica: cbTipoPessoa.SelectedIndex = 0; break;
                case TipoPessoa.Juridica: cbTipoPessoa.SelectedIndex = 1; break;
            }

            switch (pessoa.TipoRemuneracao)
            {
                case TipoRemuneracao.Fixa: cbTipoRemuneracao.SelectedIndex = 0; break;
                case TipoRemuneracao.Obra: cbTipoRemuneracao.SelectedIndex = 1; break;
                case TipoRemuneracao.Metro2: cbTipoRemuneracao.SelectedIndex = 2; break;
                case TipoRemuneracao.Dia: cbTipoRemuneracao.SelectedIndex = 3; break;
            }

            switch (pessoa.Cargo)
            {
                case Cargo.EncarregadoGeral : cbCargo.SelectedIndex = 0; break;
                case Cargo.Encarregado : cbCargo.SelectedIndex = 1; break;
                case Cargo.Pedreiro : cbCargo.SelectedIndex = 2; break;
                case Cargo.Servente : cbCargo.SelectedIndex = 3; break;
                case Cargo.Pintor : cbCargo.SelectedIndex = 4; break;
                case Cargo.AssentadorPiso : cbCargo.SelectedIndex = 5; break;
                case Cargo.Calheiro : cbCargo.SelectedIndex = 6; break;
                case Cargo.Gesseiro : cbCargo.SelectedIndex = 7; break;
                case Cargo.FuradorFossa : cbCargo.SelectedIndex = 8; break;
                case Cargo.Motorista : cbCargo.SelectedIndex = 9; break;
                case Cargo.Encanador : cbCargo.SelectedIndex = 10; break;
                case Cargo.AuxiliarEncanador : cbCargo.SelectedIndex = 11; break;
                case Cargo.Secretaria : cbCargo.SelectedIndex = 12; break;
                case Cargo.Estagiaria : cbCargo.SelectedIndex = 13; break;
            }

            edtValorRemuneracao.Text = pessoa.Remuneracao.ToString("####,##");
        }

        public void SalvarCliente()
        {
            try
            {
                Pessoa pessoa = new Pessoa();

                pessoa.Nome = edtNome.Text;
                pessoa.DataCadastro = DateTime.Now;
                pessoa.RazaoSocial = edtRazao.Text;
                pessoa.Rg = edtRG.Text;
                pessoa.CpfCnpj = edtCPF.Text;
                pessoa.Telefone = edtTelefone.Text;
                pessoa.Celular = edtCelular.Text;
                pessoa.Email = edtEmail.Text;
                pessoa.Observacao = null;
                pessoa.Inativo = "N";
                pessoa.Cep = edtCEP.Text;
                pessoa.Logradouro = edtLogradouro.Text;
                pessoa.Bairro = edtBairro.Text;
                pessoa.Cidade = edtCidade.Text;
                pessoa.Estado = "GO";

                if (comboTipoCadastro.SelectedIndex < 0)
                {
                    MessageBox.Show("Informar o tipo de cadastro: ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboTipoCadastro.Focus();
                    return;
                }

                if (comboTipoCadastro.SelectedItem.ToString() == "Cliente")
                {
                    pessoa.TipoCadastro = TipoCadastro.Cliente;
                }
                if (comboTipoCadastro.SelectedItem.ToString() == "Fornecedor")
                {
                    pessoa.TipoCadastro = TipoCadastro.Fornecedor;
                }
                if (comboTipoCadastro.SelectedItem.ToString() == "Funcionario")
                {
                    pessoa.TipoCadastro = TipoCadastro.Funcionario;
                }

                switch (comboTipoCadastro.SelectedIndex)
                {
                    case 0: pessoa.TipoCadastro = TipoCadastro.Cliente;break;
                    case 1: pessoa.TipoCadastro = TipoCadastro.Fornecedor; break;
                    case 2: pessoa.TipoCadastro = TipoCadastro.Funcionario; break;
                    case 3: pessoa.TipoCadastro = TipoCadastro.Terceirizado; break;
                }

                switch (cbTipoPessoa.SelectedIndex)
                {
                    case 0: pessoa.Tipopessoa = TipoPessoa.Fisica; break;
                    case 1: pessoa.Tipopessoa = TipoPessoa.Juridica; break;
                }

                switch (cbCargo.SelectedIndex)
                {
                    case 0: pessoa.Cargo = Cargo.EncarregadoGeral; break;
                    case 1: pessoa.Cargo = Cargo.Encarregado; break;
                    case 2: pessoa.Cargo = Cargo.Pedreiro; break;
                    case 3: pessoa.Cargo = Cargo.Servente; break;
                    case 4: pessoa.Cargo = Cargo.Pintor; break;
                    case 5: pessoa.Cargo = Cargo.AssentadorPiso ; break;
                    case 6: pessoa.Cargo = Cargo.Calheiro; break;
                    case 7: pessoa.Cargo = Cargo.Gesseiro; break;
                    case 8: pessoa.Cargo = Cargo.FuradorFossa; break;
                    case 9: pessoa.Cargo = Cargo.Motorista; break;
                    case 10: pessoa.Cargo = Cargo.Encanador; break;
                    case 11: pessoa.Cargo = Cargo.AuxiliarEncanador; break;
                    case 12: pessoa.Cargo = Cargo.Secretaria; break;
                    case 13: pessoa.Cargo = Cargo.Estagiaria; break;
                }

                switch (cbTipoRemuneracao.SelectedIndex)
                {
                    case 0: pessoa.TipoRemuneracao = TipoRemuneracao.Fixa; break;
                    case 1: pessoa.TipoRemuneracao = TipoRemuneracao.Obra; break;
                    case 2: pessoa.TipoRemuneracao = TipoRemuneracao.Metro2; break;
                    case 3: pessoa.TipoRemuneracao = TipoRemuneracao.Dia ; break;
                }  

                pessoa.Remuneracao = Convert.ToDouble(Equals(edtValorRemuneracao.Text,0));

                PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoa);

                if (gStatusTela == StatusTela.Incluir)
                {
                    foreach (DataRow item in ProximoCodigoPessoa().Rows)
                    {
                        pessoa.Codigo = Convert.ToInt16(item["CODIGO"]);
                    }
                    pessoaNegocio.Gravar();
                    MessageBox.Show(string.Format("{0} {1} gravado com sucesso! ", pessoa.TipoCadastro, pessoa.Nome));
                    visualizacao(pessoa, true);
                }
                if (gStatusTela == StatusTela.Editar)
                {
                    pessoa.Codigo = Convert.ToInt16(edtCodigo.Text);
                    pessoaNegocio.Alterar();
                    MessageBox.Show(string.Format("{0} {1} Alterado com sucesso! ", pessoa.TipoCadastro, pessoa.Nome));
                    visualizacao(pessoa, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na gravação do registro!" + Environment.NewLine + ex.Message + " - " + ex.StackTrace);
            }
        }

        private DataTable ProximoCodigoPessoa()
        {
            PessoaNegocio pessoaNegocio = new PessoaNegocio(null);
            return pessoaNegocio.ProximoCodigo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarCliente();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro selecionado?", "Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    Pessoa pessoaSelecionada = new Pessoa();
                    pessoaSelecionada.Codigo = Convert.ToInt32(edtCodigo.Text);

                    PessoaNegocio pessoaNegocio = new PessoaNegocio(pessoaSelecionada);
                    pessoaNegocio.Excluir();

                    MessageBox.Show("Registro excluído com sucesso!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Não foi possível Excluir.Detalhes: ", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void comboTipoCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTipoCadastro.SelectedItem.ToString() == "Cliente")
            {
                edtRazao.Enabled = false;
                cbCargo.Enabled = false;
                cbTipoRemuneracao.Enabled = false;
                edtValorRemuneracao.Enabled = false;
                cbTipoRemuneracao.SelectedIndex = -1;
            }
            if (comboTipoCadastro.SelectedItem.ToString() == "Fornecedor")
            {
                edtRazao.Enabled = true;
                cbCargo.Enabled = false;
                cbTipoRemuneracao.Enabled = false;
                edtValorRemuneracao.Enabled = false;
                cbTipoRemuneracao.SelectedIndex = -1;
            }
            if (comboTipoCadastro.SelectedItem.ToString() == "Funcionario")
            {
                edtRazao.Enabled = false;
                cbCargo.Enabled = true;
                cbTipoRemuneracao.SelectedIndex = 0;
                edtValorRemuneracao.Enabled = true;
                cbTipoRemuneracao.Enabled = false;
            }
            if (comboTipoCadastro.SelectedItem.ToString() == "Terceirizado")
            {
                edtRazao.Enabled = false;
                cbCargo.Enabled = true;
                cbTipoRemuneracao.Enabled = true;
                edtValorRemuneracao.Enabled = true;
            }
        }

        private void cbTipoPessoa_TextChanged(object sender, EventArgs e)
        {
            if (cbTipoPessoa.Text == "Fisica")
            {
                edtCPF.Mask = "000,000,000-00";
                edtCPFCNPJ.Text = "CPF";
            }
            if (cbTipoPessoa.Text == "Juridica")
            {
                edtCPFCNPJ.Text = "CNPJ";
                edtCPF.Mask = "00,000,000/0000-00";
            }
        }

        private void FrmCadastroPessoa_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            toolStripStatusLabel1.Text = "Versão: " + version.ToString();
        }
    }
}
