using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion() : base()
        {
            // Initialize with True/False answers
            AnswerList = new Answer[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
        }

        public TrueFalseQuestion(string header, string body, int mark) : base(header, body, mark)
        {
            // Initialize with True/False answers
            AnswerList = new Answer[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
        }

        public override object Clone()
        {
            var clone = new TrueFalseQuestion(Header, Body, Mark);
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
            if (obj is TrueFalseQuestion other)
                return Header.CompareTo(other.Header);
            throw new ArgumentException("Object is not a TrueFalseQuestion");
        }

        public override string ToString()
        {
            return $"True/False Question: {Header}\n{Body}\nMark: {Mark}";
        }
    }
}
