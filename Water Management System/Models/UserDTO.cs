namespace WebApplication1.Models
{
    // This Model is used to pass values to Actions, No table for this

    public class UserDTO
    {
        //User ID
        public int Userid { get; set; }
        
        //User name
        public string username { get; set; }
        
        //Company Name
        public string CompanyName { get; set; }
        
        //Water amount
        public int? water { get; set; }
        
        //Approved or Not
        public int isApproved { get; set; }
    }
}
