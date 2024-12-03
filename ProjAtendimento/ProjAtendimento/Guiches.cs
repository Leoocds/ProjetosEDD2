using System.Collections.Generic;
using ProjAtendimento;


namespace ProjAtendimento
{
    public class Guiches
    {
        public List<Guiche> ListaGuiches { get; private set; }

        public Guiches()
        {
            ListaGuiches = new List<Guiche>();
        }

        public void Adicionar(Guiche guiche)
        {
            ListaGuiches.Add(guiche);
        }
    }

}