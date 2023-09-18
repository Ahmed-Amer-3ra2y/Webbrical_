namespace ECommerce.Helpers
{
    public class JWTData
    {

            //define properties same to which defined in json file

            public string Key { get; set; }

            public string Issuer { get; set; }
        
            public string Audience { get; set; }
            
            public double DurationInDays { get; set; }
        
    }
}
