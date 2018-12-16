using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulazioneMVC.Model
{
    public partial class Presentazioni
    {
        public Presentazioni()
        {
            Registrazioni = new HashSet<Registrazioni>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Titolo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInizio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFine { get; set; }
        public int Livello { get; set; }

        [InverseProperty("IdPresentazioneNavigation")]
        public ICollection<Registrazioni> Registrazioni { get; set; }
    }
}
