using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    public class Produto
    {
        int codigo;
        string descricao;
        Grupos grupo;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public Grupos Grupo { get => grupo; set => grupo = value; }
    }
}
