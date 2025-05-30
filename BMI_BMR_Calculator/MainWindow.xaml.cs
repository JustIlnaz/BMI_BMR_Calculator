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

        // Переход к BMR калькулятору (открытие нового окна)
        private void BmrButton_Click(object sender, RoutedEventArgs e)
        {
            BMR bmrWindow = new BMR();
            bmrWindow.Show();
            // Если хотите закрыть главное окно при открытии BMR:
            // this.Close();
        }

        // Переход к BMI калькулятору (навигация внутри текущего окна)
        private void BmiButton_Click(object sender, RoutedEventArgs e)
        {
            // Вариант 1: Замена содержимого главного окна на BMI страницу
            BMI bmiPage = new BMI();

            // Создаем Frame для навигации
            Frame navigationFrame = new Frame();
            navigationFrame.Navigate(bmiPage);

            // Заменяем содержимое главного окна на Frame
            this.Content = navigationFrame;

            // Вариант 2 (альтернативный): Открыть BMI в новом окне
            // Window bmiWindow = new Window();
            // bmiWindow.Content = new BMI();
            // bmiWindow.Title = "BMI Calculator";
            // bmiWindow.Height = 450;
            // bmiWindow.Width = 800;
            // bmiWindow.Show();
        }
    }
}