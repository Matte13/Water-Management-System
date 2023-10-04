using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    // waterrequest Model for waterrequests table
    public class waterrequest
    {
        //Primery Key
       [Key]
        public int id { get; set; }
       
        //Date
        public DateTime Date { get; set; }
       
        //Company id
        public int    companyid { get; set; }
        
        //water reqtesed amount
        public int waterreqtesed { get; set; }
        
        //Status for Approved or Rejected
        public int Status { get; set; }

        [NotMapped]
        //Company Name
        public string? Compname { get; set; }

    }
}
