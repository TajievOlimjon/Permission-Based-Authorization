namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllUsers")]
        [PermissionAuthorize(Permissions.User.View)]
        public async Task<IActionResult> GetAllUsers([FromQuery]UserFilter filter)
        {
            var response = await _userService.GetAllUsersAsync(filter);

            return StatusCode(response.StatusCode,response);
        }
        [HttpGet("GetCurrentUserInfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value;
            var userId = Convert.ToInt32(currentUserId);

            var response = await _userService.GetCurrentUserInfoAsync(userId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("UpdateCurrentUserInfo")]
        public async Task<IActionResult> UpdateCurrentUserInfoAsync([FromBody]UpdateCurrentUserDto model)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value;
            var userId = Convert.ToInt32(currentUserId);

            model.UserId = userId;

            var response = await _userService.UpdateCurrentUserInfoAsync(model);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("BlockedUserAccount")]
        [PermissionAuthorize(Permissions.User.Blocked)]
        public async Task<IActionResult> BlockedUserAccountAsync([FromBody]int userId)
        {
            var response = await _userService.BlockedUserAccountAsync(userId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("UnBlockedUserAccount")]
        [PermissionAuthorize(Permissions.User.UnBlocked)]
        public async Task<IActionResult> UnBlockedUserAccountAsync([FromBody] int userId)
        {
            var response = await _userService.UnBlockedUserAccountAsync(userId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("AddRoleToUser")]
        [PermissionAuthorize(Permissions.User.Create)]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] AddRoleToUserDto model)
        {
            var response = await _userService.AddRoleToUserAsync(model);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("RemoveRoleFromUser")]
        [PermissionAuthorize(Permissions.User.Delete)]
        public async Task<IActionResult> RemoveRoleFromUserAsync([FromBody] RemoveRoleFromUserDto model)
        {
            var response = await _userService.RemoveRoleFromUserAsync(model);

            return StatusCode(response.StatusCode, response);
        }
       
        [HttpDelete("DeleteUser")]
        [PermissionAuthorize(Permissions.User.Delete)]
        public async Task<IActionResult> RemoveUserAsync([FromQuery]int userId)
        {
            var response = await _userService.RemoveUserAsync(userId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
