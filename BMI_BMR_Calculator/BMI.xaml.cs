using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BMI_BMR_Calculator
{
    public partial class BMI : Page
    {
        private string selectedGender = "";
        private MainWindow parentWindow;

        public BMI(MainWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
        }

        private void MaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "male";
            // Визуальная обратная связь
            maleButton.BorderBrush = new SolidColorBrush(Colors.Blue);
            maleButton.BorderThickness = new Thickness(3);
            femaleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            femaleButton.BorderThickness = new Thickness(2);
        }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "female";
            // Визуальная обратная связь
            femaleButton.BorderBrush = new SolidColorBrush(Colors.Pink);
            femaleButton.BorderThickness = new Thickness(3);
            maleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(25, 118, 210));
            maleButton.BorderThickness = new Thickness(2);
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
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double height = Convert.ToDouble(heightTextBox.Text);
                double weight = Convert.ToDouble(weightTextBox.Text);

                if (height <= 0 || weight <= 0)
                {
                    MessageBox.Show("Рост и вес должны быть положительными числами.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Расчет BMI (рост в метрах)
                double heightInMeters = height / 100;
                double bmi = weight / (heightInMeters * heightInMeters);

                // Отображение результата
                bmiValueText.Text = bmi.ToString("F1");

                // Определение категории и изменение изображения
                string category;
                string imagePath;
                Color arrowColor;
                double arrowPosition;

                if (bmi < 18.5)
                {
                    category = "Недостаточный вес";
                    imagePath = "image/bmi-underweight-icon.png";
                    arrowColor = Colors.LightBlue;
                    arrowPosition = 50;
                }
                else if (bmi >= 18.5 && bmi < 25)
                {
                    category = "Здоровый вес";
                    imagePath = "image/bmi-healthy-icon.png";
                    arrowColor = Colors.Green;
                    arrowPosition = 130;
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    category = "Избыточный вес";
                    imagePath = "image/bmi-overweight-icon.png";
                    arrowColor = Colors.Orange;
                    arrowPosition = 210;
                }
                else
                {
                    category = "Ожирение";
                    imagePath = "image/bmi-obese-icon.png";
                    arrowColor = Colors.Red;
                    arrowPosition = 290;
                }

                // Обновление изображения и текста
                try
                {
                    resultImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imagePath, UriKind.Relative));
                }
                catch
                {
                    // Если изображение не найдено, используем стандартное
                    resultImage.Source = null;
                }

                categoryText.Text = category;

                // Позиционирование стрелки на шкале
                bmiArrow.Visibility = Visibility.Visible;
                System.Windows.Controls.Canvas.SetLeft(bmiArrow, arrowPosition);
                bmiArrow.Fill = new SolidColorBrush(arrowColor);

                // Показать результат с дополнительной информацией
                string message = $"Ваш BMI: {bmi:F1}\nКатегория: {category}\n\n";

                if (bmi < 18.5)
                {
                    message += "Рекомендация: Обратитесь к врачу для составления плана набора веса.";
                }
                else if (bmi >= 18.5 && bmi < 25)
                {
                    message += "Поздравляем! У вас здоровый вес. Поддерживайте активный образ жизни.";
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    message += "Рекомендация: Рассмотрите возможность снижения веса через диету и упражнения.";
                }
                else
                {
                    message += "Рекомендация: Обязательно обратитесь к врачу для составления плана снижения веса.";
                }

                MessageBox.Show(message, "Результат BMI", MessageBoxButton.OK, MessageBoxImage.Information);

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
            // Возврат к главному меню
            parentWindow?.ReturnToMainMenu();
        }
    }
}