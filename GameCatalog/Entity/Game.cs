using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalog.Entity
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Stars { get; set; }
        public double Downloads { get; set; }
        public string fileName { get; set; }
    }
}
