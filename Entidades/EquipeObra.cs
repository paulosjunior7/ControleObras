using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    public class EquipeObra
    {
        int codigoObra;
        Pessoa funcionario;
        Double custoTotal;

        public int CodigoObra { get => codigoObra; set => codigoObra = value; }
        public Pessoa Funcionario { get => funcionario; set => funcionario = value; }
        public double CustoTotal { get => custoTotal; set => custoTotal =  value; }

        public void AtualizaCusto()
        {
            this.custoTotal = Math.Round((funcionario.Remuneracao / 30) * funcionario.Tempo , 2);
        }
    }
}
