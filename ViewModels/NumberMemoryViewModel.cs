using MindMemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MindMemo.ViewModels
{
    class NumberMemoryViewModel : INotifyPropertyChanged
    {
        public NumberMemory numberMemory = new NumberMemory();

        const int LEVEL_TIMER = 5;

        private bool isCorrect = true;

        private bool _isStartButtonVisible = true;
        public bool IsStartButtonVisible
        {
            get { return _isStartButtonVisible; }
            set
            {
                if (_isStartButtonVisible != value)
                {
                    _isStartButtonVisible = value;
                    OnPropertyChanged(nameof(IsStartButtonVisible));
                }
            }
        }

        public bool _isNumberScreenVisible = false;
        public bool IsNumberScreenVisible
        {
            get { return _isNumberScreenVisible; }
            set
            {
                if (_isNumberScreenVisible != value)
                {
                    _isNumberScreenVisible = value;
                    OnPropertyChanged(nameof(IsNumberScreenVisible));
                }
            }
        }

        public bool _isAnswerScreenVisible = false;
        public bool IsAnswerScreenVisible
        {
            get { return _isAnswerScreenVisible; }
            set
            {
                if (_isAnswerScreenVisible != value)
                {
                    _isAnswerScreenVisible = value;
                    OnPropertyChanged(nameof(IsAnswerScreenVisible));
                }
            }
        }

        private bool _isContinueScreenVisible = false;
        public bool IsContinueScreenVisible
        {
            get { return _isContinueScreenVisible; }
            set
            {
                if (_isContinueScreenVisible != value)
                {
                    _isContinueScreenVisible = value;
                    OnPropertyChanged(nameof(IsContinueScreenVisible));
                }
            }
        }

        private bool _isFailScreenVisible = false;
        public bool IsFailScreenVisible
        {
            get { return _isFailScreenVisible; }
            set
            {
                if (_isFailScreenVisible != value)
                {
                    _isFailScreenVisible= value;
                    OnPropertyChanged(nameof(IsFailScreenVisible));
                }
            }
        }

        private string _randomNumbers = "";
        public string RandomNumbers
        {
            get { return _randomNumbers; }
            set
            {
                if (_randomNumbers != value)
                {
                    _randomNumbers = value;
                    OnPropertyChanged(nameof(RandomNumbers));
                }
            }
        }

        private string _userAnswer;
        public string UserAnswer
        {
            get { return _userAnswer; }
            set
            {
                if (_userAnswer != value)
                {
                    _userAnswer = value;
                    OnPropertyChanged(nameof(UserAnswer));
                }
            }
        }

        private string _level;
        public string Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        private DispatcherTimer timer;

        private int _remainingTime = LEVEL_TIMER;

        public int RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                if (_remainingTime != value)
                {
                    _remainingTime = value;
                    OnPropertyChanged(nameof(RemainingTime));
                }
            }
        }

        RelayCommand _startLevelCommand;
        public ICommand StartLevelCommand
        {
            get
            {
                if (_startLevelCommand == null)
                {
                    _startLevelCommand = new RelayCommand(param => StartLevel(),
                        param => predicate(isCorrect));
                }
                return _startLevelCommand;
            }
        }

        RelayCommand _submitAnswerCommand;
        public ICommand SubmitAnswerCommand
        {
            get
            {
                if (_submitAnswerCommand == null)
                {
                    _submitAnswerCommand = new RelayCommand(param => SubmitAnswer(),
                        param => predicate(isCorrect));
                }
                return _submitAnswerCommand;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void StartLevel()
        {
            ShowNumberScreen();
            UserAnswer = "";
            numberMemory.StartLevel();
            RandomNumbers = numberMemory.randomNumbers;
            Level = "Level " + numberMemory.level.ToString();
            RemainingTime = LEVEL_TIMER;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RemainingTime--;

            // Update the time bar value
            //timeBar.Value = remainingTime;

            if (RemainingTime <= 0)
            {
                // Time is up, stop the timer
                timer.Stop();

                // Call your method or trigger the desired action
                ShowAnswerScreen();
            }
        }

        public void SubmitAnswer()
        {
            numberMemory.CheckAnswer(UserAnswer);
            if (numberMemory.isCorrect)
            {
                ShowContinueScreen();
            }
            else
            {
                
            }
        }

        private void ShowNumberScreen()
        {
            IsNumberScreenVisible = true;
            IsAnswerScreenVisible = false;
            IsContinueScreenVisible = false;
            IsStartButtonVisible = false;
        }

        private void ShowAnswerScreen()
        {
            IsAnswerScreenVisible = true;
            IsNumberScreenVisible= false;
            IsContinueScreenVisible = false;
            IsStartButtonVisible= false;
        }

        private void ShowContinueScreen()
        {
            IsContinueScreenVisible = true;
            IsAnswerScreenVisible = false;
            IsStartButtonVisible = false;
            IsNumberScreenVisible = false;
        }

        public bool predicate(object param)
        {
            return true;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
