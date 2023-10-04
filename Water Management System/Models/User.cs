using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // User Model for Users table
    public class User
    {
        //Primery Key
        [Key]
        public int id { get; set; }
        
        //User name
        public string username { get; set; }
        
        //Company ID
        public int companyid { get; set; }
        
        //Type
        public string type { get; set; }
        
        //IsApproved
        public int isApproved { get; set; }

   
    }
}
