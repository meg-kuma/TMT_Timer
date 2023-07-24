using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace TMT_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count = 0;
        private readonly DispatcherTimer _timerTask = new();
        private readonly DispatcherTimer _timerRest = new();

        private DateTime _endDt = new DateTime();

        private bool _isEnd = false;

        public MainWindow()
        {
            InitializeComponent();
            _timerTask.Interval = new TimeSpan(10 * 1000);
            _timerTask.Tick += new EventHandler(TimerTask_Tick);
            _timerRest.Interval = new TimeSpan(10 * 1000);
            _timerRest.Tick += new EventHandler(TimerRest_Tick);

            ButtonRest.IsEnabled = false;

            Label1.Content = $"{_count + 1}回目の作業を始めてください";
            Label2.Content = "";
        }

        private void TimerRest_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (_endDt < now)
            {
                var str = $"休憩時間は終わりました。\n{_count + 1}回目の作業を始めてください";
                Label1.Content = str;
                
                ShowForm();

                ButtonStart.IsEnabled = true;
                ButtonRest.IsEnabled = false;
                
                _timerRest.Stop();
            }
        }

        private void TimerTask_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (_endDt < now)
            {
                var str = $"{_count} 回目終了 休憩してください";
                Label1.Content = str;

                ShowForm();

                ButtonRest.IsEnabled = true;
                ButtonStart.IsEnabled = false;

                _timerTask.Stop();
            }
        }

        private void ShowForm()
        {
            Visibility = Visibility.Visible;
            Topmost = true;
            WindowState = WindowState.Normal;
        }
        
        private void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            _count++;

            DateTime now = DateTime.Now;
            _endDt = now.AddMinutes(25);

            var str = $"{_endDt:HH 時 mm 分} まで集中して作業しましょう";
            var str1 = $"{_count} 回目";
            Label1.Content = str;
            Label2.Content = str1;

            ButtonRest.IsEnabled = false;
            ButtonStart.IsEnabled = false;

            _timerTask.Start();
        }

        private void ButtonRest_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if (_count % 4 != 0)
            {
                _endDt = now.AddMinutes(5);
            }
            else
            {
                _count = 0;
                _endDt = now.AddMinutes(30);
            }
            
            var str = $"{_endDt:HH 時 mm 分} まで休憩しましょう";
            Label1.Content = str;

            ButtonRest.IsEnabled = false;
            
            _timerRest.Start();
        }
        
        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            if (_isEnd)
            {
                return;
            }

            e.Cancel = true;
            Visibility = Visibility.Hidden;
            Topmost = false;
        }

        private void ExitMenuItem_OnClick(object sender, EventArgs e)
        {
            _isEnd = true;
            Application.Current.Shutdown();
        }
    }
}