using Microsoft.AspNetCore.Mvc;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class PackageItemController : Controller
    {
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "ExecItemPackage";

        public PackageItemController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataPackageAllItem()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-PACKAGEITEM-ALL" }
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
                    { "Type", "GET-PACKAGEITEM-PUBLIC" }
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
        public async Task<IActionResult> InsertItem([FromBody] PackItem.InsertItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "INSERT-PACK" },
                    { "Name", request.Name },
                    { "Price", request.Price },
                    { "TypePack", request.TypePack },
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
        public async Task<IActionResult> EditItem([FromBody] PackItem.EditPackItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "EDIT-PACK" },
                    { "ID",request.ID},
                    { "Name", request.Name },
                    { "Price", request.Price },
                    { "TypePack", request.TypePack },
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
        public async Task<IActionResult> DeletePackItem([FromBody] PackItem.DeletePackItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "DELETE-PACK-ITEM" },
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
        public async Task<IActionResult> ActivePackItem([FromBody] PackItem.ActivePackItem request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "ACTIVE-PACK-ITEM" },
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
