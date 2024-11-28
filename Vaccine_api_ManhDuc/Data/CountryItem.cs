namespace Vaccine_api_ManhDuc.Data
{
    public static class CountryItem
    {
        public class InsertCountry
        {
            public string NameCountry { get; set; }
        }
        public class UpdateCountry
        {
            public int ID { get; set; }
        }

        public class EditByID
        {
            public int ID { get; set; }
            public string NameCountry { get; set; }
        }

        public class SetActive
        {
            public int ID { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
