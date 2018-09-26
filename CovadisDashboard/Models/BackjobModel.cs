
using System.ComponentModel;

namespace CovadisDashboard.Models
{
    public class BackjobModel
    {
        [DisplayName("Name: ")]
        public string Name { get; set; }

        [DisplayName("Increment: ")]
        public int Increment { get; set; }
    }
}
