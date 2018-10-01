using System.Collections.Generic;
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
        
        public List<string> Elements { get; set; }
    }
}
