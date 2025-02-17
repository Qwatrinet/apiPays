using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPays.Models
{
    public class PaysLike
    {
        public string country_code { get; set; }
        public string country_name { get; set; }
    }

    [Table("pays")]
    [Index(nameof(CodePays), IsUnique = true)]
    [Index(nameof(NomPays), IsUnique = true)]
    public class Pays
    {
        public int PaysId { get; set; }

        [Column("code_pays", TypeName = "char")]
        [StringLength(2)]
        //[Length(2, 2, ErrorMessage = "Le code doit être composé de strictement 2 lettres")]
        [Required]
        public string CodePays { get; set; }

        [Column("nom_pays")]
        [MaxLength(255)]
        [Required]
        public string NomPays { get; set; }

        [JsonIgnore]
        public ICollection<Ville> Villes { get; init; }

        public Pays()
        {
            this.Villes = new List<Ville>();
        }
    }
}
