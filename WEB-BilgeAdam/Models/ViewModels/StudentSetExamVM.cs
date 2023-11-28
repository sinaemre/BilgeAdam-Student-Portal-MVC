using System.ComponentModel.DataAnnotations;

namespace WEB_BilgeAdam.Models.ViewModels
{
    public class StudentSetExamVM
    {
        public int Id { get; set; }
        public string? FullName { get; set; }

        [Range(0,100, ErrorMessage = "0 ile 100 arasında giriş yapınız!")]
        [Display(Name = "Sınav 1")]
        public double? Exam1 { get; set; }
        
        [Range(0,100, ErrorMessage = "0 ile 100 arasında giriş yapınız!")]
        [Display(Name = "Sınav 2")]
        public double? Exam2 { get; set; }
        
        public int? ClassroomId { get; set; }
        
        
        public string? ProjectPath { get; set; }
        
        [Display(Name = "Proje")]
        public IFormFile? Project { get; set; }

        [Range(0, 100, ErrorMessage = "0 ile 100 arasında giriş yapınız!")]
        [Display(Name = "Proje Notu")]
        public double? ProjectExam { get; set; }
    }
}
