using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class FinalExam : Exam
    {
        public int Grade { get; private set; }

        public FinalExam() : base() { }

        public FinalExam(TimeSpan timeOfExam, int numberOfQuestions) : base(timeOfExam, numberOfQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("=== FINAL EXAM ===");
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

            Grade = CalculateGrade();
            Console.WriteLine($"Final Grade: {Grade}");
        }

        public int CalculateGrade()
        {
            // Simple grade calculation - for demo purposes
            // In real implementation, this would be based on student answers
            if (Questions != null)
            {
                return Questions.Sum(q => q.Mark);
            }
            return 0;
        }

        public override object Clone()
        {
            var clone = new FinalExam(TimeOfExam, NumberOfQuestions);
            if (Questions != null)
            {
                clone.Questions = Questions.Select(q => (Question)q.Clone()).ToArray();
            }
            return clone;
        }

        public override int CompareTo(object obj)
        {
            if (obj is FinalExam other)
                return TimeOfExam.CompareTo(other.TimeOfExam);
            throw new ArgumentException("Object is not a FinalExam");
        }

        public override string ToString()
        {
            return $"Final Exam - Time: {TimeOfExam.TotalMinutes} mins, Questions: {NumberOfQuestions}, Grade: {Grade}";
        }
    }
}
