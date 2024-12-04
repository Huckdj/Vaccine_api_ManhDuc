using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vaccine_api_ManhDuc.Data;

namespace Vaccine_api_ManhDuc.Controllers.AuthUser
{
    [Route("api/[controller]/[action]")]
    public class AuthUserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;
        private readonly string _procedureName = "[ExecAccount]";

        public AuthUserController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration.GetConnectionString("VaccineDB");
            _dataRepository = new DataRepository(connectionString);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAuthUs([FromBody]AuthUs.login request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Auth-Login" },
                    { "UserAuth", request.UserAuth},
                    { "Password", request.Password }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");
                // Trả về kết quả
                var user = resultList[0];
                if (user.ContainsKey("ErrorCode") && Convert.ToInt32(user["ErrorCode"]) == 0)
                {
                    string token = GenerateJwtToken(user);
                    user["token"] = token;
                }


                return Ok(new { user });
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAuthUs([FromBody] AuthUs.Register request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Auth-Register" },
                    { "Email", request.Email},
                    { "SDT",request.SDT },
                    { "Password", request.Password }
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");
                // Trả về kết quả
                var user = resultList[0];
                if (user.ContainsKey("ErrorCode") && Convert.ToInt32(user["ErrorCode"]) == 0) {
                    var emailService = new EmailService(_configuration);
                    var body = "<div style=\"display: grid; place-content: center; gap: 20px; text-align: center; font-family: Arial, sans-serif;\">\r\n    <div>\r\n        <h1 style=\"white-space: nowrap; font-size: 2rem; font-weight: bold; color: #2c3e50;\">Chào mừng bạn đến với MADU Vaccinations</h1>\r\n    </div>\r\n    <div style=\"width: 80%; text-align: justify; margin: 0 auto;\">\r\n        <p style=\"font-size: 1.1rem; color: #34495e;\">Chào mừng bạn đến với MADU Vaccinations – nơi cung cấp các dịch vụ tiêm chủng chất lượng cao, giúp bảo vệ sức khỏe cộng đồng và gia đình bạn. Chúng tôi cam kết mang đến các dịch vụ tiêm phòng an toàn, hiệu quả và tiện lợi, với đội ngũ chuyên gia y tế tận tâm, luôn sẵn sàng hỗ trợ bạn. Cảm ơn bạn đã tin tưởng lựa chọn MADU Vaccinations, nơi sức khỏe của bạn là ưu tiên hàng đầu!</p>\r\n    </div>\r\n    <div>\r\n        <img src=\"https://res.cloudinary.com/dumx42hqq/image/upload/v1731056834/PostImages/icsym0mh9awpaghpnvd0.png\" style=\"max-width: 100%; height: auto; border-radius: 10px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);\" alt=\"MADU Vaccinations\">\r\n    </div>\r\n\t<div class=''>\r\n\t\t<p>Liên hệ với chúng tôi</span>\r\n\t\t<div>\r\n\t\t\t<a href=\"tel:0331234567\">033.123.4567</a>\r\n\t\t\t|\r\n\t\t\t<a href=\"https://www.facebook.com/madu.vaccinations/?_rdc=1&_rdr#\">FACEBOOK</a>\r\n\t\t\t|\r\n\t\t\t<a href=\"/\">WEBSITE</a>\r\n\t\t</div>\r\n\t</div>\r\n</div>\r\n";
                    await emailService.SendEmailAsync(request.Email, "Cảm ơn bạn đã đăng ký", body);

                }
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Token không hợp lệ" });    
                }

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                {
                    return Unauthorized(new { message = "Token không hợp lệ" });
                }

                // Trích xuất thông tin từ các claims trong token
                var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
                var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "AuthUser")?.Value;
                var role = jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                if (userId == null || email == null)
                {
                    return Unauthorized(new { message = "Không thể lấy thông tin người dùng từ token" });
                }

                // Tạo đối tượng trả về thông tin người dùng
                var userInfo = new
                {
                    UserId = userId,
                    Email = email,
                    Role = role
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetByID([FromBody] AuthUs.Getid request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Get-by-ID" },
                    { "ID", request.ID}
                };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Tạo đối tượng DataResponse
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");
                // Trả về kết quả
                var user = resultList[0];
                if (user.ContainsKey("ErrorCode") && Convert.ToInt32(user["ErrorCode"]) == 0)
                {
                    string token = GenerateJwtToken(user);
                    user["token"] = token;
                }


                return Ok(new { user });
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RechargePassword([FromBody] AuthUs.RechangerPassword request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "Type", "Recharge-Password" },
                    { "ID", request.ID},
                    { "Password",request.Password},
                    { "NewPassword",request.NewPassword}
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
        public IActionResult Logout([FromBody] string token)
        {
            // Gọi API để làm token hết hạn
            TokenExpiryMiddleware.ExpireToken(token);

            return Ok(new { ErrorCode = 200, Message = "Đăng xuất thành công." });
        }

        public class TokenExpiryMiddleware
        {
            private readonly RequestDelegate _next;
            private static HashSet<string> _expiredTokens = new HashSet<string>(); // Dùng để lưu trữ các token đã hết hạn

            public TokenExpiryMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {
                // Lấy token từ header Authorization
                var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (!string.IsNullOrEmpty(token) && _expiredTokens.Contains(token))
                {
                    // Nếu token đã hết hạn, trả về lỗi
                    httpContext.Response.StatusCode = 401; // Unauthorized
                    await httpContext.Response.WriteAsync("Token đã hết hạn.");
                    return;
                }

                await _next(httpContext);
            }

            // API để làm token hết hạn
            public static void ExpireToken(string token)
            {
                _expiredTokens.Add(token); // Thêm token vào danh sách hết hạn
            }
        }
        private string GenerateJwtToken(Dictionary<string, object> user)
        {
            var jwtSettings = HttpContext.RequestServices.GetService<IConfiguration>().GetSection("JwtSettings");

            // Sử dụng tên claim đơn giản thay vì URI dài
            var claims = new List<Claim>
    {
        new Claim("ID", user["ID"].ToString()),
        new Claim("AuthUser", user["Email"].ToString()),
        new Claim("role", (bool)user["Admin"] ? "Admin" : "User") 
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
