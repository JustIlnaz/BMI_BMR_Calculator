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
using System.Windows.Shapes;

namespace BMI_BMR_Calculator
{
    /// <summary>
    /// Логика взаимодействия для BMR.xaml
    /// </summary>
    public partial class BMR : Page
    {
        public BMR()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сидячий образ: Нет работы или работа за столом \n Маленькая активность: Мало физических работы или занятия спортом 1-3 раза в неделю \n Средняя активность: Умеренная физическая работа или занятия спортом 3 - 5 дней в неделю \n Сильная активность: Сильные физическая нагрузка или занятия спортом 6 - 7 дней в неделю \n Максимальная активность: Сильная ежедневная физическая нагрузка или спорт и физическая работа");
        }

        private void CalculateBMR(object sender, RoutedEventArgs e)
        {

        }

        private void Man(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Rost.Text, out double rost) && double.TryParse(Weight.Text, out double WEight) && double.TryParse(OLD.Text, out double old))
            {
                double height = rost;
                double weight = WEight;
                double Age = old;
                double bmr = 66 + (13.7 * weight) + (5 * height) - (6.8 * old);
                YourBMR.Text = bmr.ToString();
                ManBm1.Text = (bmr * 1.2).ToString();
                ManBm2.Text = (bmr * 1.375).ToString();
                ManBm3.Text = (bmr * 1.55).ToString();
                ManBm4.Text = (bmr * 1.725).ToString();
                ManBm5.Text = (bmr * 1.9).ToString();

            }
        }
        private void Woman(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Rost.Text, out double rost) && double.TryParse(Weight.Text, out double WEight) && double.TryParse(OLD.Text, out double old))
            {
                double height = rost;
                double weight = WEight;
                double Age = old;
                double bmr = 65 + (9.6 * weight) + (1.8 * height) - (4.7 * old);
                YourBMR.Text = bmr.ToString();
                ManBm1.Text = (bmr * 1.2).ToString();
                ManBm2.Text = (bmr * 1.375).ToString();
                ManBm3.Text = (bmr * 1.55).ToString();
                ManBm4.Text = (bmr * 1.725).ToString();
                ManBm5.Text = (bmr * 1.9).ToString();
            }
}
