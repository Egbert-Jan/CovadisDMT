using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {
        //[Required(ErrorMessage = "Please fill out a name for this website configuration.")]
        //[DisplayName("Name:")]
        //public string Name { get; set; }

        public int websiteID { get; set; }

        [DisplayName("Url:")]
        public string url { get; set; }

        public List<ElementModel> elementen { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", url, elementen);
        }
    }
}
