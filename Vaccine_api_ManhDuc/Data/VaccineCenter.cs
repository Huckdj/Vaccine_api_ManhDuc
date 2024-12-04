namespace Vaccine_api_ManhDuc.Data
{
    public static class VaccineCenter
    {
        public class insertcenter
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Ward { get; set; }
            public string District { get; set; }

            public string City { get; set; }
            public string LinkGoogle { get; set; }
        }

        public class EditCenter
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Ward { get; set; }
            public string District { get; set; }
            public string City { get; set; }
            public string LinkGoogle { get; set; }
        }

        public class DeleteCenterr
        {
            public int ID { get; set; }
        }
    }
}
