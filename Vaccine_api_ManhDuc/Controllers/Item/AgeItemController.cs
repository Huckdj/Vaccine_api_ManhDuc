using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class AgeItemController : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[ExecAgeItem]";

        public AgeItemController(IConfiguration configuration)
        {
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
                    { "Type", "GET-AGE-ITEM" }
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
        public async Task<IActionResult> GetdataPublic()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-AGE-PUBLIC" }
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
        public async Task<IActionResult> InsertCountry([FromBody] AgeItem.InsertAge request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-AGE" },
                    { "NameYearOld", request.NameYearOld }
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
        public async Task<IActionResult> DeleteAge([FromBody] AgeItem.UpdateAge request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE-AGE" },
                    { "ID", request.ID }
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
        public async Task<IActionResult> EditByAgeId([FromBody] AgeItem.EditByAgeID request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EDITAGE-BYID" },
                    { "ID", request.ID },
                    { "NameYearOld", request.NameYearOld}
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
        public async Task<IActionResult> SetActiveAge([FromBody] AgeItem.SetActiveAge request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ACTIVE-AGE" },
                    { "IsActive", request.IsActive},
                    { "ID", request.ID }
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
