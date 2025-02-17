using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiPays.Models
{
    public class Personne
    {
        public int Id { get; init; }
        [MaxLength(60)]
        [Required]
        public string Nom { get; set; }
        [MaxLength(60)]
        [Required]
        public string Prenom { get; set; }

        [JsonIgnore]
        public List<PersonneTrajet> PersonneTrajets { get; } = [];
    }
}
