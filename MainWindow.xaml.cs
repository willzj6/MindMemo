using MindMemo.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MindMemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new HomeViewModel();

        }

        private void NumberMemory_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NumberMemoryViewModel();

        }

        private void NumberMemoryScores_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NumberMemoryDashboardViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new NumberMemoryViewModel();
        }
    }
}