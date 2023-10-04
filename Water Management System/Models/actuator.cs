using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // Actuator Model for actuators table
    public class actuator
    {
        [Key]
        //primery key
        public int id { get; set; }
        //company id
        public int Companyid { get; set; }
        //crop id
        public int Cropid { get; set; }
        //type
        public string Type { get; set; }
        //Status
        public string State { get; set; }
        //Mode
        public string Mode { get; set; }
    }
}
