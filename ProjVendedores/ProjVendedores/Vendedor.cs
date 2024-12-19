using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjVendedores
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double PercComissao { get; set; }
        public Venda[] AsVendas { get; set; }

        public Vendedor(int id, string nome, double percComissao)
        {
            Id = id;
            Nome = nome;
            PercComissao = percComissao;
            AsVendas = new Venda[31]; // Um mês de vendas
        }

        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia < 1 || dia > 31)
            {
                throw new ArgumentException("O dia deve estar entre 1 e 31.");
            }

            AsVendas[dia - 1] = venda;
        }

        public double ValorVendas()
        {
            return AsVendas.Where(v => v != null).Sum(v => v.Valor);
        }

        public double ValorComissao()
        {
            return ValorVendas() * PercComissao;
        }

        public double ValorMedioVendas()
        {
            var vendas = AsVendas.Where(v => v != null);
            return vendas.Any() ? vendas.Average(v => v.Valor) : 0;
        }
    }
}
