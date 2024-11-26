namespace Vaccine_api_ManhDuc.Data
{
    public static class BannerPublics
    {    
        public class AddBanner
        {
            public string PosText { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string LinkImages { get; set; }
        }

        public class AddPositonBanner
        {
            public string Name { get; set; }
            public string PositionCode { get; set; }
        }
        public class EditPosition
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string PositionCode { get; set; }
        }
        public class DeletePositon
        {
            public int ID { get; set; }
        }
        public class DeleteBanner
        {
            public int ID { get; set; }
        }
        public class UpDateBanner
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string PosText { get; set; }
            public string Description { get; set; }
            public string LinkImages { get; set; }
        }
        public class GetByPosText
        {
            public string PosText { get; set; }
        }
    }
}
