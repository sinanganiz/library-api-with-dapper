namespace LibraryApi.DTOs.Books
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public bool IsAvailable { get; set; } = true;
    }
}