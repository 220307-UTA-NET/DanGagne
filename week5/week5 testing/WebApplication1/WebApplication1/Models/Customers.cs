using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Customers
    {

        [Key]
        public int custID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public int storeID { get; set; }
    }
}
