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
        public string randomNumbers { get; set; } = string.Empty;

        public bool isCorrect = true;

        public void StartLevel()
        {
            level += 1;
            GenerateRandomNumbers();
        }

        public void GenerateRandomNumbers()
        {
            Random rnd = new Random();
            randomNumbers = rnd.Next(10 ^ (level - 1), 10 ^ level).ToString();
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
