using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostTypeController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecPostType";

        public PostTypeController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }


        [HttpPost]
        public async Task<IActionResult> GetDataPostType()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET" }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> AddPostType([FromBody] PostPublics.AddPostType request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ADD-POSTYPE" },
                    { "NameType", request.NameType }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPostType([FromBody] PostPublics.EditPostType request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "UPDATE-BY-ID" },
                    { "ID", request.ID},
                    { "NameType", request.NameType }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeletePostType([FromBody] PostPublics.DeletePostType request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE" },
                    { "ID", request.ID}
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
