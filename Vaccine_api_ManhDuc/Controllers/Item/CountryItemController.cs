using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class CountryItemController : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecCountryItem";

        public CountryItemController(IConfiguration configuration)
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
                    { "Type", "GET-COUNTRY" }
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
                    { "Type", "GET-COUNTRY-PUBLIC" }
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
        public async Task<IActionResult> InsertCountry([FromBody] CountryItem.InsertCountry request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-COUNTRY" },
                    { "NameCountry", request.NameCountry }
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
        public async Task<IActionResult> DeletCountry([FromBody] CountryItem.UpdateCountry request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE-COUNTRY" },
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
        public async Task<IActionResult> EditById([FromBody] CountryItem.EditByID request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EDITCOUNTRY-BYID" },
                    { "NameCountry", request.NameCountry},
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
        public async Task<IActionResult> SetActive([FromBody] CountryItem.SetActive request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ACTIVE-COUNTRY" },
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
