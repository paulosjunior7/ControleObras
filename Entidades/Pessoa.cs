using ControleObras.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    public class Pessoa
    {
        private int codigo;
        private string nome;
        private string razaoSocial;
        private DateTime dataCadastro;
        private string rg;
        private string cpfCnpj;
        private string email;
        private string telefone;
        private string celular;
        private TipoPessoa tipoPessoa;
        private TipoCadastro tipoCadastro;
        private string observacao;
        private string inativo;
        private string cep;
        private string logradouro;
        private string bairro;
        private string cidade;
        private string estado;
        private Double remuneracao;
        private Double tempo;
        private TipoRemuneracao tipoRemuneracao;
        private Cargo cargo;
        
        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        public DateTime DataCadastro { get => dataCadastro; set => dataCadastro = value; }
        public string Rg { get => rg; set => rg = value; }
        public string CpfCnpj { get => cpfCnpj; set => cpfCnpj = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Celular { get => celular; set => celular = value; }
        public TipoCadastro TipoCadastro { get => tipoCadastro; set => tipoCadastro = value; }
        public TipoPessoa Tipopessoa { get => tipoPessoa; set => tipoPessoa = value; }
        public string Observacao { get => observacao; set => observacao = value; }
        public string Inativo { get => inativo; set => inativo = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Estado { get => estado; set => estado = value; }
        public double Remuneracao { get => remuneracao; set => remuneracao = value; }
        public TipoRemuneracao TipoRemuneracao { get => tipoRemuneracao; set => tipoRemuneracao = value; }
        public Cargo Cargo { get => cargo; set => cargo = value; }
        public double Tempo { get => tempo; set => tempo = value; }

    }
}
