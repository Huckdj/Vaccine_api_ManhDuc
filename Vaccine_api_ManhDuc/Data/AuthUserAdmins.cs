namespace Vaccine_api_ManhDuc.Data
{
    public static class AuthUserAdmins
    {
        public class EDitByID
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string SDT { get; set; }
            public int ID { get; set; }
        }

        public class Delete { 

            public int ID { get; set; }
        }
        public class UpdateActive
        {

            public int ID { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
