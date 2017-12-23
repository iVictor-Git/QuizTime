using System;

namespace QuizTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Quiz newQuiz = new Quiz();
            newQuiz.Play();
            Console.ReadLine();
        }
    }
}
