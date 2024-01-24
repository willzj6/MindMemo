using LiveCharts.Wpf;
using LiveCharts;
using MindMemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;

namespace MindMemo.Views
{
    /// <summary>
    /// Interaction logic for NumberMemoryDashboardView.xaml
    /// </summary>
    public partial class NumberMemoryDashboardView : UserControl
    {
        private readonly MindMemoContext _context = new MindMemoContext();
        public NumberMemoryDashboardView()
        {
            InitializeComponent();
            ScoreChart();
        }

        private void ScoreChart()
        {
            DateTime endDate = DateTime.Today; // Current date
            DateTime startDate = endDate.AddDays(-6); // 7 days ago

            List<DateTime> dateRange = Enumerable.Range(0, 7)
                .Select(offset => endDate.AddDays(-offset))
                .ToList();

            List<double?> selectWeeklyAverageScores = dateRange
                .GroupJoin(
                    _context.NumberMemoryScores
                        .Where(entry => entry.Time >= startDate && entry.Time <= endDate), // Filter by date range
                    date => date.Date,
                    entry => entry.Time.Date,
                    (date, entries) => entries.Any() ? entries.Average(entry => entry.Score) : 0
                )
                .ToList();

            List<double> nonNullWeeklyAverageScores = selectWeeklyAverageScores.Select(d => d ?? 0.0).ToList();


            LineSeries lines = new LineSeries                               // Create a lineSeries from the share prices 
            {
                Values = new ChartValues<double>(nonNullWeeklyAverageScores)
            };

            numberMemoryChart.Series.Clear();                                        // Clear any existing charts
            numberMemoryChart.Series.Add(lines);                                     // Draw the line chart
        }
    }
}
