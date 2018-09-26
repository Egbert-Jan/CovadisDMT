using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {
        [Required(ErrorMessage = "Please fill out a name for this website configuration.")]
        [DisplayName("Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A url is required! You twat!")]
        [DisplayName("Url:")]
        public string Url { get; set; }

        [Required(ErrorMessage = "All elements are required!")]
        [DisplayName("Element 1:")]
        public string Element1 { get; set; }

        [Required(ErrorMessage = "All elements are required!")]
        [DisplayName("Element 2:")]
        public string Element2 { get; set; }

        [Required(ErrorMessage = "All elements are required!")]
        [DisplayName("Element 3:")]
        public string Element3 { get; set; }
    }
}
