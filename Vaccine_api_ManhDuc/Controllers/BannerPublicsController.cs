using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // Sử dụng Microsoft.Data.SqlClient
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerPublicsController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly string _procedureName = "ExecBanner";

        public BannerPublicsController(IConfiguration configuration)
        {
            // Lấy chuỗi kết nối từ appsettings.json
            _connectionString = configuration.GetConnectionString("VaccineDB");
        }

        [HttpPost]
        public async Task<IActionResult> GetBannerData() // Mặc định là 'GET'
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET" }
                };

                // Get the data response from the database
                var resultList = await GetDataResponse(parameters);

                // Create the DataResponse object
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

        private async Task<List<Dictionary<string, object>>> GetDataResponse(Dictionary<string, object> parameters)
        {
            // Danh sách chứa kết quả trả về từ SQL Server
            var resultList = new List<Dictionary<string, object>>();

            // Tạo kết nối SQL
            using (var connection = new SqlConnection(_connectionString))
            {
                // Tạo command để thực thi stored procedure
                using (var command = new SqlCommand(_procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    // Mở kết nối
                    await connection.OpenAsync();

                    // Thực thi stored procedure và lấy dữ liệu
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Duyệt qua các hàng dữ liệu trả về
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();

                            // Lấy dữ liệu từ mỗi cột trong hàng
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }

                            // Thêm hàng vào danh sách kết quả
                            resultList.Add(row);
                        }
                    }
                }
            }

            return resultList;
        }
    }
}
