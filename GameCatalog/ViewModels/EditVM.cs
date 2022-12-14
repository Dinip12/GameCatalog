using System.ComponentModel.DataAnnotations;

namespace GameCatalog.ViewModels
{
    public class EditVM
    {
        public int ID { get; set; }

        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Title { get; set; }

        [Display(Name = "Desciption: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Description { get; set; }

        [Display(Name = "ReleaseDate: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Stars: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Stars { get; set; }

        [Display(Name = "Downloads: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Downloads { get; set; }
    }
}
