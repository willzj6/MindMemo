using MindMemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MindMemo.ViewModels
{
    class NumberMemoryViewModel
    {
        public NumberMemory numberMemory { get; set; }

        public string randomNumber { get; set; }

        public bool isCorrect { get; set; }

        public ICommand StartLevelCommand {  get; private set; }

        public int level { get; set; }
        public NumberMemoryViewModel()
        {
            numberMemory = new NumberMemory();
            StartLevelCommand = new RelayCommand(numberMemory.StartLevel());
        }

    }
}
