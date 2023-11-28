using ApplicationCore_BilgeAdam.Entities.Concrete;

namespace WEB_BilgeAdam.Models.ViewModels
{
    public class ClassroomsForTeacherVM
    {
        public string TeacherName { get; set; }
        public string ClassroomName { get; set; }
        public byte ClassroomNo { get; set; }
        public int ClassroomId { get; set; }
        public int ClassroomSize { get; set; }

    }
}
