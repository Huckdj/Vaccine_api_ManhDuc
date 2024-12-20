using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vaccine_api_ManhDuc.Data;
using static Vaccine_api_ManhDuc.Data.AuthUs;

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
        [HttpPost]
        public async Task<IActionResult> GoogleLogin([FromBody] AuthUs.GoogleLoginRequest request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
        {
            { "Type", "Google-Login" },
            { "Email", request.Email },
            { "GoogleId", request.GoogleId }
        };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                // Create DataResponse object
                DataResponse dataResponse = new DataResponse("Success", resultList, "0");

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
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] AuthUs.forgotpassword request)
        {
            try
            {
                Dictionary<string, object> parameters = new()
        {
            { "Type", "Forget-Password" },
            { "Email", request.Email},
            { "SDT",request.SDT }
        };

                var resultList = await _dataRepository.GetDataResponse(_procedureName, parameters);

                DataResponse dataResponse = new DataResponse("Success", resultList, "0");
                var user = resultList[0];
                if (user.ContainsKey("ErrorCode") && Convert.ToInt32(user["ErrorCode"]) == 0)
                {
                    string email = user.ContainsKey("Email") ? user["Email"].ToString() : "null";
                    string password = user.ContainsKey("Password") ? user["Password"].ToString() : "null";
                    var emailService = new EmailService(_configuration);

                    var body = $@"<!DOCTYPE html>
                        <html lang=""vi"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>MADU Vaccinations - Đặt Mật Khẩu Mới</title>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                    display: grid;
                                    place-content: center;
                                    min-height: 100vh;
                                    background-color: #f0f2f5;
                                    margin: 0;
                                    padding: 20px;
                                    box-sizing: border-box;
                                }}
                                .container {{
                                    background-color: white;
                                    border-radius: 10px;
                                    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                                    padding: 30px;
                                    width: 100%;
                                    max-width: 400px;
                                    text-align: center;
                                }}
                                h1 {{
                                    color: #2c3e50;
                                    margin-bottom: 20px;
                                }}
                                .form-group {{
                                    margin-bottom: 15px;
                                    text-align: left;
                                }}
                                label {{
                                    display: block;
                                    margin-bottom: 5px;
                                    color: #34495e;
                                }}
                                input {{
                                    width: 100%;
                                    padding: 10px;
                                    border: 1px solid #bdc3c7;
                                    border-radius: 5px;
                                    box-sizing: border-box;
                                }}
                                .submit-btn {{
                                    width: 100%;
                                    padding: 12px;
                                    background-color: #3498db;
                                    color: white;
                                    border: none;
                                    border-radius: 5px;
                                    cursor: pointer;
                                    transition: background-color 0.3s ease;
                                    margin-top: 15px;
                                }}
                                .submit-btn:hover {{
                                    background-color: #2980b9;
                                }}
                                .logo {{
                                    max-width: 150px;
                                    margin-bottom: 20px;
                                }}
                                .password-note {{
                                    font-size: 0.9em;
                                    color: #7f8c8d;
                                    margin-top: 10px;
                                    text-align: left;
                                }}
                                .contact-links {{
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                    gap: 10px;
                                }}
                                .contact-links a {{
                                    text-decoration: none;
                                    color: #2c3e50;
                                    font-weight: bold;
                                }}
                                .contact-links a:hover {{
                                    color: #3498db;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <img src=""https://res.cloudinary.com/dumx42hqq/image/upload/v1731056834/PostImages/icsym0mh9awpaghpnvd0.png"" 
                                     alt=""MADU Vaccinations Logo"" 
                                     class=""logo"">
        
                                <h1>Mật Khẩu Mới</h1>
        
                                <form id=""newPasswordForm"">
                                    <div class=""form-group"">
                                        <label for=""newPassword"">Mật Khẩu Mới</label>
                                        <input 
                                            type=""text"" 
                                            id=""newPassword"" 
                                            name=""newPassword"" 
                                            value=""{password}"" 
                                            readonly
                                        >
                                        <p class=""password-note"">
                                            Đây là mật khẩu mới của bạn. Vui lòng ghi lại và thay đổi sau khi đăng nhập.
                                        </p>
                                    </div>
            
                                    <button type=""button"" class=""submit-btn"" onclick=""copyPassword()"">
                                        Sao Chép Mật Khẩu
                                    </button>
                                </form>
                            </div>
                            <footer>
                                <div>
                                    <p style=""font-weight: bold;"">Liên hệ với chúng tôi</p>
                                    <div class=""contact-links"">
                                        <a href=""tel:0331234567"">033.123.4567</a>
                                        <span>|</span>
                                        <a href=""https://www.facebook.com/madu.vaccinations/?_rdc=1&_rdr#"">FACEBOOK</a>
                                        <span>|</span>
                                        <a href=""https://maduvaccinations.vercel.app/"">WEBSITE</a>
                                    </div>
                                </div>
                            </footer>
                            <script>
                                function copyPassword() {{
                                    const passwordInput = document.getElementById('newPassword');
            
                                    // Chọn nội dung trong input
                                    passwordInput.select();
                                    passwordInput.setSelectionRange(0, 99999); // Hỗ trợ mobile

                                    // Sao chép vào clipboard
                                    navigator.clipboard.writeText(passwordInput.value).then(() => {{
                                        alert('Đã sao chép mật khẩu mới. Vui lòng lưu giữ cẩn thận!');
                                    }}).catch(err => {{
                                        alert('Không thể sao chép. Hãy thử chọn và copy thủ công.');
                                    }});
                                }}
                            </script>
                        </body>
                        </html>";
                    await emailService.SendEmailAsync(email, "Bạn đã yêu cầu cấp lại mật khẩu", body);

                    var errorCode = resultList[0]["ErrorCode"].ToString();
                    var errorMessage = resultList[0]["ErrorMessage"].ToString();
                    var result = resultList
                        .Where(user => user.ContainsKey("Email") && user.ContainsKey("ErrorCode") && user.ContainsKey("ErrorMessage"))
                        .Select(user => new
                        {
                            Email = user["Email"].ToString(),
                            ErrorCode = user["ErrorCode"].ToString(),
                            ErrorMessage = user["ErrorMessage"].ToString()
                        }).ToList();


                    return Ok(new
                    {
                        message = "Success",
                        status = "0",
                        data = result
                    });
                }
                else
                {
                    return Ok(dataResponse);
                }
                
            }
            catch (Exception ex)
            {
                // Trả về lỗi
                return StatusCode(500, new { message = ex.Message });
            }
        }


        private string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
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
