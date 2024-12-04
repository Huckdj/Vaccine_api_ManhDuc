using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class SingleItemController : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecSingleItem";

        public SingleItemController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataSingleAllItem()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-SINGLE-ALL" }
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
                    { "Type", "GET-SINGLE-PUBLIC" }
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
        public async Task<IActionResult> InsertItem([FromBody] SingleItem.InsertSingleItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-SINGLE" },
                    { "Name", request.Name },
                    { "Price", request.Price },
                    { "CountryItem", request.CountryItem },
                    { "ShortContent", request.ShortContent },
                    { "FullContent", request.FullContent },
                    { "LinkImages", request.LinkImages },
                    { "LinkRoute", request.LinkRoute }
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
        public async Task<IActionResult> EditSingleItem([FromBody] SingleItem.EditSingleItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EDIT-SINGLE" },
                    { "ID",request.ID},
                    { "Name", request.Name },
                    { "Price", request.Price },
                    { "CountryItem", request.CountryItem },
                    { "ShortContent", request.ShortContent },
                    { "FullContent", request.FullContent },
                    { "LinkImages", request.LinkImages },
                    { "LinkRoute", request.LinkRoute }
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
        public async Task<IActionResult> Deletesingleitem([FromBody] SingleItem.DeleteSingleItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE-SINGLE-ITEM" },
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
        public async Task<IActionResult> ActiveSingleItem([FromBody] SingleItem.ActiveSingleItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ACTIVE-SINGLE-ITEM" },
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

    }
}
