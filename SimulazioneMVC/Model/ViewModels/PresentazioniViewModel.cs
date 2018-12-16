using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulazioneMVC.Model.ViewModels
{
    public class PresentazioniViewModel
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public int Livello { get; set; }

        public IEnumerable<int> IdAutore { get; set; }

        public List<Autori> Autori = new List<Autori>();
    }
}
