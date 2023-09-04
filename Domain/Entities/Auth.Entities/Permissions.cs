using System.Reflection;

namespace Domain
{
    public static class Permissions
    {
        public static void GetPermissions(this List<GetAllPermissionsDto> allPermissions, Type policy)
        {
            var now = DateTime.Now;
            var nestedTypes = policy.GetNestedTypes(BindingFlags.Public);
            if (nestedTypes.Length > 0)
            {
                foreach (var nested in nestedTypes)
                {
                    FieldInfo[] fields = nested.GetFields(BindingFlags.Static | BindingFlags.Public);

                    foreach (FieldInfo fi in fields)
                    {
                        allPermissions.Add(item: new GetAllPermissionsDto { PermissionType = "Permission", PermissionValue = fi?.GetValue(null).ToString()});
                    }
                }
            }
            else
            {
                allPermissions = new List<GetAllPermissionsDto>();
                return;
            }

        }
        public static List<string> PermissionsForRoleByModule(string module)
        {
            return new List<string>()
            {
                $"{module}.Create",
                $"{module}.View",
                $"{module}.Edit",
                $"{module}.Delete",
            };
        }
        public class Course
        {
            public const string View = "Course.View";
            public const string Create = "Course.Create";
            public const string Edit = "Course.Edit";
            public const string Delete = "Course.Delete";
        }
        public class User
        {
            public const string View = "User.View";
            public const string Create = "User.Create";
            public const string Edit = "User.Edit";
            public const string Delete = "User.Delete";
            public const string Blocked = "User.Blocked";
            public const string UnBlocked = "User.UnBlocked";
        }
        public class Role
        {
            public const string View = "Role.View";
            public const string Create = "Role.Create";
            public const string Edit = "Role.Edit";
            public const string Delete = "Role.Delete";
        }
    }
}

