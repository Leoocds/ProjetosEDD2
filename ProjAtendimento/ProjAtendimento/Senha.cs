using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAtendimento
{
    public class Senha
    {
        public int Id { get; private set; }
        public DateTime DataGeracao { get; private set; }
        public DateTime HoraGeracao { get; private set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? HoraAtendimento { get; set; }

        public Senha(int id)
        {
            Id = id;
            DataGeracao = DateTime.Now;
            HoraGeracao = DateTime.Now;
        }

        public string DadosParciais()
        {
            return $"{Id} - {DataGeracao:dd/MM/yyyy} - {HoraGeracao:HH:mm:ss}";
        }

        public string DadosCompletos()
        {
            return $"{Id} - {DataGeracao:dd/MM/yyyy} - {HoraGeracao:HH:mm:ss}" +
                   $" - {DataAtendimento:dd/MM/yyyy} - {HoraAtendimento:HH:mm:ss}";
        }
    }
}
