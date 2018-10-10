using System.ComponentModel;

namespace CovadisDashboard.Models
{
    public class ElementModel
    {
        [DisplayName("Id: ")]
        public int Id { get; set; }
        [DisplayName("Name: ")]
        public string Name { get; set; }
        [DisplayName("Status: ")]
        public string Status { get; set; }
        [DisplayName("Website: ")]
        public string Website { get; set; }
    }
}
