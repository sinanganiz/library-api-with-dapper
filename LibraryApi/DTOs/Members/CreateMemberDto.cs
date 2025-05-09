namespace LibraryApi.DTOs.Members
{
    public class CreateMemberDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}