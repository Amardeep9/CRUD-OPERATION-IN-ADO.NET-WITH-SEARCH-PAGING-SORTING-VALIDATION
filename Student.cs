using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewPracticeforCRUDinADO.Models
{
    public class Student
    {

        public int RollNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Emailid { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Compare("PassWord")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DOB { get; set; }
    }
}