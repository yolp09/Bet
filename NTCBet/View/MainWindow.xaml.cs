using NTCBet.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace NTCBet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isWiden = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(new ApplicationMain());
            this.MouseLeftButtonDown += titleBar_MouseLeftButtonDown;
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void window_initiateWiden(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isWiden = true;
            this.MouseLeftButtonDown -= titleBar_MouseLeftButtonDown;
        }

        private void window_endWiden(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _isWiden = false;

            Rectangle rect = (Rectangle)sender;
            rect.ReleaseMouseCapture();
            this.MouseLeftButtonDown += titleBar_MouseLeftButtonDown;
        }

        private void window_HeightenAndWiden(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (_isWiden)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 5;
                double newHeight = e.GetPosition(this).Y + 5;
                if (newHeight > 0 && newWidth > 0)
                {
                    this.Height = newHeight;
                    this.Width = newHeight;
                }
            }
        }
    }
}
