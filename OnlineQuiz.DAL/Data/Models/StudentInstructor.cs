using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public class StudentInstructor
    {
        
            public string StudentId { get; set; }
            public Student Student { get; set; }

            public string InstructorId { get; set; }
            public Instructor Instructor { get; set; }


     //   public DateTime
        

    }
}
