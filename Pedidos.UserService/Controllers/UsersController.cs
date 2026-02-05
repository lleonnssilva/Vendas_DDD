using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;

namespace Pedidos.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
        private readonly IConfiguration _configuration;

        public UsersController(IAmazonCognitoIdentityProvider cognitoIdentityProvider, IConfiguration configuration)
        {
            _cognitoIdentityProvider = cognitoIdentityProvider;
            _configuration = configuration;
            //_cognitoClient = cognitoClient;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _configuration["AWS:AppClientId"],
                AuthParameters = new Dictionary<string, string>
        {
            { "USERNAME", request.Email },
            { "PASSWORD", request.Password }
        }
            };

            try
            {
                var authResponse = await _cognitoIdentityProvider.InitiateAuthAsync(authRequest);

                // Check if the user is being prompted for a new password
                if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                {
                    // In case of NEW_PASSWORD_REQUIRED, respond with a new password and additional user attributes
                    var respondRequest = new RespondToAuthChallengeRequest
                    {
                        ChallengeName = ChallengeNameType.NEW_PASSWORD_REQUIRED,
                        ClientId = _configuration["AWS:AppClientId"],
                        ChallengeResponses = new Dictionary<string, string>
                {
                    { "USERNAME", request.Email },  // User email/username
                    { "NEW_PASSWORD", request.Password },  // New password (should be a different one if intended)
                    { "GIVEN", "given" },  // You may want to adjust these fields based on your setup
                    { "GENDER", "Masculino" }
                },
                        Session = authResponse.Session  // Required session from the initial auth attempt
                    };

                    // Send the response to Cognito to complete the password change
                    var respondResponse = await _cognitoIdentityProvider.RespondToAuthChallengeAsync(respondRequest);

                    // Return the authentication result containing the new access token
                    return Ok(new
                    {
                        AccessToken = respondResponse.AuthenticationResult.AccessToken,
                        RefreshToken = respondResponse.AuthenticationResult.RefreshToken,
                        IdToken = respondResponse.AuthenticationResult.IdToken
                    });
                }

                // If no challenge is required (i.e., successful login), just return the access token
                return Ok(new
                {
                    AccessToken = authResponse.AuthenticationResult.AccessToken,
                    RefreshToken = authResponse.AuthenticationResult.RefreshToken,
                    IdToken = authResponse.AuthenticationResult.IdToken
                });
            }
            catch (AmazonCognitoIdentityProviderException ex)
            {
                // Detailed logging and error handling
                return BadRequest(new
                {
                    message = "Authentication failed",
                    errorCode = ex.ErrorCode,
                    errorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _configuration["AWS:AppClientId"],
                Username = request.Email,
                Password = request.Password,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = request.Email }
                }
            };

            try
            {
                var response = await _cognitoIdentityProvider.SignUpAsync(signUpRequest);
                return Ok(new { message = "Usuário registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
        {
            var respondRequest = new RespondToAuthChallengeRequest
            {
                ChallengeName = ChallengeNameType.SMS_MFA, // Use EMAIL_VERIFICATION if email code
                ClientId = _configuration["AWS:AppClientId"],
                ChallengeResponses = new Dictionary<string, string>
        {
            { "USERNAME", request.Email }, // The user's email/username
            { "SMS_MFA_CODE", request.Code } // The verification code the user has received
        },
                Session = request.Session // Session returned from the initial login attempt
            };

            try
            {
                var respondResponse = await _cognitoIdentityProvider.RespondToAuthChallengeAsync(respondRequest);

                // Return the token after successful verification
                return Ok(new { AccessToken = respondResponse.AuthenticationResult.AccessToken });
            }
            catch (AmazonCognitoIdentityProviderException ex)
            {
                return BadRequest(new
                {
                    message = "Verification failed",
                    errorCode = ex.ErrorCode,
                    errorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"An error occurred: {ex.Message}" });
            }
        }

        public class RegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class VerifyCodeRequest
        {
            public string Email { get; set; }  // The email/username used for login
            public string Code { get; set; }   // The verification code received via SMS or email
            public string Session { get; set; } // The session from the initial auth response
        }
    }
}
