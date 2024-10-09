namespace Vaccine_api_ManhDuc.Data
{
    public class DataResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }  // Change this to object
        public string Status { get; set; }

        public DataResponse(string message = "", object data = null, string status = "0") // Change to object
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }
}
