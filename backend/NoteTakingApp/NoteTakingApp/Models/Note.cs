namespace NoteTakingApp.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
