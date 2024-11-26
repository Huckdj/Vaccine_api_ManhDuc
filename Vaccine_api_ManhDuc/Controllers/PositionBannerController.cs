using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PositionBannerController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecPositionBanner";

        public PositionBannerController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        // Định tuyến riêng cho phương thức GetBannerData
        [HttpPost]
        public async Task<IActionResult> GetPositionData()
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
        [HttpPost]
        public async Task<IActionResult> AddPositionBanner([FromBody] BannerPublics.AddPositonBanner request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ADD-POSITION-BANNER" },
                    { "Name", request.Name },
                    {"PositionCode",request.PositionCode }
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
        [HttpPost]
        public async Task<IActionResult> EditPositionBanner([FromBody] BannerPublics.EditPosition request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "UPDATE-BY-ID" },
                    { "ID", request.ID },
                    { "Name", request.Name },
                    { "PositionCode",request.PositionCode }
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
        public async Task<IActionResult> DeletePosition([FromBody] BannerPublics.DeletePositon request)
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
    }
}
