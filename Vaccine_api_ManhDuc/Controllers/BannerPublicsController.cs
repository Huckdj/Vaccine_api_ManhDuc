using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpPost]
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
        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> DeleteBanner([FromBody] BannerPublics.DeleteBanner banner)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE" },
                    { "ID", banner.ID },
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
        public async Task<IActionResult> UpDateByID([FromBody] BannerPublics.UpDateBanner banner)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "UPDATE-BY-ID" },
                    { "ID", banner.ID },
                    { "Name", banner.Name },
                    { "PosText", banner.PosText },
                    { "Description", banner.Description },
                    { "LinkImages", banner.LinkImages }
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
        public async Task<IActionResult> GetByPosText([FromBody] BannerPublics.GetByPosText banner)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-BYPOSTEXT" },
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
