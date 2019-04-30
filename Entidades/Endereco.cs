using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    class Endereco
    {
        private string CEP;
        private string logradouro;
        private string bairro;
        private string cidade;
        private string estado;

        public string CEP1 { get => CEP; set => CEP = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
