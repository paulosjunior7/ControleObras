using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleObras.Entidades
{
    public class Terceirizado
    {
        int codigoObra;
        Pessoa funcTerceirizado;
        Double custoTotal;

        public int CodigoObra { get => codigoObra; set => codigoObra = value; }
        public double CustoTotal { get => custoTotal; set => custoTotal = value; }
        public Pessoa FuncTerceirizado { get => funcTerceirizado; set => funcTerceirizado = value; }

        public void AtualizaCusto(Double m2)
        {
            this.custoTotal = Math.Round(funcTerceirizado.Remuneracao * m2);
        }
    }
}
