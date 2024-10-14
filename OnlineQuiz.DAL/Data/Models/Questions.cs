using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public  class Questions            
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public  string CorrectAnswer { get; set; }
        public bool IsDeleted { get; set; } = false;



        // For Quiz
        public int QuizId { get; set; }
        public virtual Quizzes Quiz { get; set; }

        // for Answer
        public virtual ICollection<Answers> Answers { get; set; } = new HashSet<Answers>();

        // for Option
        public virtual ICollection<Option> Options { get; set; } = new HashSet<Option>();
    }   

    }


