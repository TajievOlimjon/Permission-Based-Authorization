namespace Infrastructure
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.RoleName).IsRequired();
            builder.HasIndex(x => x.RoleName).IsUnique();
            builder.HasKey(x => x.Id);

            builder.HasData(new List<Role>
            {
                new Role{Id=1,RoleName=DefaultRoles.Administrator.ToString(),IsActive=true},
                new Role{Id=2,RoleName=DefaultRoles.User.ToString(),IsActive=true}
            });
        }
    }
}
