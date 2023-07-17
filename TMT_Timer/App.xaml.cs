using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace TMT_Timer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon _notifyIcon = new NotifyIcon();

        public App()
        {
            _notifyIcon.Icon = new Icon(@".\Images\icon.ico");
            _notifyIcon.Visible = true;
            _notifyIcon.ShowBalloonTip(5000, "TMT Timer", "Pomodoro Timer.", ToolTipIcon.Info);
            _notifyIcon.Click += _notifyIcon_Click;
        }

        private void _notifyIcon_Click(object? sender, EventArgs e)
        {
            if (MainWindow == null) return;
            MainWindow.Visibility = Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }
    }
}