using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAtendimento
{
    public class Senhas
    {
        private int proximoAtendimento = 1;
        public Queue<Senha> FilaSenhas { get; private set; }

        public Senhas()
        {
            FilaSenhas = new Queue<Senha>();
        }

        public void Gerar()
        {
            FilaSenhas.Enqueue(new Senha(proximoAtendimento));
            proximoAtendimento++;
        }
    }

}
