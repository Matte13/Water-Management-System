using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // Sensor Model for Sensors table
    public class Sensor
    {
        //Primery Key
        [Key]
        public int id { get; set; }
        
        //cropid
        public int CropID { get; set; }
        
        //type
        public string type { get; set; }
    }
}
