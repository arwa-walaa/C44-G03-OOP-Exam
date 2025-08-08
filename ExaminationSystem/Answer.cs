using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class Answer : ICloneable, IComparable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer() { }

        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }

        public object Clone()
        {
            return new Answer(AnswerId, AnswerText);
        }

        public int CompareTo(object obj)
        {
            if (obj is Answer other)
                return AnswerId.CompareTo(other.AnswerId);
            throw new ArgumentException("Object is not an Answer");
        }

        public override string ToString()
        {
            return $"{AnswerId}. {AnswerText}";
        }
    }

}
