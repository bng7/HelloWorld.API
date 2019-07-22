namespace HelloWorld.API.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}