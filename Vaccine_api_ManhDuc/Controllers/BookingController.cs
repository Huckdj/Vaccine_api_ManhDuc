using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class BookingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[ExecBooking]";

        public BookingController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> Booking([FromBody] Booking.insertbooking request)
        {
            try
            {
                // Serialize vaccine data nếu cần
                string vaccinepack = request?.vaccinepack != null ? JsonSerializer.Serialize(request.vaccinepack) : null;
                string vaccinesingle = request?.vaccinesingle != null ? JsonSerializer.Serialize(request.vaccinesingle) : null;

                // Tạo dictionary parameters cho stored procedure
                Dictionary<string, object> parameters = new()
        {
            { "Type", "BOOKING" },
            { "Name", request?.Name },
            { "Birthday", request?.Birthday },
            { "SDTInject", request?.SDTInject },
            { "NameContact", request?.NameContact },
            { "DateInject", request?.DateInject },
            { "SDT", request?.SDT },
            { "Email", request?.Email },
            { "IDUser", request?.IDUser },
            { "IDCenter", request?.IDCenter },
            { "vaccinepack", vaccinepack },
            { "vaccinesingle", vaccinesingle }
        };

                // Gọi stored procedure và nhận kết quả
                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse để trả về
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                var user = resultList[0];
                if (user.ContainsKey("ErrorCode") && Convert.ToInt32(user["ErrorCode"]) == 0)
                {
                    // Lấy thông tin cần thiết
                    int bookingID = Convert.ToInt32(user["BookingID"]);

                    var emailService = new EmailService(_configuration);
                    string formattedDateInject = user["FormattedDateInject"]?.ToString();
                    string body = $@"
                    <div style='text-align: center; font-family: Arial, sans-serif;'>
                        <h2 style='color: #2c3e50;'>Thông Báo Đặt Tiêm Chủng Thành Công</h2>
                        <p style='font-size: 1.1rem; color: #34495e;'>Cảm ơn {request.Name} đã đặt lịch tiêm chủng với chúng tôi!</p>
                        <p><strong>Tên:</strong> {request.Name}</p>
                        <p><strong>Tên Liên hệ:</strong> {request.NameContact}</p>
                        <p><strong>Số điện thoại:</strong> {request.SDT}</p>
                        <p><strong>Ngày tiêm chủng:</strong> {formattedDateInject}</p>
                        <p style='color: #16a085; font-size: 1.1rem;'>Mã lịch đặt trước: {bookingID}</p>
                        <p>Chúng tôi sẽ liên hệ với bạn để xác nhận lịch tiêm!</p>
                    </div>";

                    // Gửi email thông báo
                    await emailService.SendEmailAsync(request.Email, "Thông Báo Đặt Tiêm Chủng Thành Công", body);
                }

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có exception
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetDataChart()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "GET-DATA-CHART" }
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
        public async Task<IActionResult> GetDataByID([FromBody] Booking.GetdataID request)
        {
            try
            {

                // Tạo dictionary parameters cho stored procedure
                Dictionary<string, object> parameters = new()
                {
                   { "Type", "GET-DATA-BY-ID-USER" },
                    {"IDUser", request.IDUser }
                   
                };

                // Gọi stored procedure và nhận kết quả
                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse để trả về
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

                
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có exception
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllData()
        {
            try
            {

                // Tạo dictionary parameters cho stored procedure
                Dictionary<string, object> parameters = new()
                {
                   { "Type", "GET-DATA-BY-BOOKINGID" }

                };

                // Gọi stored procedure và nhận kết quả
                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse để trả về
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");


                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có exception
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UPDATESTATUS([FromBody] Booking.updatestatus request)
        {
            try
            {

                // Tạo dictionary parameters cho stored procedure
                Dictionary<string, object> parameters = new()
                {
                   { "Type", "UPDATE-STATUS" },
                    {"ID", request.ID },
                    {"NewStatus",request.NewStatus }

                };

                // Gọi stored procedure và nhận kết quả
                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse để trả về
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");


                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có exception
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
