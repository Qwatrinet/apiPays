using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPays.Models
{
    [Table("ville")]
    [Index(nameof(CodePostal), nameof(NomVille), nameof(PaysId), IsUnique = true)]
    public class Ville
    {
        public int Id { get; set; }

        [Column("code_postal")]
        [Length(4, 6, ErrorMessage = "Un code postal contient entre 4 et 6 caractères")]
        [Required]
        public string CodePostal { get; set; }

        [Column("nom_ville")]
        [Required]
        [MaxLength(60)]
        public string NomVille { get; set; }

        [ForeignKey("Pays")]
        [Required]
        public int PaysId { get; set; }

        [JsonIgnore]
        public Pays? Pays { get; set; }

        public Ville()
        {
        }


    }
}
