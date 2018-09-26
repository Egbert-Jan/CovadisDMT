using System.ComponentModel;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {
        [DisplayName("Name:")]
        public string Name { get; set; }

        [DisplayName("Url:")]
        public string Url { get; set; }

        [DisplayName("Element 1:")]
        public string Element1 { get; set; }

        [DisplayName("Element 2:")]
        public string Element2 { get; set; }

        [DisplayName("Element 3:")]
        public string Element3 { get; set; }
    }
}
