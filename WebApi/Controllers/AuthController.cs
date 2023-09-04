namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);

            return StatusCode(response.StatusCode, response);
        }
    }
}
