using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class company
    {
        // Company Model for Companies table

        //Primery Key
        [Key]
        public int id { get; set; }
        //Company Name
        public string name { get; set; }
        //Allocated water
        public int water { get; set; }
    }
}
