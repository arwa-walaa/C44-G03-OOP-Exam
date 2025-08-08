namespace ExaminationSystem
{
    internal class Program
    {
        static Subject CreateSubject()
        {
            Console.Write("Enter Subject ID: ");
            int subjectId = int.Parse(Console.ReadLine());

            Console.Write("Enter Subject Name: ");
            string subjectName = Console.ReadLine();

            return new Subject(subjectId, subjectName);
        }

        static void CreateAndTakeFinalExam(Subject subject)
        {
            Console.Write("\nEnter exam duration in minutes: ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter number of questions: ");
            int numQuestions = int.Parse(Console.ReadLine());

            FinalExam finalExam = new FinalExam(TimeSpan.FromMinutes(duration), numQuestions);
            var questions = new List<Question>();

            for (int i = 0; i < numQuestions; i++)
            {
                Console.WriteLine($"\n--- Creating Question {i + 1} ---");
                Console.WriteLine("Question types for Final Exam:");
                Console.WriteLine("1. True/False");
                Console.WriteLine("2. MCQ");
                Console.Write("Choose question type (1 or 2): ");

                string questionType = Console.ReadLine();
                Question question = null;

                if (questionType == "1")
                {
                    question = CreateTrueFalseQuestion();
                }
                else if (questionType == "2")
                {
                    question = CreateMCQQuestion();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Creating MCQ by default.");
                    question = CreateMCQQuestion();
                }

                questions.Add(question);
            }

            finalExam.AddQuestions(questions.ToArray());
            subject.CreateExam(finalExam);

            // Start the exam
            Console.WriteLine($"\n=== STARTING EXAM FOR {subject.SubjectName} ===");
            TakeExam(finalExam);
        }

        static void CreateAndTakePracticalExam(Subject subject)
        {
            Console.Write("\nEnter exam duration in minutes: ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter number of questions: ");
            int numQuestions = int.Parse(Console.ReadLine());

            PracticalExam practicalExam = new PracticalExam(TimeSpan.FromMinutes(duration), numQuestions);
            var questions = new List<Question>();

            for (int i = 0; i < numQuestions; i++)
            {
                Console.WriteLine($"\n--- Creating Question {i + 1} ---");
                Console.WriteLine("Only MCQ questions are allowed in Practical Exams");
                Question question = CreateMCQQuestion();
                questions.Add(question);
            }

            practicalExam.AddQuestions(questions.ToArray());
            subject.CreateExam(practicalExam);

            // Start the exam
            Console.WriteLine($"\n=== STARTING EXAM FOR {subject.SubjectName} ===");
            TakeExam(practicalExam);
        }

        static TrueFalseQuestion CreateTrueFalseQuestion()
        {
            Console.Write("Enter question header: ");
            string header = Console.ReadLine();

            Console.Write("Enter question body: ");
            string body = Console.ReadLine();

            Console.Write("Enter question mark: ");
            int mark = int.Parse(Console.ReadLine());

            TrueFalseQuestion question = new TrueFalseQuestion(header, body, mark);

            Console.WriteLine("Choose the correct answer:");
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
            Console.Write("Enter correct answer (1 or 2): ");

            string correctAnswer = Console.ReadLine();
            if (correctAnswer == "1")
            {
                question.SetRightAnswer(question.AnswerList[0]); // True
            }
            else
            {
                question.SetRightAnswer(question.AnswerList[1]); // False
            }

            return question;
        }

        static MCQQuestion CreateMCQQuestion()
        {
            Console.Write("Enter question header: ");
            string header = Console.ReadLine();

            Console.Write("Enter question body: ");
            string body = Console.ReadLine();

            Console.Write("Enter question mark: ");
            int mark = int.Parse(Console.ReadLine());

            MCQQuestion question = new MCQQuestion(header, body, mark);

            Console.Write("Enter number of answer choices: ");
            int numAnswers = int.Parse(Console.ReadLine());

            var answers = new List<Answer>();

            for (int i = 0; i < numAnswers; i++)
            {
                Console.Write($"Enter answer choice {i + 1}: ");
                string answerText = Console.ReadLine();
                answers.Add(new Answer(i + 1, answerText));
            }

            question.AddAnswers(answers.ToArray());

            Console.Write("Enter the number of the correct answer (1-" + numAnswers + "): ");
            int correctAnswerIndex = int.Parse(Console.ReadLine()) - 1;

            if (correctAnswerIndex >= 0 && correctAnswerIndex < answers.Count)
            {
                question.SetRightAnswer(answers[correctAnswerIndex]);
            }

            return question;
        }

        static void TakeExam(Exam exam)
        {
            var userAnswers = new Answer[exam.Questions.Length];
            int totalScore = 0;
            int earnedScore = 0;

            Console.WriteLine($"Time Limit: {exam.TimeOfExam.TotalMinutes} minutes");
            Console.WriteLine($"Total Questions: {exam.NumberOfQuestions}");
            Console.WriteLine("\n--- EXAM STARTS NOW ---\n");

            for (int i = 0; i < exam.Questions.Length; i++)
            {
                Question question = exam.Questions[i];
                Console.WriteLine($"Question {i + 1}:");
                Console.WriteLine($"Header: {question.Header}");
                Console.WriteLine($"Body: {question.Body}");
                Console.WriteLine($"Mark: {question.Mark}");
                Console.WriteLine();

                if (question.AnswerList != null)
                {
                    Console.WriteLine("Answer choices:");
                    foreach (var answer in question.AnswerList)
                    {
                        Console.WriteLine($"  {answer}");
                    }
                }

                Console.Write("Enter your answer (answer number): ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int answerChoice))
                {
                    if (answerChoice >= 1 && answerChoice <= question.AnswerList.Length)
                    {
                        userAnswers[i] = question.AnswerList[answerChoice - 1];

                        // Check if answer is correct
                        if (question.RightAnswer != null &&
                            userAnswers[i].AnswerId == question.RightAnswer.AnswerId)
                        {
                            earnedScore += question.Mark;
                        }
                    }
                }

                totalScore += question.Mark;
                Console.WriteLine();
            }

            Console.WriteLine("=== EXAM COMPLETED ===\n");

            // Show results based on exam type
            if (exam is FinalExam finalExam)
            {
                ShowFinalExamResults(finalExam, userAnswers, earnedScore, totalScore);
            }
            else if (exam is PracticalExam practicalExam)
            {
                ShowPracticalExamResults(practicalExam, userAnswers);
            }
        }

        static void ShowFinalExamResults(FinalExam exam, Answer[] userAnswers, int earnedScore, int totalScore)
        {
            Console.WriteLine("=== FINAL EXAM RESULTS ===");

            for (int i = 0; i < exam.Questions.Length; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}: {exam.Questions[i].Header}");
                Console.WriteLine($"Your Answer: {userAnswers[i]?.AnswerText ?? "No Answer"}");
                Console.WriteLine($"Correct Answer: {exam.Questions[i].RightAnswer?.AnswerText ?? "Not Set"}");

                bool isCorrect = userAnswers[i] != null && exam.Questions[i].RightAnswer != null &&
                               userAnswers[i].AnswerId == exam.Questions[i].RightAnswer.AnswerId;

                Console.WriteLine($"Status: {(isCorrect ? "Correct" : "Wrong")}");
                Console.WriteLine($"Points: {(isCorrect ? exam.Questions[i].Mark : 0)}/{exam.Questions[i].Mark}");
            }

            Console.WriteLine($"\n=== FINAL GRADE ===");
            Console.WriteLine($"Total Score: {earnedScore}/{totalScore}");
            Console.WriteLine($"Percentage: {(double)earnedScore / totalScore * 100:F2}%");
        }

        static void ShowPracticalExamResults(PracticalExam exam, Answer[] userAnswers)
        {
            Console.WriteLine("=== PRACTICAL EXAM - CORRECT ANSWERS ===");

            for (int i = 0; i < exam.Questions.Length; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}: {exam.Questions[i].Header}");
                Console.WriteLine($"Correct Answer: {exam.Questions[i].RightAnswer?.AnswerText ?? "Not Set"}");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("=== EXAMINATION SYSTEM ===");

            // Create a subject
            Subject subject = CreateSubject();

            Console.WriteLine("\nChoose exam type:");
            Console.WriteLine("1. Final Exam");
            Console.WriteLine("2. Practical Exam");
            Console.Write("Enter your choice (1 or 2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateAndTakeFinalExam(subject);
            }
            else if (choice == "2")
            {
                CreateAndTakePracticalExam(subject);
            }
            else
            {
                Console.WriteLine("Invalid choice. Creating Final Exam by default.");
                CreateAndTakeFinalExam(subject);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }


    }
    
}
