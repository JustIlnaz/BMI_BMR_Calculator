using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BMI_BMR_Calculator
{
    public partial class BMI : Page
    {
        private string selectedGender = "";

        public BMI()
        {
            InitializeComponent();
        }

        private void MaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "male";
            maleButton.Background = new SolidColorBrush(Colors.LightBlue);
            femaleButton.Background = new SolidColorBrush(Colors.White);

            // Обновляем изображение результата
            resultImage.Source = new BitmapImage(new Uri("image/male-icon.png", UriKind.Relative));
        }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "female";
            femaleButton.Background = new SolidColorBrush(Colors.LightPink);
            maleButton.Background = new SolidColorBrush(Colors.White);

            // Обновляем изображение результата
            resultImage.Source = new BitmapImage(new Uri("image/female-icon.png", UriKind.Relative));
        }

        private void CalculateBMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedGender))
                {
                    MessageBox.Show("Пожалуйста, выберите пол.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(heightTextBox.Text) || string.IsNullOrWhiteSpace(weightTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, введите рост и вес.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double height = Convert.ToDouble(heightTextBox.Text) / 100; // конвертируем см в метры
                double weight = Convert.ToDouble(weightTextBox.Text);

                if (height <= 0 || weight <= 0)
                {
                    MessageBox.Show("Рост и вес должны быть положительными числами.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double bmi = weight / (height * height);
                bmiValueText.Text = bmi.ToString("F1");

                // Определяем категорию и обновляем UI
                string category;
                double arrowPosition;

                if (bmi < 18.5)
                {
                    category = "Недостаточный";
                    arrowPosition = 75; // позиция для первого сегмента
                }
                else if (bmi < 25)
                {
                    category = "Здоровый";
                    arrowPosition = 150; // позиция для второго сегмента
                }
                else if (bmi < 30)
                {
                    category = "Избыточный";
                    arrowPosition = 225; // позиция для третьего сегмента
                }
                else
                {
                    category = "Ожирение";
                    arrowPosition = 300; // позиция для четвертого сегмента
                }

                categoryText.Text = category;
                Canvas.SetLeft(bmiArrow, arrowPosition);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся к главному окну
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                // Восстанавливаем главное окно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                parentWindow.Close();
            }
        }

        // Дополнительный метод для очистки полей
        private void ClearFields()
        {
            heightTextBox.Text = "";
            weightTextBox.Text = "";
            bmiValueText.Text = "0.0";
            categoryText.Text = "Здоровый";
            selectedGender = "";
            maleButton.Background = new SolidColorBrush(Colors.White);
            femaleButton.Background = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(bmiArrow, 150);
        }
    }
}