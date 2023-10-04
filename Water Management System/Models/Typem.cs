using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // type Model for Types table
    public class Typem
    {
        //Primery Key
        [Key]
        public int id { get; set; }
        
        //Type Name
        public string Name { get; set; }
    }

    
  
}
