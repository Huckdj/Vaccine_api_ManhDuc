using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUserAdmin
{
    [Route("api/[controller]/[action]")]
    public class ScanBookingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[ExecBooking]";

        public ScanBookingController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> ScanDaily()
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "SCAN-DAILY" }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                if (resultList == null || resultList.Count == 0)
                {
                    return Ok(new DataResponse("Không có lịch tiêm cần thông báo", null, "0"));
                }

                var emailService = new EmailService(_configuration);

                foreach (var user in resultList)
                {
                    string name = user.ContainsKey("Name") ? user["Name"].ToString() : "Không có tên";
                    string nameContact = user.ContainsKey("NameContact") ? user["NameContact"].ToString() : "Không có tên liên hệ";
                    string sdt = user.ContainsKey("SDT") ? user["SDT"].ToString() : "Không có số điện thoại";
                    string email = user.ContainsKey("Email") ? user["Email"].ToString() : "Không có email";
                    string formattedDateInject = user.ContainsKey("FormattedDateInject") ? user["FormattedDateInject"].ToString() : "Không có ngày tiêm";
                    int bookingID = user.ContainsKey("ID") ? Convert.ToInt32(user["ID"]) : 0;
                    string address = user.ContainsKey("Address") ? user["Address"].ToString() : "null";
                    string namecenter = user.ContainsKey("namecenter") ? user["namecenter"].ToString() : "null";

                    if (!string.IsNullOrEmpty(email))
                    {
                        string body = $@"
                            <div style=""display: grid; place-content: center; gap: 20px; text-align: center; font-family: Arial, sans-serif; padding: 20px;"">
                            <div>
                                <img src=""https://res.cloudinary.com/dumx42hqq/image/upload/v1731056834/PostImages/icsym0mh9awpaghpnvd0.png"" 
                                        style=""max-width: 100%; height: auto; border-radius: 10px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);"" 
                                        alt=""MADU Vaccinations"">
                            </div>
                            <div style=""margin-top: 20px;"">
                                <h2 style=""color: #2c3e50;"">Nhắc nhở lịch tiêm của bạn hôm nay</h2>
		                        <p>Bạn có một lịch tiêm hôm nay ở trung tâm {namecenter} hãy đến địa chỉ dưới để chúng tôi hỗ trợ nhé</p>
                                <p><strong>Địa chỉ:</strong> {address}</p>
                                <p><strong>Tên trung tâm:</strong> {namecenter}</p>
                                <p style=""font-size: 1.1rem; color: #34495e;"">Chào {name}</p>
                                <p><strong>Số điện thoại:</strong> {sdt}</p>
                                <p><strong>Ngày tiêm chủng:</strong> {formattedDateInject}</p>
                                <p style=""color: #16a085; font-size: 1.1rem;"">Mã lịch đặt trước: {bookingID}</p>
                            </div>
                            <div style=""margin-top: 30px; font-size: 1.1rem; color: #34495e;"">
                                <p>Liên hệ với chúng tôi:</p>
                                <div>
                                    <a href=""tel:0331234567"" style=""color: #16a085; text-decoration: none;"">033.123.4567</a>
                                    &nbsp;|&nbsp;
                                    <a href=""https://www.facebook.com/madu.vaccinations/?_rdc=1&_rdr#"" style=""color: #16a085; text-decoration: none;"">FACEBOOK</a>
                                    &nbsp;|&nbsp;
                                    <a href=""/"" style=""color: #16a085; text-decoration: none;"">WEBSITE</a>
                                </div>
                            </div>
                        </div>
";

                        // Gửi email thông báo
                        await emailService.SendEmailAsync(email, "Nhắc lịch tiêm chủng", body);
                    }
                }

                // Trả về phản hồi thành công
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
