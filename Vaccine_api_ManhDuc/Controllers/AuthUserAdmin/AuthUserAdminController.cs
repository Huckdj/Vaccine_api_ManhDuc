using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class AuthUserAdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[ExecAccount]";

        public AuthUserAdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Get-Data" }
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
        public async Task<IActionResult> EditByID([FromBody]AuthUserAdmins.EDitByID request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EditBy-ID" },
                    { "Email",request.Email},
                    { "ID" , request.ID},
                    { "SDT", request.SDT},
                    { "Password", request.Password}

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
        public async Task<IActionResult> Delete([FromBody] AuthUserAdmins.Delete request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Delete" },
                    { "ID" , request.ID}

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
        public async Task<IActionResult> UpdateActive([FromBody] AuthUserAdmins.UpdateActive request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "UpdateActive" },
                    { "ID" , request.ID},
                    { "IsActive" , request.IsActive}

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
