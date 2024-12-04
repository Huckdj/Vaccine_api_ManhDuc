namespace Vaccine_api_ManhDuc.Data
{
    public static class Booking
    {
        public class insertbooking
        {
            public string Name { get; set; }
            public string Birthday { get; set; }
            public string SDTInject { get; set; }
            public string NameContact { get; set; }

            public string DateInject { get; set; }
            public string SDT { get; set; }
            public string Email { get; set; }
            public int IDUser { get; set; }
            public int IDCenter { get; set; }
            public List<VaccineSelection> vaccinepack { get; set; }
            public List<VaccineSelection> vaccinesingle { get; set; }
        }
        public class VaccineSelection
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }
        public class GetdataID
        {
            public int IDUser { get; set; }
        }

        public class updatestatus
        {
            public int ID { get; set; }
            public string NewStatus { get; set; }
        }

    }
}
