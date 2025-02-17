using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPays.Models
{
    public class Trajet
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateDepart { get; set; }

        public DateTime? DateArrivee { get; set; }

        [ForeignKey("Ville")]
        [Required]
        public int VilleDepartId { get; set; }

        [JsonIgnore]
        public Ville? VilleDepart { get; set; }

        [ForeignKey("Ville")]
        [Required]
        public int? VilleArriveeId { get; set; }

        [JsonIgnore]
        public Ville? VilleArrivee { get; set; }

        [JsonIgnore]
        public List<PersonneTrajet>? TrajetPersonnes { get; } = [];
    }
}
