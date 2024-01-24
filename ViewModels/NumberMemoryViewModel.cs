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

        const int LEVEL_TIMER = 100;

        private readonly MindMemoContext _context = new MindMemoContext(); // Get DBContext

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

        private bool _isGameOverScreenVisible = false;
        public bool IsGameOverScreenVisible
        {
            get { return _isGameOverScreenVisible; }
            set
            {
                if (_isGameOverScreenVisible != value)
                {
                    _isGameOverScreenVisible = value;
                    OnPropertyChanged(nameof(IsGameOverScreenVisible));
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
                        param => predicate(null));
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
                        param => predicate(null));
                }
                return _submitAnswerCommand;
            }
        }

        RelayCommand _tryAgainCommand;
        public ICommand TryAgainCommand
        {
            get
            {
                if (_tryAgainCommand == null)
                {
                    _tryAgainCommand = new RelayCommand(param => TryAgain(),
                        param => predicate(null));
                }
                return _tryAgainCommand;
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
            //RemainingTime--;
            RemainingTime -= 20;

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
                AddScoreToDatabase();
                ShowGameOverScreen();
            }
        }

        private void AddScoreToDatabase()
        {
            DateTime dateTime = DateTime.Now;
            int Score = numberMemory.level;
            string username = "HarryPotter";
            NumberMemoryScore nms = new NumberMemoryScore() { Username = username, Time = dateTime, Score = Score };
            _context.NumberMemoryScores.Add(nms);
            _context.SaveChanges();
        }

        private void TryAgain()
        {
            numberMemory = new NumberMemory();
            ShowStartButton();
        }

        // This this is a horrible set up. Change this ASAP
        private void ShowStartButton()
        {
            IsNumberScreenVisible = false;
            IsAnswerScreenVisible = false;
            IsContinueScreenVisible = false;
            IsStartButtonVisible = true;
            IsGameOverScreenVisible = false;
        }
        private void ShowNumberScreen()
        {
            IsNumberScreenVisible = true;
            IsAnswerScreenVisible = false;
            IsContinueScreenVisible = false;
            IsStartButtonVisible = false;
            IsGameOverScreenVisible = false;
        }

        private void ShowAnswerScreen()
        {
            IsAnswerScreenVisible = true;
            IsNumberScreenVisible= false;
            IsContinueScreenVisible = false;
            IsStartButtonVisible= false;
            IsGameOverScreenVisible = false;
        }

        private void ShowContinueScreen()
        {
            IsContinueScreenVisible = true;
            IsAnswerScreenVisible = false;
            IsStartButtonVisible = false;
            IsNumberScreenVisible = false;
            IsGameOverScreenVisible = false;
        }

        private void ShowGameOverScreen()
        {
            IsGameOverScreenVisible = true;
            IsAnswerScreenVisible = false;
            IsStartButtonVisible = false;
            IsNumberScreenVisible = false;
            IsContinueScreenVisible=false;
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

    public enum NumberMemoryAppScreen
    {
        StartScreen,
        NumberScreen,
        AnswerScreen,
        ContinueScreen,
        GameOverScreen
    }
}
