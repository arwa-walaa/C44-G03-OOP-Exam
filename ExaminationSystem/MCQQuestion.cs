using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class MCQQuestion : Question
    {
        public MCQQuestion() : base() { }

        public MCQQuestion(string header, string body, int mark) : base(header, body, mark) { }

        public override object Clone()
        {
            var clone = new MCQQuestion(Header, Body, Mark);
            if (AnswerList != null)
            {
                clone.AnswerList = AnswerList.Select(a => (Answer)a.Clone()).ToArray();
            }
            if (RightAnswer != null)
            {
                clone.RightAnswer = (Answer)RightAnswer.Clone();
            }
            return clone;
        }

        public override int CompareTo(object obj)
        {
            if (obj is MCQQuestion other)
                return Header.CompareTo(other.Header);
            throw new ArgumentException("Object is not an MCQQuestion");
        }

        public override string ToString()
        {
            return $"MCQ Question: {Header}\n{Body}\nMark: {Mark}";
        }
    }

    
  
}
