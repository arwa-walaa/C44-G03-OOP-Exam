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

      
        public override string ToString()
        {
            return $"MCQ Question: {Header}\n{Body}\nMark: {Mark}";
        }
    }

    
  
}
