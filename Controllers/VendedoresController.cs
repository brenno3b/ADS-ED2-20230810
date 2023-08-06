using ADS_ED2_20230810.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED2_20230810.Controllers
{
    internal class VendedoresController
    {
        private static readonly int _max = 2;

        private VendedorController[] _osVendedores;
        private int _qtde;

        public VendedorController[] OsVendedores { get { return _osVendedores; } }
        public int Qtde { get { return _qtde; } }

        public VendedoresController()
        {
            _qtde = 0;
            _osVendedores = new VendedorController[_max];
            for (int i = 0; i < _max; i++)
            {
                _osVendedores[i] = new VendedorController();
            }
        }

        public bool AddVendedor(VendedorController vendedor)
        {
            if (_qtde < _max)
            {
                int index = 0;

                while (index < _max && _osVendedores[index].Id != -1)
                {
                    index++;
                }

                if (index < _max)
                {
                    VendedorController newVendedor = new VendedorController(index + 1, vendedor.Nome, vendedor.PercComissao);

                    _osVendedores[index] = newVendedor;
                    _qtde++;
                    return true;
                }
            }

            return false;
        }

        public bool DelVendedor(VendedorController vendedor)
        {
            int index = IndexOf(vendedor);

            if (index > -1 && vendedor.Id > 0)
            {
                VendedorController foundVendedor = _osVendedores[index];
                bool hasVenda = false;

                for (int i = 0; i < foundVendedor.AsVendas.Length; i++)
                {
                    Console.WriteLine($"DEBUG: {foundVendedor.AsVendas[i].Qtde > 0}");

                    if (foundVendedor.AsVendas[i].Qtde > 0) { 
                        hasVenda = true; 
                        break; 
                    }
                }

                Console.WriteLine($"DEBUG - hasVenda: {hasVenda}");

                if (!hasVenda)
                {
                    _osVendedores[index] = new VendedorController();
                    _qtde--;
                    return true;
                }
            }

            return false;
        }

        public VendedorController SearchVendedor(VendedorController vendedor)
        {
            int index = IndexOf(vendedor);

            if (index > -1) return _osVendedores[index];

            return new VendedorController();
        }

        public double ValorVendas()
        {
            double sum = 0.0;

            for (int i = 0; i < _max; i++)
            {
                sum += _osVendedores[i].ValorVendas();
            }

            return sum;
        }

        public double ValorComissao()
        {
            double sum = 0.0;

            for (int i = 0; i < _max; i++)
            {
                sum += _osVendedores[i].ValorComissao();
            }

            return sum;
        }
        private int IndexOf(VendedorController vendedor)
        {
            int i = 0;

            while (i < _max && _osVendedores[i].Id != vendedor.Id)
            {
                i++;
            }

            if (i < _max) return i;

            return -1;
        }
    }
}
