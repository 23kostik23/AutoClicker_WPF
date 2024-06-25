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

namespace AutoClicker_WPF
{
    /// <summary>
    /// Логика взаимодействия для SelectPointPositionWindow.xaml
    /// </summary>
    public partial class SelectPointPositionWindow : Window
    {
        public int ClickedX { get; private set; }
        public int ClickedY { get; private set; }
        private MainWindow _mainWindow;

        public SelectPointPositionWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                _mainWindow.StopButton_Click(null, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Устанавливаем размер окна на весь экран
            this.Left = SystemParameters.VirtualScreenLeft;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Width = SystemParameters.VirtualScreenWidth;
            this.Height = SystemParameters.VirtualScreenHeight;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point relativePoint = e.GetPosition(this);
            Point point = PointToScreen(relativePoint);
            ClickedX = (int)relativePoint.X;
            ClickedY = (int)relativePoint.Y;

            int tempX = (int)point.X;
            int tempY = (int)point.Y;
            _mainWindow.ClickedY = tempX;
            _mainWindow.ClickedX = tempY;
            _mainWindow.UpdateClickedXY(tempX, tempY);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"C:\c#\tic_tac_toe_WPF\ноль.png"));
            img.Width = 20;
            img.Height = 20;
            Canvas.SetLeft(img, ClickedX - 10);
            Canvas.SetTop(img, ClickedY - 10);

            Canvas.Children.Add(img);

            this.Background.Opacity = 0;
            //this.Close();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
    }
}
