using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public abstract class Exam : ICloneable, IComparable
    {
        public TimeSpan TimeOfExam { get; set; }
        public int NumberOfQuestions { get; set; }
        public Question[] Questions { get; set; }

        public Exam() { }

        public Exam(TimeSpan timeOfExam, int numberOfQuestions)
        {
            TimeOfExam = timeOfExam;
            NumberOfQuestions = numberOfQuestions;
        }

        public void AddQuestions(Question[] questions)
        {
            Questions = questions;
            NumberOfQuestions = questions.Length;
        }

        public abstract void ShowExam();
        public abstract object Clone();
        public abstract int CompareTo(object obj);
        public abstract override string ToString();
    }

}
