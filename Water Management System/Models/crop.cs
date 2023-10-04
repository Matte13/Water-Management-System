using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // crop Model for cropes table
    public class crop
    {
        //Primery Key
        [Key]
        public int id { get; set; }
        //Crop name
        public string Name { get; set; }
        //Company Id
        public int Companyid { get; set; }
       
        //Water Id
        public int waterid { get; set; }
        
        // Quantity
        public string  Quantity { get; set; }
        
        //Irrigation
        public string Irrigation { get; set;}
        
        //Humidity
        public int Humidity { get; set; }
        
        //Hectars
        public int Hectars { get; set; }
        
        //water
        public int water { get; set; }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
}
