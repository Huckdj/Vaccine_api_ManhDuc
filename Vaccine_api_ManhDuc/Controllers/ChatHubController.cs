using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class ChatHub : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[InsertMessage]";

        public ChatHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> PushSender([FromBody] Chat.sender request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-MESSAGE" },
                    { "Sender", request.Sender },
                    { "Receiver", request.Receiver },
                    { "MessageContent", request.MessageContent}
                };


                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetChat([FromBody] Chat.sender request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET" },
                    { "Sender", request.Sender },
                    { "Receiver", request.Receiver }
                };


                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAdminAll([FromBody] Chat.sender request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-ADMIN-ALL" },
                    { "Receiver", request.Receiver }
                };


                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
