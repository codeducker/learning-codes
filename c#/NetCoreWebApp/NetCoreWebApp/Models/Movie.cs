using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic.CompilerServices;

namespace NetCoreWebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(60,MinimumLength = 3)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Release Time")]
        public DateTime ReleaseTime { get; set; }

        [Range(0,100)]
        [DataType(DataType.Currency)]
        public Double Price { get; set; }

        public String Description { get; set; }

        [RegularExpression("/^A-Z0-9$/")]
        [StringLength(30)]
        public string Rating { get; set; } = string.Empty;
    }
}
