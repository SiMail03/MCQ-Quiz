using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Timers;


public class Quiz
{
    //private System.Timers.Timer questionTimer;

    private string json; // Path to the JSON file
    private List<QuizQuestion> questions; // List to hold QuizQuestions
    //private int defaultQuestionTimeInSeconds = 3;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int incorrectAnswers = 0;

    public Quiz()
    {
        Console.CancelKeyPress += OnExit; // Subscribe to exit event
    }
    private void OnExit(object sender, ConsoleCancelEventArgs args)
    {
        Console.WriteLine("Progress saved. Exiting quiz.");
    }

    // Load questions from the JSON file
    private void loadQuestionsFromJson()
    {
        int userInput = chooseDifficulty();
        if(userInput == 1)
        {
         json = "chapter6.json"; 
             string parsText = File.ReadAllText(json); // Read all text from JSON file
            questions = JsonSerializer.Deserialize<List<QuizQuestion>>(parsText); // Deserialize JSON to List<QuizQuestion>
            validateQuestions(); // Validate loaded questions
        }
        else if (userInput == 2)
        {
            json = "chapter7.json";
            string parsText = File.ReadAllText(json); // Read all text from JSON file
            questions = JsonSerializer.Deserialize<List<QuizQuestion>>(parsText); // Deserialize JSON to List<QuizQuestion>
            validateQuestions(); // Validate loaded questions
        }
        else if (userInput == 3)
        {
            json = "chapter11.json";
            string parsText = File.ReadAllText(json); // Read all text from JSON file
            questions = JsonSerializer.Deserialize<List<QuizQuestion>>(parsText); // Deserialize JSON to List<QuizQuestion>
            validateQuestions(); // Validate loaded questions
        }
        else if (userInput == 4)
        {
            json = "chapter12.json";
            string parsText = File.ReadAllText(json); // Read all text from JSON file
            questions = JsonSerializer.Deserialize<List<QuizQuestion>>(parsText); // Deserialize JSON to List<QuizQuestion>
            validateQuestions(); // Validate loaded questions
        }
        else if (userInput == 5)
        {
            json = "chapter14.json";
            string parsText = File.ReadAllText(json); // Read all text from JSON file
            questions = JsonSerializer.Deserialize<List<QuizQuestion>>(parsText); // Deserialize JSON to List<QuizQuestion>
            validateQuestions(); // Validate loaded questions
        }

    }

    // Validate the loaded questions
    private void validateQuestions()
    {
      /*  // Check if the number of loaded questions is not 5
        if (questions.Count != 5)
        {
            Console.WriteLine("Number of questions needs to be exactly 5!");
            Environment.Exit(1); // Exit the program due to incorrect question count
        }*/

        // Validate each individual question
        foreach (var question in questions)
        {
            if (!IsValidQuestion(question))
            {
                Console.WriteLine("Invalid question format!");
                Environment.Exit(1); // Exit the program due to invalid question format
            }
        }
    }

    // Check if a single QuizQuestion is valid
    private bool IsValidQuestion(QuizQuestion question)
    {
        return question.multipleChoice.Count == 4 || question.multipleChoice.Count == 2  &&// Check if there are exactly 4 choices
               question.correctAnswerIndex >= 0 && // Check if the correct answer index is non-negative
               question.correctAnswerIndex < question.multipleChoice.Count && // Check if the correct answer index is within the choices range
               !string.IsNullOrWhiteSpace(question.questionText); // Check if the question text is not null or empty
    }

    // Start the quiz
    public void startQuiz()
    {
        loadQuestionsFromJson(); // Load questions from JSON file

        questions = RandomPermutation(questions);

        //questionTimer = new System.Timers.Timer();
        //questionTimer.Interval = defaultQuestionTimeInSeconds * 1000;
        //questionTimer.Elapsed += OnQuestionTimerElapsed;


        Console.WriteLine("Welcome to Computer Programming Quiz!");
        Console.WriteLine("Answer the following questions:");
        AskNextQuestion();
    }

        private void AskNextQuestion()
        {
        Thread.Sleep(500);
        Console.Clear();

            if (currentQuestionIndex < questions.Count)
            {
                QuizQuestion currentQuestion = questions[currentQuestionIndex];
                Console.WriteLine($"\nQuestion: {currentQuestion.questionText}");

                for (int i = 0; i < currentQuestion.multipleChoice.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentQuestion.multipleChoice[i]}");
                }

            int userAnswer = UserInput(currentQuestion.multipleChoice.Count); // Get user's answer

        }
            else
            {
                generateResult(correctAnswers);
            }
        }

        //private void OnQuestionTimerElapsed(object sender, ElapsedEventArgs e)
        //{
        //    //questionTimer.Stop();
        //    Console.WriteLine("Time's up!");
        //    HandleAnswer(-1); // Time's up, consider the answer as incorrect (-1)
        //    currentQuestionIndex++;
        //    AskNextQuestion();
        //}

        public int UserInput(int numberOfChoices)
        {
            int answer;
        Console.Write("Your Answer: ");

        while (!int.TryParse(Console.ReadLine(), out answer) || answer < 1 || answer > numberOfChoices)
            {
                Console.WriteLine("Invalid input!");
                Console.Write("Your Answer: ");
            }

            HandleAnswer(answer);
            return answer;
        }

        private void HandleAnswer(int userAnswer)
        {
            //questionTimer.Stop();

            QuizQuestion currentQuestion = questions[currentQuestionIndex];

            if (userAnswer == currentQuestion.correctAnswerIndex + 1)
            {
                Console.WriteLine("Correct!");
                correctAnswers++;

        }
        else
            {
                Console.WriteLine($"Incorrect! Correct answer is: {currentQuestion.multipleChoice[currentQuestion.correctAnswerIndex]}");
            Thread.Sleep(5000);

            incorrectAnswers++;
            }

            currentQuestionIndex++;
        generateResult(correctAnswers, incorrectAnswers);
            AskNextQuestion();
        }

        // Display quiz results
        private void generateResult(int correctAnswers)
    {
        Console.WriteLine("\nFinal results:");
        Console.WriteLine("Correct answers: " + correctAnswers);
        Console.WriteLine("Incorrect answers: " + (questions.Count - correctAnswers));
    }

    private void generateResult(int correctAnswers, int incorrectAnswers)
    {
        Console.WriteLine("\nCurrent results:");
        Console.WriteLine("Correct answers: " + correctAnswers);
        Console.WriteLine("Incorrect answers: " + incorrectAnswers);
    }

    private int chooseDifficulty()
    {
        Console.WriteLine("Choose chapter: \n 1.Chapter 6 \n 2.Chapter 7 \n 3.Chapter 11 \n 4.Chapter 12 \n 5.Chapter 14");

        int userInput;

        while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 1 || userInput > 5)
        {
           Console.WriteLine("Invalid input!");
        }
        return userInput;
    }

    public List<QuizQuestion> RandomPermutation<QuizQuestion>(List<QuizQuestion> array)
    {
        Random random = new Random();
        List<QuizQuestion> retArray = new List<QuizQuestion>(array);

        int maxIndex = array.Count - 1;

        for (int i = 0; i <= maxIndex; i++)
        {
            int swapIndex = random.Next(i, maxIndex);
            if (swapIndex != i)
            {
                QuizQuestion temp = retArray[i];
                retArray[i] = retArray[swapIndex];
                retArray[swapIndex] = temp;
            }
        }
        return retArray;
    }
   

}
