namespace Vaccine_api_ManhDuc.Data
{
    public static class SingleItem
    {
        public class InsertSingleItem
        {
            public string Name { get; set; }         
            public int Price { get; set; }     
            public string CountryItem { get; set; } 
            public string ShortContent { get; set; } 
            public string FullContent { get; set; } 
            public string LinkImages { get; set; }  
            public string LinkRoute { get; set; }  
            public string AgeType { get; set; }
        }
        public class EditSingleItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string CountryItem { get; set; }
            public string ShortContent { get; set; }
            public string FullContent { get; set; }
            public string LinkImages { get; set; }
            public string LinkRoute { get; set; }
            public string AgeType { get; set; }
        }

        public class DeleteSingleItem
        {
            public int ID { get; set; }
        }
        public class getroute
        {
            public string LinkRoute { get; set; }
        }

        public class ActiveSingleItem
        {
            public int ID { get; set; }
            public bool IsActive { get; set; }
        }

    }
}
