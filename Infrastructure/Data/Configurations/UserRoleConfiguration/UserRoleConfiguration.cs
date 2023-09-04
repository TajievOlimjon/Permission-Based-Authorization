namespace Infrastructure
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasData(new List<UserRole>
            {
                new UserRole{RoleId = 1,UserId =1},
            });
        }
    }
}
