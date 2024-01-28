using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using MindMemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMemo.ViewModels
{
    class NumberMemoryDashboardViewModel
    {
        private ChartValues<double> _averageScores;
        public ChartValues<double> AverageScores
        {
            get { return _averageScores; }
            set
            {
                _averageScores = value;
                OnPropertyChanged(nameof(AverageScores));
            }
        }

        private List<string> _dateLabels;
        public List<string> DateLabels {
            get { return _dateLabels; }
            set
            {
                _dateLabels = value;
                OnPropertyChanged(nameof(DateLabels));
            }
        }

        public Func<double, string> AverageScoreFormatter { get; set; }

        private readonly MindMemoContext _context = new MindMemoContext();

        public NumberMemoryDashboardViewModel()
        {
            // Initialize your properties here
            AverageScores = new ChartValues<double>();
            DateLabels = new List<string>();
            AverageScoreFormatter = value => value.ToString("0.0"); // Format as needed
            PopulateChartData();
        }

        private void PopulateChartData()
        {
            // Fetch your data for the last 7 days and populate the properties
            // Calculate average scores and add them to the AverageScores collection
            // Add corresponding day labels to the Days collection

            var averageDailyScoresWeeklyQuery = _context.NumberMemoryScores
                .Where(entry => entry.Time >= DateTime.Today.AddDays(-14))
                .GroupBy(entry => entry.Time.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    AverageScore = group.Average(entry => entry.Score)
                })
                .OrderBy(result => result.Date).ToList();

            List<double> averageDailyScoresWeekly = averageDailyScoresWeeklyQuery.Select(result => result.AverageScore.GetValueOrDefault()).ToList();
            AverageScores.AddRange(averageDailyScoresWeekly);
            List<string> dateLabels = averageDailyScoresWeeklyQuery.Select(result => result.Date.ToString("MM-dd")).ToList();
            DateLabels.AddRange(dateLabels);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
