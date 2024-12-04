namespace Vaccine_api_ManhDuc.Data
{
    public static class PostPublics
    {
        public class AddPost
        {

            public string Title { get; set; }
            public string ShortContent { get; set; }
            public string FullContentDesktop { get; set; }
            public string FullContentMobile { get; set; }
            public string LinkImages { get; set; }
            public string LinkRoute { get; set; }
            public string PostType { get; set; }
        }

        public class AddPostType
        {
            public string NameType { get; set; }
        }
        public class EditPostType
        {
            public int ID { get; set; }
            public string NameType { get; set; }
        }

        public class DeletePostType
        {
            public int ID { get; set; }
        }

        public class UpdatePost
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string ShortContent { get; set; }
            public string FullContentDesktop { get; set; }
            public string FullContentMobile { get; set; }
            public string LinkImages { get; set; }
            public string LinkRoute { get; set; }
            public string PostType { get; set; }
        }

        public class GetPostByID
        {
            public int ID { get; set; }
        }

        public class GetByLinkRoute
        {
            public string LinkRoute { get; set; }
        }
        public class SearchKeywords
        {
            public string SearchKeyword { get; set; }
        }
    }
}
