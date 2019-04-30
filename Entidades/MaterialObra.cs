using System;

namespace ControleObras.Entidades
{
    public class MaterialObra
    {
        int codigoObra;
        Produto produto;
        string unidade;
        double quantidade;
        double precoUnitario;
        DateTime dataCompra;
        Pessoa fornecedor;
        string formaPagamento;
        double valorTotalItem;

        public string Unidade { get => unidade; set => unidade = value; }
        public double Quantidade { get => quantidade; set => quantidade = value; }
        public DateTime DataCompra { get => dataCompra; set => dataCompra = value; }
        public string FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        public double ValorTotalItem { get => valorTotalItem; set => valorTotalItem = value; }
        public int CodigoObra { get => codigoObra; set => codigoObra = value; }
        public Pessoa Fornecedor { get => fornecedor; set => fornecedor = value; }
        public Produto Produto { get => produto; set => produto = value; }
        public double PrecoUnitario { get => precoUnitario; set => precoUnitario = value; }

    }
}
