namespace SimpleMVC.Models
{
    public abstract class AbstractPerson
    {
        public int Id { get; set; }
        public string? ProfilePictureURL { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
    }
}
