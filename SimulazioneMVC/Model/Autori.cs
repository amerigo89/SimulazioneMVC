using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulazioneMVC.Model
{
    public partial class Autori
    {
        public Autori()
        {
            Registrazioni = new HashSet<Registrazioni>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public string Skills { get; set; }

        [InverseProperty("IdAutoreNavigation")]
        public ICollection<Registrazioni> Registrazioni { get; set; }
    }
}
