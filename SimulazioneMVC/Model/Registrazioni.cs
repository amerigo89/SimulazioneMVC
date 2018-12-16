using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulazioneMVC.Model
{
    public partial class Registrazioni
    {
        public int IdPresentazione { get; set; }
        public int IdAutore { get; set; }

        [ForeignKey("IdAutore")]
        [InverseProperty("Registrazioni")]
        public Autori IdAutoreNavigation { get; set; }
        [ForeignKey("IdPresentazione")]
        [InverseProperty("Registrazioni")]
        public Presentazioni IdPresentazioneNavigation { get; set; }
    }
}
