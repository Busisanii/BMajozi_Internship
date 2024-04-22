namespace BMajozi.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Names { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; } = DateTime.Now;
        public string ContactNo { get; set; }
    }
}
