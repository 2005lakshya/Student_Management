using System.ComponentModel.DataAnnotations.Schema;
namespace YourApp.Models
{
    [Table("tblStudent")]
    public class FormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Course { get; set; }
        public int Semester { get; set; }
        public decimal CGPA { get; set; }
        public DateTime DOB { get; set; }
        public string Hometown { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
