using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPays.Models
{
    [PrimaryKey(nameof(PersonneId), nameof(TrajetId))]
    public class PersonneTrajet
    {
        [ForeignKey("Personne")]
        public int PersonneId { get; set; }

        [JsonIgnore]
        public Personne? Personne { get; set; }

        public bool estConducteur { get; set; }

        [ForeignKey("Trajet")]
        public int TrajetId { get; set; }

        [JsonIgnore]
        public Trajet? Trajet { get; set; }
    }
}
