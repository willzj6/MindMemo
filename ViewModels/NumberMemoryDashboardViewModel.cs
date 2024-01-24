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
        public SeriesCollection AverageScores { get; set; }
        public ObservableCollection<string> DateLabels { get; set; }

        public Func<double, string> AverageScoreFormatter { get; set; }

        private readonly MindMemoContext _context = new MindMemoContext();

        public NumberMemoryDashboardViewModel()
        {
            // Initialize your properties here
            AverageScores = new SeriesCollection();
            DateLabels = new ObservableCollection<string>();
            AverageScoreFormatter = value => value.ToString("0"); // Format as needed

            // Populate your data
            // Example data:
            PopulateChartData();
        }

        private void PopulateChartData()
        {
            // Fetch your data for the last 7 days and populate the properties
            // Calculate average scores and add them to the AverageScores collection
            // Add corresponding day labels to the Days collection

            DateLabels = new ObservableCollection<string>(
                Enumerable.Range(0, 7)
                .Select(offset => DateTime.Today.AddDays(-offset).ToString("yyyy-MM-dd"))
                .Reverse()
            );

            DateLabels = new ObservableCollection<string>() { "first", "second", "third" };

            // Get the current date and calculate the date 7 days ago
            DateTime endDate = DateTime.Today;
            DateTime startDate = endDate.AddDays(-6); // 7 days ago

            // Initialize lists to store date labels and average scores
            List<string> dateLabels = new List<string>();
            List<double> averageScores = new List<double>();

            // Fetch data from the database for the last 7 days
            var query = _context.NumberMemoryScores
                .Where(entry => entry.Time >= startDate && entry.Time <= endDate)
                .GroupBy(entry => entry.Time.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    AverageScore = group.Average(entry => entry.Score)
                })
                .OrderBy(result => result.Date)
                .ToList();

            // Populate date labels and average scores
            foreach (var result in query)
            {
                dateLabels.Add(result.Date.ToString("yyyy-MM-dd"));
                averageScores.Add((double)result.AverageScore);
            }

            // Update DateLabels with the generated date labels
            DateLabels.Clear();
            foreach (string label in dateLabels)
            {
                DateLabels.Add(label);
            }

            // Update the SeriesCollection with the calculated average scores
            AverageScores.Clear();
            AverageScores.Add(new LineSeries
            {
                Title = "Average Scores",
                Values = new ChartValues<double>(averageScores)
            });
        }
    }
}
