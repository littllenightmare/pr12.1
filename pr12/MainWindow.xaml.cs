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
using System.Windows.Threading;

namespace pr12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;//Описываем таймер
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();//добавляем таймер
            timer.Tick += Timer_Tick;//добавляем ему тики
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);//каждую секунду таймер будет тикать
            timer.IsEnabled = true;//таймер включен
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
           DateTime date = DateTime.Now;//создание объекта
            tbtime.Text = date.ToString("HH:mm");//текстблок со временем
            tbdate.Text = date.ToString("dd:MM:yyyy");//текстблок с датой
        }

        private void infoClick(object sender, RoutedEventArgs e)//инфокнопка
        {
            MessageBox.Show("Выполнено Кульковой Ангелиной.\r\n Даны координаты трех вершин треугольника: (x1, y1), (x2, y2), (x3, y3). Найти его периметр и площадь, используя окнолу для расстояния между двумя точками на плоскости (см. задание 12).\r\n Для нахождения площади треугольника со сторонами a, b, c использовать окнолу Герона: S = p(p − a)( p −b)( p − c), где p = (a + b + c)/2 —полупериметр.\r\nДано трехзначное число. Найти сумму и произведение его цифр.");
        }

        private void exitClick(object sender, RoutedEventArgs e)//кнопка выхода
        {
            this.Close();
        }

        private void rasClick(object sender, RoutedEventArgs e)
        {
            if(tabControl.SelectedItem == secondpr)//если выбрана вторая задача
            {
                int ch = 1, sum, pr, a, b, c;
                if(Int32.TryParse(tbDano.Text, out ch) == true)//и есть пользователь ввёл данные
                {
                    if (Convert.ToInt32(tbDano.Text) / 1000 == 0)//если данные корректные, отделяем цифры и считаем сумму и произведение
                    {
                        a = ch % 10;
                        ch = ch / 10;
                        b = ch % 10;
                        ch = ch / 10;
                        c = ch % 10;
                        sum = a + b + c;
                        pr = a * b * c;
                        tbRes.Text = ($"Сумма цифр числа: {sum}, произведение цифр числа: {pr}");//выводим в результаты 
                    }
                }
                else if (tbDano.Text == "")//если данные отсутствуют
                {
                        MessageBox.Show("Вы не ввели данные");
                }
            }
            if(tabControl.SelectedItem == firstpr)//если выбрана первая практическая
            {
                int a, b, c, d, f, g;
                double st1, st2, st3, pl, p;
                if (Int32.TryParse(x1.Text, out a) == true & Int32.TryParse(x2.Text, out a) == true & Int32.TryParse(x3.Text, out a) == true
                     & Int32.TryParse(y1.Text, out a) == true & Int32.TryParse(y2.Text, out a) == true & Int32.TryParse(y3.Text, out a) == true)//если в каждом
                     //окошке нормальные значения
                {
                    a = Convert.ToInt32(x1.Text);
                    b = Convert.ToInt32(x2.Text);
                    c = Convert.ToInt32(x3.Text);
                    d = Convert.ToInt32(y1.Text);
                    f = Convert.ToInt32(y2.Text);
                    g = Convert.ToInt32(y3.Text);
                    st1 = Math.Abs(Math.Pow((b - a), 2) + Math.Pow((f - d), 2));//вычисляем стороны по формуле в задании 12
                    st2 = Math.Abs(Math.Pow((c - b), 2) + Math.Pow((g - f), 2));
                    st3 = Math.Abs(Math.Pow((a - c), 2) + Math.Pow((d - g), 2));
                    p = (st1 + st2 + st3) / 2;//это полупериметр
                    pl = Math.Abs(p * (p - st1) * (p - st2) * (p - st3));//это площадь
                    tbRes.Text = $"Периметр треугольника: {st1 + st2 + st3}, площадь треугольника: {pl}";//выводим в результат
                }
                else MessageBox.Show("Введите корректные данные");//если данные ненормальные
            }
            tbDano.Focus();//передаём фокус окну данных
            x1.Focus();//или первой цифре формулы
        }

        private void tbtch(object sender, TextChangedEventArgs e)//если данные меняются, стираем результат
        {
            tbRes.Text = "";
        }

        private void prMouseLeftButtonUp(object sender, MouseButtonEventArgs e)//при нажатии на панель закладок очищаем результат
        {
            tbRes.Text = "";
            if (tabControl.SelectedItem == firstpr)//если нажали на первую практическую, меняем строку статуса и изменяем видимость элементов
            {
                tbDano.Visibility = Visibility.Hidden;
                x1.Visibility = Visibility.Visible;
                x2.Visibility = Visibility.Visible;
                x3.Visibility = Visibility.Visible;
                y1.Visibility = Visibility.Visible;
                y2.Visibility = Visibility.Visible;
                y3.Visibility = Visibility.Visible;
                tbtask.Text = "Задача №1";
            }
            if (tabControl.SelectedItem == secondpr)//если нажали на вторую практическую, меняем строку статуса и изменяем видимость элементов
            {
                x1.Visibility = Visibility.Hidden; 
                x2.Visibility = Visibility.Hidden;
                x3.Visibility = Visibility.Hidden;
                y1.Visibility = Visibility.Hidden;
                y2.Visibility = Visibility.Hidden;
                y3.Visibility = Visibility.Hidden;
                tbDano.Visibility = Visibility.Visible;
                tbtask.Text = "Задача №2";
            }
        }

        private void ochClick(object sender, RoutedEventArgs e)//Кнопка очистки для контекстного меню
        {
            tbRes.Clear();
        }
    }
}
