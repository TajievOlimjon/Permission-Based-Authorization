namespace WebApi.PermissionConfiguration
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string requiredPermission)
        {
            AuthenticationSchemes = "Bearer";
            Policy = requiredPermission;
        }

    }
}
