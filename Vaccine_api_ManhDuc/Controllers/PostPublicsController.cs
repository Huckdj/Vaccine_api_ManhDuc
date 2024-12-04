using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostPublicsController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecPost";

        public PostPublicsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        
        [HttpPost]
        public async Task<IActionResult> GetAllDataPost()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-ALL" }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                // Trả về kết quả
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDataPublic()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET" }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                // Trả về kết quả
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] PostPublics.AddPost request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ADD-POST" },
                    { "Title", request.Title },
                    { "ShortContent", request.ShortContent },
                    { "FullContentDesktop" , request.FullContentDesktop },
                    { "FullContentMobile" , request.FullContentMobile },
                    { "LinkImages" , request.LinkImages },
                    { "LinkRoute" , request.LinkRoute },
                    { "PostType",request.PostType}
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
        public async Task<IActionResult> DeletePost([FromBody] PostPublics.DeletePostType request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE" },
                    { "ID", request.ID }
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
        public async Task<IActionResult> UpdatePostbyID([FromBody] PostPublics.UpdatePost request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "UPDATE-BY-ID" },
                    { "ID", request.ID },
                    { "Title", request.Title },
                    { "ShortContent", request.ShortContent },
                    { "FullContentDesktop" , request.FullContentDesktop },
                    { "FullContentMobile" , request.FullContentMobile },
                    { "LinkImages" , request.LinkImages },
                    { "LinkRoute" , request.LinkRoute },
                    { "PostType" , request.PostType }
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
        public async Task<IActionResult> GetPostByID([FromBody] PostPublics.GetPostByID request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-POST-BYID" },
                    { "ID", request.ID }
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
        public async Task<IActionResult> GetByLinkRoute([FromBody] PostPublics.GetByLinkRoute request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-BY-LINKROUTE" },
                    { "LinkRoute", request.LinkRoute }
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
        public async Task<IActionResult> GetFullPostByIdType([FromBody] PostPublics.DeletePostType request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-POST-NAMETYPE" },
                    { "ID", request.ID }
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
        public async Task<IActionResult> RandomPost()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "RANDOM_POST" }
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
        public async Task<IActionResult> RandomAllPost()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "RANDOM_POST_ALL" }
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
        public async Task<IActionResult> SearchPost([FromBody] PostPublics.SearchKeywords request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "SEARCH" },
                    { "SearchKeyword", request.SearchKeyword }
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
