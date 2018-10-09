using System.ComponentModel;

namespace CovadisDashboard.Models
{
    public class ApiModel
    {
        public int Id { get; set; }

        [DisplayName("Name: ")]
        public string Name { get; set; }

        [DisplayName("Url: ")]
        public string Url { get; set; }
    }
}
