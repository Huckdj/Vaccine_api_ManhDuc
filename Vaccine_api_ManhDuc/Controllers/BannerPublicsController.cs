using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerPublicsController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecBanner";

        public BannerPublicsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        // Định tuyến riêng cho phương thức GetBannerData
        [HttpPost("GetBannerData")]
        public async Task<IActionResult> GetBannerData()
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

        // Định tuyến riêng cho phương thức AddBanner
        [HttpPost("AddBanner")]
        public async Task<IActionResult> AddBanner([FromBody] BannerPublics.AddBanner banner)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ADD-BANNER" },
                    { "Name", banner.Name },
                    { "Description", banner.Description },
                    { "LinkImages", banner.LinkImages },
                    { "PosText", banner.PosText }
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
    }
}
