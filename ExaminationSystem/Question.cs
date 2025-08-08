using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
  
    public abstract class Question 
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] AnswerList { get; set; }
        public Answer RightAnswer { get; set; }

        public Question() { }

        public Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }
        public void AddAnswers(Answer[] answers)
        {
            AnswerList = answers;
        }

        public void SetRightAnswer(Answer answer)
        {
            RightAnswer = answer;
        }

      
        public abstract override string ToString();
    }
}
