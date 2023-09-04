namespace Infrastructure
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasData(new List<User>
            {
                 new User
                 {
                    Id=1,
                    FirstName = "Olimjon",
                    LastName ="Tajiev",
                    CreateDate = DateTimeOffset.UtcNow,
                    Email = "tajiev@gmail.com",
                    IsBlocked = false,
                    Phone = "000000001",
                    UserName ="Olimjon",
                    HashPassword =BCrypt.Net.BCrypt.HashPassword("12345"),
                    ImageName = "default.png"
                 }
            });
        }
    }
}
