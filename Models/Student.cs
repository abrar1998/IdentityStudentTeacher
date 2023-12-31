using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2Identity.Models
{
    public class Student
    {
        [Key]
        public int S_Id {  get; set; }
        public string Name {  get; set; }
        public string Class {  get; set; }
        public string Email {  get; set; }

       
        public string TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }


    }
}
