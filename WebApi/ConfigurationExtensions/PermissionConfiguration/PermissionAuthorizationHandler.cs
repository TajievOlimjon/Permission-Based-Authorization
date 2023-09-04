namespace WebApi
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly ILogger<PermissionAuthorizationHandler> _logger;
        private readonly ApplicationDbContext _context;

        public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            _logger.LogWarning("Evaluating authorization requirement for permission {permission}", requirement.Permission);

            var user = context.User;
            var userId = user.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (userId == null)
                return Task.CompletedTask;

            var findUser = _context.Users.First(x => x.Id.ToString() == userId);
            if (findUser.IsBlocked) return Task.CompletedTask;

            foreach (Claim claim in context.User.Claims)
            {
                if (claim.Type != "Permission" || claim.Value != requirement.Permission)
                    continue;

                _logger.LogInformation("Permission {permission} is satisfied", requirement.Permission);
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}