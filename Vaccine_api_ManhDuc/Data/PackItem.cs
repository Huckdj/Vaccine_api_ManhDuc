namespace Vaccine_api_ManhDuc.Data
{
    public static class PackItem
    {
        public class InsertItem
        {
            public string Name { get; set; }         
            public int Price { get; set; }     
            public string TypePack { get; set; } 
            public string ShortContent { get; set; } 
            public string FullContent { get; set; } 
            public string LinkImages { get; set; }  
            public string LinkRoute { get; set; }  
        }
        public class EditPackItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string TypePack { get; set; }
            public string ShortContent { get; set; }
            public string FullContent { get; set; }
            public string LinkImages { get; set; }
            public string LinkRoute { get; set; }
        }

        public class DeletePackItem
        {
            public int ID { get; set; }
        }

        public class ActivePackItem
        {
            public int ID { get; set; }
            public bool IsActive { get; set; }
        }
        public class centerID
        {
            public int ID { get; set; }
        }

    }
}
