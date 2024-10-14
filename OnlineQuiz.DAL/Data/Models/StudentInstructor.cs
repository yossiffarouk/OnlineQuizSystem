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
            public virtual Student Student { get; set; }

            public string InstructorId { get; set; }
            public virtual Instructor Instructor { get; set; }


            public DateTime TimeNow { get; set; } = DateTime.Now;


    }
}
