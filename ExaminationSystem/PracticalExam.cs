using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class PracticalExam : Exam
    {
        public PracticalExam() : base() { }

        public PracticalExam(TimeSpan timeOfExam, int numberOfQuestions) : base(timeOfExam, numberOfQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("=== PRACTICAL EXAM ===");
            Console.WriteLine($"Time: {TimeOfExam.TotalMinutes} minutes");
            Console.WriteLine($"Number of Questions: {NumberOfQuestions}");
            Console.WriteLine();

            if (Questions != null)
            {
                for (int i = 0; i < Questions.Length; i++)
                {
                    Console.WriteLine($"Question {i + 1}:");
                    Console.WriteLine($"Header: {Questions[i].Header}");
                    Console.WriteLine($"Body: {Questions[i].Body}");
                    Console.WriteLine($"Mark: {Questions[i].Mark}");

                    if (Questions[i].AnswerList != null)
                    {
                        Console.WriteLine("Answers:");
                        foreach (var answer in Questions[i].AnswerList)
                        {
                            Console.WriteLine($"  {answer}");
                        }
                    }
                    Console.WriteLine();
                }
            }

            // Show right answers after finishing the exam
            ShowRightAnswers();
        }

        public void ShowRightAnswers()
        {
            Console.WriteLine("=== RIGHT ANSWERS ===");
            if (Questions != null)
            {
                for (int i = 0; i < Questions.Length; i++)
                {
                    if (Questions[i].RightAnswer != null)
                    {
                        Console.WriteLine($"Question {i + 1}: {Questions[i].RightAnswer}");
                    }
                }
            }
        }

        public override object Clone()
        {
            var clone = new PracticalExam(TimeOfExam, NumberOfQuestions);
            if (Questions != null)
            {
                clone.Questions = Questions.Select(q => (Question)q.Clone()).ToArray();
            }
            return clone;
        }

        public override int CompareTo(object obj)
        {
            if (obj is PracticalExam other)
                return TimeOfExam.CompareTo(other.TimeOfExam);
            throw new ArgumentException("Object is not a PracticalExam");
        }

        public override string ToString()
        {
            return $"Practical Exam - Time: {TimeOfExam.TotalMinutes} mins, Questions: {NumberOfQuestions}";
        }
    }
}
