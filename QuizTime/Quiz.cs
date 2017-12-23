using System;
using System.Collections.Generic;
using System.Text;

namespace QuizTime
{
    class Quiz
    {
        private static int nextId = 0;
        private int Id { get; set; }
        private string Name { get; set; }
        private static int dictionaryIndex = 0;
        private double Correct { get; set; }
        private List<string> incorrect = new List<string>();

        private Dictionary<string, Question> questions;
        public Dictionary<string, Question> Questions
        {
            get { return questions; }
        }
        private Dictionary<string, string> questionTypes;

        private void LoadQuestionType()
        {
            questionTypes = new Dictionary<string, string>
            {
                { "1", "Multiple Choice" },
                { "2", "True or False" },
                { "3", "Check Box" }
            };
        }

        public Quiz(string name)
        {
            Id = nextId++;
            Name = name;
            questions = new Dictionary<string, Question>();
            SetUpQuiz();
            Correct = 0;
        }

        public Quiz()
        {
            string name = GetQuizName();
            Id = nextId++;
            Name = name;
            questions = new Dictionary<string, Question>();
            SetUpQuiz();
            Correct = 0;
        }

        public string GetQuizName()
        {
            Console.Write("Enter name of quiz: ");
            string name = Console.ReadLine();
            Console.Clear();

            return name;
        }

        public void ShowQuestionTypes()
        {
            LoadQuestionType();
            foreach (KeyValuePair<string, string> types in questionTypes)
            {
                Console.WriteLine("{0}) {1}", types.Key, types.Value);
            }
        }

        public string GetChoice()
        {
            string choice = "";
            do
            {
                Console.WriteLine("[Question Choice] Question we are adding: ");
                ShowQuestionTypes();
                choice = Console.ReadLine();
                Console.Clear();
            } while (!(choice == "1" || choice == "2" || choice == "3"));

            return choice;
        }

        public void DetermineWhichQuestionToMake(string choice)
        {
            Console.Write("[{0}] ", questionTypes[choice]);
            if (choice == "1")
            {
                MultipleChoice newQuestion = new MultipleChoice();
                questions.Add(dictionaryIndex.ToString(), newQuestion);
                dictionaryIndex++;
            }
            else if (choice == "2")
            {
                TrueOrFalse newQuestion = new TrueOrFalse();
                questions.Add(dictionaryIndex.ToString(), newQuestion);
                dictionaryIndex++;
            }
            else
            {
                CheckBox  newQuestion = new CheckBox();
                questions.Add(dictionaryIndex.ToString(), newQuestion);
                dictionaryIndex++;
            }
        }

        public void AddQuestion()
        {
            string choice = GetChoice();
            DetermineWhichQuestionToMake(choice);
        }

        public int ObtainQuestionCount()
        {
            Console.Write("[Question Count] Questions in this quiz: ");
            int questionCount = int.Parse(Console.ReadLine());
            Console.Clear();

            return questionCount;
        }

        public void SetUpQuiz()
        {
            int numberOfQuestions = ObtainQuestionCount();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                AddQuestion();
            }
        }

        public string GetAnswerChoice(Question question)
        {
            string choice = "";
            // TODO implement a check for checkbox
            do
            {
                Console.Write("Answer choice: ");
                choice = Console.ReadLine();
                if (!question.PossibleAnswers.ContainsKey(choice))
                {
                    Console.WriteLine("Answer chosen not in list, try again");
                }
            } while (!question.PossibleAnswers.ContainsKey(choice));

            return choice;
        }

        public void IsCorrectChoice(string choice, Question question)
        {
            if(choice == question.Answer)
            {
                Correct++;
            }
            else
            {
                incorrect.Add(question.Name);
            }
        }

        public double GetGrade()
        {
            double count = Questions.Count;
            double correct = Correct;

            return correct / count;
        }

        public void DisplayIncorrectQuestions()
        {
            foreach (string question in incorrect)
            {
                Console.WriteLine("[Incorrect] {0}", question);
            }
        }

        public void Play()
        {
            foreach (KeyValuePair<string, Question> question in Questions)
            {
                question.Value.DisplayQuestionAndPossibleAnswer();
                string choice = GetAnswerChoice(question.Value);
                IsCorrectChoice(choice, question.Value);
            }
            Console.WriteLine("Grade: {0:P}", GetGrade());
            DisplayIncorrectQuestions();
        }
    }
}
