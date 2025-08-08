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

        public override string ToString()
        {
            return $"True/False Question: {Header}\n{Body}\nMark: {Mark}";
        }
    }
}
