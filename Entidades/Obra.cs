using ControleObras.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControleObras.Entidades;
using ControleObras.Negocio;

namespace ControleObras.Entidades
{
    public class Obra
    {
        int codigoObra;
        Pessoa pessoa;
        DateTime dataCadastro;
        StatusObra statusObra;
        string cep;
        string logradouro;
        string unidades;
        double areaUnidade;
        double areaLote;
        double areaTotal;
        string inscricaoMunicipal;
        string numeroAlvara;
        string usodesolo;
        string matriculaMae;
        string art;
        double valorTotal;
        List<MaterialObra> listaMaterialObra;
        List<EquipeObra> listaEquipeObra;
        List<Terceirizado> listaEquipeTerceirizado;

        public int CodigoObra { get => codigoObra; set => codigoObra = value; }
        public DateTime DataCadastro { get => dataCadastro; set => dataCadastro = value; }
        public StatusObra StatusObra { get => statusObra; set => statusObra = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Unidades { get => unidades; set => unidades = value; }
        public double AreaUnidade { get => areaUnidade; set => areaUnidade = value; }
        public double AreaTotal { get => areaTotal; set => areaTotal = value; }
        public string InscricaoMunicipal { get => inscricaoMunicipal; set => inscricaoMunicipal = value; }
        public string NumeroAlvara { get => numeroAlvara; set => numeroAlvara = value; }
        public string Usodesolo { get => usodesolo; set => usodesolo = value; }
        public string MatriculaMae { get => matriculaMae; set => matriculaMae = value; }
        public double ValorTotal { get => valorTotal; set => valorTotal = value; }
        public double AreaLote { get => areaLote; set => areaLote = value; }
        public string Art { get => art; set => art = value; }
        public Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        public List<MaterialObra> ListamaterialObras { get => listaMaterialObra; set => listaMaterialObra = value; }
        public List<EquipeObra> ListaEquipeObra { get => listaEquipeObra; set => listaEquipeObra = value; }
        public List<Terceirizado> ListaEquipeTerceirizado { get => listaEquipeTerceirizado; set => listaEquipeTerceirizado = value; }

        public void AdicionarMaterial(MaterialObra materialObra)
        {
            ListamaterialObras.Add(materialObra);
        }

        public void RemoverMaterial(MaterialObra materialObra)
        {
            ListamaterialObras.Remove(materialObra);
        }

        public void AdicionarEquipeObra(EquipeObra equipeObra)
        {
            listaEquipeObra.Add(equipeObra);
        }

        public void RemoverEquipeObra(EquipeObra equipeObra)
        {
            listaEquipeObra.Remove(equipeObra);
        }

        public void AdicionarTerceirizado(Terceirizado terceirizado)
        {
            ListaEquipeTerceirizado.Add(terceirizado);
        }

        public void RemoverTerceirizado(Terceirizado terceirizado)
        {
            ListaEquipeTerceirizado.Remove(terceirizado);
        }

        public Obra()
        {
            listaMaterialObra = new List<MaterialObra>();
            ListaEquipeObra = new List<EquipeObra>();
            ListaEquipeTerceirizado = new List<Terceirizado>();
        }
    }
}
