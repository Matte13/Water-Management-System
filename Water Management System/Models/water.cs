using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // water Model for water table
    public class water
    {
        //Primery Key
        [Key]
        public int id { get; set; }
        
        //Company id
        public int Companyid { get; set; }
        
        //Crop id
        public int cropid { get; set; }
        
        //Water Used
        public int WaterUsed { get; set; }
        
        //water Addigned
        public int WaterAssigned { get; set; }
        
        //Entered Date
        public DateTime Date { get; set; }

    


    }
}
