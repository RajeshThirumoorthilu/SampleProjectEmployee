namespace WebCrud.Models
{
    public class EmployeeWebModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Role { get; set; }
        public decimal Salary { get; set; }
    }
}
