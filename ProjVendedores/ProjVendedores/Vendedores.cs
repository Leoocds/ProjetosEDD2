using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjVendedores
{
    public class Vendedores
    {
        public Vendedor[] OsVendedores { get; private set; }
        public int Max { get; private set; }
        public int Qtde { get; private set; }

        public Vendedores(int max = 10)
        {
            Max = max;
            OsVendedores = new Vendedor[max];
            Qtde = 0;
        }

        public bool AddVendedor(Vendedor v)
        {
            if (Qtde >= Max)
            {
                return false;
            }

            OsVendedores[Qtde++] = v;
            return true;
        }

        public bool DelVendedor(Vendedor v)
        {
            var index = Array.IndexOf(OsVendedores, v);
            if (index != -1 && v.ValorVendas() == 0)
            {
                OsVendedores[index] = null;
                Qtde--;
                return true;
            }
            return false;
        }

        public Vendedor SearchVendedor(int id)
        {
            return OsVendedores.FirstOrDefault(v => v != null && v.Id == id);
        }

        public double ValorVendas()
        {
            return OsVendedores.Where(v => v != null).Sum(v => v.ValorVendas());
        }

        public double ValorComissao()
        {
            return OsVendedores.Where(v => v != null).Sum(v => v.ValorComissao());
        }
    }

}
