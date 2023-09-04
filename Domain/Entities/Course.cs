namespace Domain
{
    public class Course : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Logo { get; set; } = null;
    }
}


