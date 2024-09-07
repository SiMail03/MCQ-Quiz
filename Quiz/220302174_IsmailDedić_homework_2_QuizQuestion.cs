using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class QuizQuestion
{
    public string questionText { get; set; }
    public List<string> multipleChoice { get; set; }
    public int correctAnswerIndex { get; set; }
}
