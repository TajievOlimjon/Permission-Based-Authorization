namespace Infrastructure
{
    public class RolePermissionsConfiguration
    {
        public static async Task AddPermissionsToRoles(ApplicationDbContext dbContext)
        {
            var adminRole = await dbContext.Roles.FirstOrDefaultAsync(x=>x.RoleName== DefaultRoles.Administrator);
            int roleId = 0;
            if(adminRole == null)
            {
                var newRole = new Role { RoleName = DefaultRoles.Administrator, IsActive = true, CreateDate = DateTimeOffset.UtcNow };
                await dbContext.Roles.AddAsync(newRole);
                await dbContext.SaveChangesAsync();
                roleId = newRole.Id;
            }
            else
            {
                roleId = adminRole.Id;
            }

            var permissions = new List<GetAllPermissionsDto>();
            permissions.GetPermissions(typeof(Permissions));

            if (permissions.Count != 0)
            {
                var roleClaims = new List<RoleClaim>();
                foreach (var permission in permissions)
                {
                    var roleClaim = await dbContext.RoleClaims.FirstOrDefaultAsync(x=>x.RoleId==roleId&& x.ClaimType==permission.PermissionType && x.ClaimValue==permission.PermissionValue);
                    if (roleClaim != null) continue;
                    roleClaims.Add(new RoleClaim { RoleId = roleId, ClaimType = permission.PermissionType, ClaimValue = permission.PermissionValue });
                }
                await dbContext.RoleClaims.AddRangeAsync(roleClaims);
                await dbContext.SaveChangesAsync();
            }


            var userRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == DefaultRoles.User);
            roleId = 0;
            if (userRole == null)
            {
                var newRole = new Role { RoleName = DefaultRoles.Administrator, IsActive = true, CreateDate = DateTimeOffset.UtcNow };
                await dbContext.Roles.AddAsync(newRole);
                await dbContext.SaveChangesAsync();
                roleId = newRole.Id;
            }
            else
            {
                roleId = userRole.Id;
            }
            permissions.Clear();
            permissions = new List<GetAllPermissionsDto>()
            {
                new GetAllPermissionsDto{PermissionType="Permission",PermissionValue= Permissions.Course.View }
            };

            if (permissions.Count != 0)
            {
                var roleClaims = new List<RoleClaim>();
                foreach (var permission in permissions)
                {
                    var roleClaim = await dbContext.RoleClaims.FirstOrDefaultAsync(x => x.RoleId == roleId && x.ClaimType == permission.PermissionType && x.ClaimValue == permission.PermissionValue);
                    if (roleClaim != null) continue;
                    roleClaims.Add(new RoleClaim { RoleId = roleId, ClaimType = permission.PermissionType, ClaimValue = permission.PermissionValue });
                }
                await dbContext.RoleClaims.AddRangeAsync(roleClaims);
                await dbContext.SaveChangesAsync();
            }




        }
    }
   
}
