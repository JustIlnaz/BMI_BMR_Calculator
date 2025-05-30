using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BMI_BMR_Calculator
{
    public partial class BMR : Page
    {
        private string selectedGender = "";
        private MainWindow parentWindow;

        public BMR(MainWindow parent)
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
            femaleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(123, 31, 162));
            femaleButton.BorderThickness = new Thickness(2);
        }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            selectedGender = "female";
            // Визуальная обратная связь
            femaleButton.BorderBrush = new SolidColorBrush(Colors.Pink);
            femaleButton.BorderThickness = new Thickness(3);
            maleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(123, 31, 162));
            maleButton.BorderThickness = new Thickness(2);
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

                if (string.IsNullOrWhiteSpace(heightTextBox.Text) ||
                    string.IsNullOrWhiteSpace(weightTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ageTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double height = Convert.ToDouble(heightTextBox.Text);
                double weight = Convert.ToDouble(weightTextBox.Text);
                int age = Convert.ToInt32(ageTextBox.Text);

                if (height <= 0 || weight <= 0 || age <= 0)
                {
                    MessageBox.Show("Все значения должны быть положительными числами.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (age > 120)
                {
                    MessageBox.Show("Пожалуйста, введите реалистичный возраст.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (height > 300 || height < 50)
                {
                    MessageBox.Show("Пожалуйста, введите реалистичный рост (50-300 см).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (weight > 500 || weight < 20)
                {
                    MessageBox.Show("Пожалуйста, введите реалистичный вес (20-500 кг).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double bmr;

                // Формула Миффлина-Сан Жеора (более точная, чем формула Харриса-Бенедикта)
                if (selectedGender == "male")
                {
                    bmr = 10 * weight + 6.25 * height - 5 * age + 5;
                }
                else
                {
                    bmr = 10 * weight + 6.25 * height - 5 * age - 161;
                }

                // Отображаем BMR
                bmrValueText.Text = bmr.ToString("F0");

                // Рассчитываем калории для разных уровней активности
                double sedentary = bmr * 1.2;      // Сидячий образ жизни
                double light = bmr * 1.375;        // Легкая активность (1-3 дня в неделю)
                double moderate = bmr * 1.55;      // Умеренная активность (3-5 дней в неделю)
                double high = bmr * 1.725;         // Высокая активность (6-7 дней в неделю)
                double extreme = bmr * 1.9;        // Экстремальная активность (2 раза в день, интенсивные тренировки)

                // Обновляем значения в интерфейсе
                sedentaryText.Text = sedentary.ToString("F0") + " кал";
                lightText.Text = light.ToString("F0") + " кал";
                moderateText.Text = moderate.ToString("F0") + " кал";
                highText.Text = high.ToString("F0") + " кал";
                extremeText.Text = extreme.ToString("F0") + " кал";

                // Показываем детальный результат
                string genderText = selectedGender == "male" ? "мужчины" : "женщины";
                string message = $"Результаты расчета для {genderText}:\n\n" +
                               $"Базовый метаболизм (BMR): {bmr:F0} калорий в день\n\n" +
                               "Ежедневная потребность в калориях:\n" +
                               $"• Сидячий образ жизни: {sedentary:F0} кал\n" +
                               $"• Легкая активность: {light:F0} кал\n" +
                               $"• Умеренная активность: {moderate:F0} кал\n" +
                               $"• Высокая активность: {high:F0} кал\n" +
                               $"• Экстремальная активность: {extreme:F0} кал\n\n" +
                               "Это количество калорий необходимо для поддержания текущего веса.";

                MessageBox.Show(message, "Результат BMR", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Введенные значения слишком большие. Пожалуйста, проверьте данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем поля
            ClearFields();

            // Возврат к главному меню
            parentWindow?.ReturnToMainMenu();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            // Подробная информация об уровнях активности
            string info = "Коэффициенты активности для расчета ежедневной потребности в калориях:\n\n" +
                         "🚶 Сидячий образ жизни (1.2)\n" +
                         "Минимум физической активности, работа за столом\n\n" +
                         "🏃 Легкая активность (1.375)\n" +
                         "Легкие упражнения или спорт 1-3 дня в неделю\n\n" +
                         "💪 Умеренная активность (1.55)\n" +
                         "Умеренные упражнения или спорт 3-5 дней в неделю\n\n" +
                         "🔥 Высокая активность (1.725)\n" +
                         "Интенсивные упражнения или спорт 6-7 дней в неделю\n\n" +
                         "⚡ Экстремальная активность (1.9)\n" +
                         "Очень интенсивные упражнения, физическая работа или тренировки 2 раза в день\n\n" +
                         "BMR рассчитывается по формуле Миффлина-Сан Жеора - наиболее точной на сегодняшний день.";

            MessageBox.Show(info, "Информация об уровнях активности", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            heightTextBox.Text = "";
            weightTextBox.Text = "";
            ageTextBox.Text = "";
            bmrValueText.Text = "0";
            sedentaryText.Text = "0 кал";
            lightText.Text = "0 кал";
            moderateText.Text = "0 кал";
            highText.Text = "0 кал";
            extremeText.Text = "0 кал";
            selectedGender = "";

            // Сброс визуальной обратной связи кнопок
            maleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(123, 31, 162));
            maleButton.BorderThickness = new Thickness(2);
            femaleButton.BorderBrush = new SolidColorBrush(Color.FromRgb(123, 31, 162));
            femaleButton.BorderThickness = new Thickness(2);
        }

        // Дополнительный метод для получения рекомендаций по весу
        private string GetWeightRecommendation(double bmr, string gender)
        {
            string recommendation = "\n\nРекомендации:\n";

            if (gender == "male")
            {
                if (bmr < 1500)
                    recommendation += "• Рассмотрите возможность увеличения мышечной массы\n";
                else if (bmr > 2500)
                    recommendation += "• При снижении веса учитывайте высокий базовый метаболизм\n";
                else
                    recommendation += "• У вас нормальный базовый метаболизм\n";
            }
            else
            {
                if (bmr < 1200)
                    recommendation += "• Рассмотрите возможность увеличения мышечной массы\n";
                else if (bmr > 2000)
                    recommendation += "• При снижении веса учитывайте высокий базовый метаболизм\n";
                else
                    recommendation += "• У вас нормальный базовый метаболизм\n";
            }

            recommendation += "• Для похудения создайте дефицит 300-500 калорий от вашего уровня активности\n";
            recommendation += "• Для набора массы добавьте 300-500 калорий к вашему уровню активности\n";
            recommendation += "• Консультируйтесь с врачом перед изменением рациона";

            return recommendation;
        }
    }
}