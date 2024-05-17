using System.ComponentModel.DataAnnotations;

namespace MvcWebApplication.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseTime { get; set; }
        public string? Description { get; set; }
        public Double Price { get; set; }
    }
}
