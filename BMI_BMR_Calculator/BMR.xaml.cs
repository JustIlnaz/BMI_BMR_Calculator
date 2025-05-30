using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BMI_BMR_Calculator
{
    public partial class BMR : Window
    {
        private string selectedGender = "";

        public BMR()
        {
            InitializeComponent();
        }

        // Добавляем обработчики для кнопок выбора пола
        private void MaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "male";
            // Визуальная обратная связь - можно добавить изменение цвета кнопки
        }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "female";
            // Визуальная обратная связь - можно добавить изменение цвета кнопки
        }

        private void CalculateBMR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedGender))
                {
                    MessageBox.Show("Пожалуйста, выберите пол.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Rost.Text) ||
                    string.IsNullOrWhiteSpace(Weight.Text) ||
                    string.IsNullOrWhiteSpace(OLD.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double height = Convert.ToDouble(Rost.Text);
                double weight = Convert.ToDouble(Weight.Text);
                int age = Convert.ToInt32(OLD.Text);

                if (height <= 0 || weight <= 0 || age <= 0)
                {
                    MessageBox.Show("Все значения должны быть положительными.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double bmr;

                // Формула Миффлина-Сан Жеора
                if (selectedGender == "male")
                {
                    bmr = 10 * weight + 6.25 * height - 5 * age + 5;
                }
                else
                {
                    bmr = 10 * weight + 6.25 * height - 5 * age - 161;
                }

                // Отображаем BMR
                YourBMR.Text = bmr.ToString("F0");

                // Рассчитываем калории для разных уровней активности
                ManBm1.Text = (bmr * 1.2).ToString("F0"); // Сидячий образ жизни
                ManBm2.Text = (bmr * 1.375).ToString("F0"); // Легкая активность
                ManBm3.Text = (bmr * 1.55).ToString("F0"); // Умеренная активность
                ManBm4.Text = (bmr * 1.725).ToString("F0"); // Высокая активность
                ManBm5.Text = (bmr * 1.9).ToString("F0"); // Экстремальная активность
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
            // Закрываем текущее окно и возвращаемся к главному
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Информационное окно о коэффициентах активности
            string info = "Коэффициенты активности:\n\n" +
                         "Сидячий (1.2) - минимум физической активности\n" +
                         "Маленькая активность (1.375) - легкие упражнения 1-3 дня в неделю\n" +
                         "Средняя активность (1.55) - умеренные упражнения 3-5 дней в неделю\n" +
                         "Сильная активность (1.725) - интенсивные упражнения 6-7 дней в неделю\n" +
                         "Максимальная активность (1.9) - очень интенсивные упражнения, физическая работа";

            MessageBox.Show(info, "Информация об уровнях активности", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Метод для очистки полей
        private void ClearFields()
        {
            Rost.Text = "";
            Weight.Text = "";
            OLD.Text = "";
            YourBMR.Text = "";
            ManBm1.Text = "";
            ManBm2.Text = "";
            ManBm3.Text = "";
            ManBm4.Text = "";
            ManBm5.Text = "";
            selectedGender = "";
        }
    }
}