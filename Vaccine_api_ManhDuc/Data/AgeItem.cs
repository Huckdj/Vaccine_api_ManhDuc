namespace Vaccine_api_ManhDuc.Data
{
    public static class AgeItem
    {
        public class InsertAge
        {
            public string NameYearOld { get; set; }
        }
        public class UpdateAge
        {
            public int ID { get; set; }
        }

        public class EditByAgeID
        {
            public int ID { get; set; }
            public string NameYearOld { get; set; }
        }

        public class SetActiveAge
        {
            public int ID { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
