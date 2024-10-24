namespace Vaccine_api_ManhDuc.Data
{
    public class DataResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public dynamic Data { get; set; }

        public DataResponse(string message = "", object data = null, string status = "0") // Change to object
        {
            Message = message;
            Data = data;
            Status = status;
        }
    }

}
