using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class VaccineCenterController : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecVaccineCenter";

        public VaccineCenterController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllDataCenter()
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
                    { "Type", "GET-PUBLIC" }
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
        public async Task<IActionResult> InsertCenter([FromBody] VaccineCenter.insertcenter request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-CENTER" },
                    { "Name", request.Name },
                    { "Address", request.Address },
                    { "Ward", request.Ward },
                    { "District", request.District },
                    { "City", request.City },
                    { "LinkGoogle", request.LinkGoogle }
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
        public async Task<IActionResult> EditCenterItem([FromBody] VaccineCenter.EditCenter request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EDIT-CENTER" },
                    { "ID",request.ID},
                    { "Name", request.Name },
                    { "Address", request.Address },
                    { "Ward", request.Ward },
                    { "District", request.District },
                    { "City", request.City },
                    { "LinkGoogle",request.LinkGoogle}
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
        public async Task<IActionResult> DeleteCenterItem([FromBody] VaccineCenter.DeleteCenterr request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE-CENTER-ITEM" },
                    { "ID",request.ID}
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
        public async Task<IActionResult> ActiveCenterItem([FromBody] PackItem.ActivePackItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ACTIVE-CENTER-ITEM" },
                    { "ID",request.ID},
                    { "IsActive", request.IsActive }
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
        public async Task<IActionResult> GetCenterByID([FromBody] PackItem.centerID request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-BYID" },
                    { "ID",request.ID} 
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
