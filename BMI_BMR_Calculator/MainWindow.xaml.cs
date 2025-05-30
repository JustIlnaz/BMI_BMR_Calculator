using System;
using System.Windows;
using System.Windows.Controls;

namespace BMI_BMR_Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BMIButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к BMI калькулятору через Frame
            mainMenu.Visibility = Visibility.Collapsed;
            mainFrame.Visibility = Visibility.Visible;
            mainFrame.Navigate(new BMI(this));
        }

        private void BMRButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к BMR калькулятору через Frame
            mainMenu.Visibility = Visibility.Collapsed;
            mainFrame.Visibility = Visibility.Visible;
            mainFrame.Navigate(new BMR(this));
        }

        // Метод для возврата к главному меню из BMI
        public void ReturnToMainMenu()
        {
            mainFrame.Visibility = Visibility.Collapsed;
            mainMenu.Visibility = Visibility.Visible;
            mainFrame.Content = null;
        }
    }
}