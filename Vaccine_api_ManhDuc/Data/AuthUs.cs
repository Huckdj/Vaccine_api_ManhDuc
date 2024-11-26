namespace Vaccine_api_ManhDuc.Data
{
    public static class AuthUs
    {
        public class login
        {
            public string UserAuth { get; set; }
            public string Password { get; set; }
        }
        public class Register
        {
            public string Email { get; set; }
            public string SDT { get; set; }
            public string Password { get; set; }
        }
    }
}
