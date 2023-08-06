using ADS_ED2_20230810.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED2_20230810.Controllers
{
    internal class VendedorController
    {
        private int _id;
        private string _nome;
        private double _percComissao;
        private VendaModel[] _asVendas;
        private static readonly int _max = 31;

        public int Id { get { return _id; } }
        public string Nome { get { return _nome; } }
        public double PercComissao { get {  return _percComissao; } set { _percComissao = value; } }
        public VendaModel[] AsVendas { get { return _asVendas;} }

        public VendedorController(int id, string nome, double percComissao)
        {
            _id = id;
            _nome = nome;
            _percComissao = percComissao;
            _asVendas = new VendaModel[_max];
            for (int i = 0; i < _max; i++)
            {
                _asVendas[i] = new VendaModel();
            }
        }

        public VendedorController(string nome, double percComissao)
        {
            _id = -1;
            _nome = nome;
            _percComissao = percComissao;
            _asVendas = new VendaModel[_max];
            for (int i = 0; i < _max; i++)
            {
                _asVendas[i] = new VendaModel();
            }
        }

        public VendedorController(int id)
        {
            _id = id;
            _nome = "";
            _percComissao = 0.0;
            _asVendas = new VendaModel[_max];
            for (int i = 0; i < _max; i++)
            {
                _asVendas[i] = new VendaModel();
            }
        }

        public VendedorController()
        {
            _id = -1;
            _nome = "";
            _percComissao = 0.0;
            _asVendas = new VendaModel[_max];
            for (int i = 0; i < _max; i++)
            {
                _asVendas[i] = new VendaModel();
            }
        }

        public void RegistrarVenda(int dia, VendaModel venda)
        {
            if (dia < 0 || dia > _max) return;

            if (venda.Qtde < 1 || venda.Valor <= 0.0) return;

            _asVendas[dia] = venda;
        }

        public double ValorVendas()
        {
            double sum = 0.0;

            for(int i = 0; i < _max; i++)
            {
                sum += _asVendas[i].Valor;
            }

            return sum;
        }

        public double ValorComissao()
        {
            return ValorVendas() * _percComissao;
        }

        public override string? ToString()
        {
            return "VendedorController{" +
                "Id: " + _id + "," +
                "Nome: " + _nome + "," +
                "PercComissao: " + _percComissao + "," +
                "AsVendas: " + _asVendas +
                "}";
        }
    }
}
