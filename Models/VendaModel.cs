using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED2_20230810.Models
{
    internal class VendaModel
    {
        private int _qtde;
        private double _valor;

        public int Qtde { get { return _qtde; } set { _qtde = value; } }
        public double Valor { get { return _valor; } set { _valor = value; } }

        public VendaModel(int qntd, double valor)
        {
            _qtde = qntd;
            _valor = valor;
        }

        public VendaModel() {
            _qtde = 0;
            _valor = 0.0;
        }

        public double ValorMedio()
        {
            return _valor/_qtde;
        }

        public override bool Equals(object? obj)
        {
            return obj is VendaModel model &&
                   _qtde == model._qtde &&
                   _valor == model._valor;
        }

        public override string? ToString()
        {
            return "VendaModel{" +
                "Qntd: " + _qtde + "," +
                "Valor: " + _valor +
                "}";
        }
    }
}
