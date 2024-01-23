using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMemo.Models
{
    class NumberMemory
    {
        public int level { get; set; } = 0;
        public string randomNumbers { get; set; } = "";

        public bool isCorrect = true;

        public void StartLevel()
        {
            this.level += 1;
            GenerateRandomNumbers();
        }

        public void GenerateRandomNumbers()
        {
            Random rnd = new Random();
            this.randomNumbers = rnd.Next((int)Math.Pow(10, level-1), (int)Math.Pow(10, level) -1).ToString();
        }

        public void CheckAnswer(string answer)
        {
            if (!answer.Equals(randomNumbers))
            {
                isCorrect = false;
            }
        }
    }
}
