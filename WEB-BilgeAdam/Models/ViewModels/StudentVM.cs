namespace WEB_BilgeAdam.Models.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public double? Average { get; set; }
        public string ClassroomName { get; set; }
    }
}
