namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        [PermissionAuthorize(Permissions.Role.View)]
        public async Task<List<GetAllRolesDto>> GetAllRolesAsync()
        {
            return await _roleService.GetAllRolesAsync();
        }
        [HttpGet("GetAllPermissionsByRoleId")]
        [PermissionAuthorize(Permissions.Role.View)]
        public async Task<IActionResult> GetAllPermissionsByRoleIdAsync([FromQuery] PermissionFilter filter)
        {
            var response = await _roleService.GetAllPermissionsByRoleIdAsync(filter);

            return StatusCode(response.StatusCode,response);
        }
        [HttpPut("UpdateRolePermissionForRole")]
        [PermissionAuthorize(Permissions.Role.Edit)]
        public async Task<IActionResult> UpdateRolePermissionForRoleAsync([FromBody]UpdatePermissionForRoleDto model)
        {
            var response = await _roleService.UpdateRolePermissionForRoleAsync(model);

            return StatusCode(response.StatusCode, response);
        }
    }
}
