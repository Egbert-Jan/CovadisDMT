using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        [DisplayName("Url:")]
        public string Url { get; set; }

        public List<ElementModel> Elements { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Url, Elements);
        }
    }
}
