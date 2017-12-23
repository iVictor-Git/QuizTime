using System;
using System.Collections.Generic;
using System.Text;

namespace QuizTime
{
    abstract class Question
    {
        protected static int nextId = 0;
        protected int Id { get; set; }
        public string Name { get; protected set; }
        protected Dictionary<string, string> possibleAnswers = new Dictionary<string, string>();
        public Dictionary<string, string> PossibleAnswers
        {
            get { return possibleAnswers; }
        }
        public string Answer { get; protected set; }
        public string GetAnswer()
        {
            return Answer;
        }


        public Question(string name)
        {
            Id = nextId++;
            Name = name;
            Answer = "";
        }

        public Question()
        {
            Id = nextId++;
            Name = BuildQuestion();
            Answer = "";
        }

        public override string ToString()
        {
            string self = MyToStringBuilder();
            self += "Answer: " + Answer + ") " + PossibleAnswers[Answer];
            return self;
        }

        protected string MyToStringBuilder()
        {
            string self = "\n";
            self += "Question name: " + Name + "\n";
            self += "Possible answers: \n";
            foreach (KeyValuePair<string, string> answer in PossibleAnswers)
            {
                self += answer.Key + ") " + answer.Value + "\n";
            }
            return self;
        }

        public static string BuildQuestion()
        {
            string sentence;
            do
            {
                Console.Write("Question you are trying to ask: ");
                sentence = Console.ReadLine();
            } while (String.IsNullOrEmpty(sentence));

            Console.Clear();
            return sentence;
        }

        public void BuildAnswers()
        {
            int choices = -1;
            do
            {
                Console.Write("[Answer Builder] Possible Answers to \"{0}\": ", Name);
                choices = int.Parse(Console.ReadLine());
            } while (choices <= 0);

            for (int i = 0; i < choices; i++)
            {
                Console.Write("Answer {0}: ", (i + 1).ToString());
                possibleAnswers.Add((i+1).ToString(), Console.ReadLine());
            }

            Console.Clear();
        }

        public virtual void SetAnswer()
        {
            
            do
            {
                Console.WriteLine("[Set Answer] Which of the following is the answer to \"{0}\": ", Name);
                DisplayPossibleAnswers();
                Console.Write("[Choice]: ");
                Answer = Console.ReadLine();
                Console.Clear();
            } while (!possibleAnswers.ContainsKey(Answer) || String.IsNullOrEmpty(Answer));;
        }

        public void BuildAnswerAndQuestion()
        {
            BuildAnswers();
            SetAnswer();
        }

        public void DisplayQuestionName()
        {
            Console.WriteLine("[Question prompt] {0}", Name);
        }

        public void DisplayPossibleAnswers()
        {
            foreach (KeyValuePair<string, string> answers in possibleAnswers)
            {
                Console.WriteLine("{0}) {1}", answers.Key, answers.Value);
            }
        }

        public void DisplayQuestionAndPossibleAnswer()
        {
            DisplayQuestionName();
            DisplayPossibleAnswers();
        }

    }

    class MultipleChoice : Question
    {
        public MultipleChoice(string name) : base(name)
        {
            BuildAnswerAndQuestion();
        }

        public MultipleChoice() : base()
        {
            BuildAnswerAndQuestion();
        }
    }

    class TrueOrFalse : Question
        {
            public TrueOrFalse(string name) : base(name)
            {
                Name = name;
                possibleAnswers.Add("1", "True");
                possibleAnswers.Add("2", "False");
                SetAnswer();
            }

            public TrueOrFalse() : base()
            {
                possibleAnswers.Add("1", "True");
                possibleAnswers.Add("2", "False");
                SetAnswer();
            }

        }

    class CheckBox : Question
    {
        public CheckBox(string name) : base(name)
        {
            BuildAnswerAndQuestion();
        }

        public CheckBox() : base()
        {
            BuildAnswerAndQuestion();
        }

        public override void SetAnswer()
        {
            Console.WriteLine("[Set Answer] Of the following answers, which is the correct answer(s)? (Separate by comma)");
            DisplayPossibleAnswers();
            string[] answers = Console.ReadLine().Split(',');
            foreach (string answer in answers)
            {
                Answer += answer.Trim();
            }

        }

        public override string ToString()
        {
            string self = MyToStringBuilder();
            self += "Answers: ";
            foreach (char character in Answer)
            {
                self += character + ", ";
            }

            return self;
        }
    }
 }

