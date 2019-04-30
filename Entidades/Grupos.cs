using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    public class Grupos
    {
        int codigo;
        string descricao;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
    }
}
