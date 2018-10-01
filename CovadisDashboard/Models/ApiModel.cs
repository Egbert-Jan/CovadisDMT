using System.ComponentModel;

namespace CovadisDashboard.Models
{
    public class ApiModel
    {
        [DisplayName("Name: ")]
        public string Name { get; set; }

        [DisplayName("Url: ")]
        public string Url { get; set; }
    }
}
