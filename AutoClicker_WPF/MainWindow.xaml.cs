using GlobalHotKey;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoClicker_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private SelectPointPositionWindow selectPointPositionWindow;
        private bool isClicking = false;
        private Thread clickThread;
        public int ClickedX {  get; set; }
        public int ClickedY { get; set; }
        public int Interval { get; set; }
        private readonly HotKeyManager _hotKeyManager;


        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            _hotKeyManager = new HotKeyManager();
            _hotKeyManager.Register(Key.S, ModifierKeys.None);
            _hotKeyManager.KeyPressed += HotKeyManager_KeyPressed;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                StopButton_Click(null, null);
            }
        }

        private void HotKeyManager_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == Key.S)
            {
                StopButton_Click(null, null);
            }
        }

        public void SelectPointPosition_Click(object sender, EventArgs e)
        {
            if(selectPointPositionWindow !=  null)
            {
                selectPointPositionWindow.Close();
            }
            selectPointPositionWindow = new SelectPointPositionWindow(this);
            selectPointPositionWindow.Show();
        }

        public void UpdateClickedXY(int x, int y)
        {
            this.ClickedX = x;
            this.ClickedY = y;
            PosXTextBlock.Text = $"pos X: {this.ClickedX}";
            PosYTextBlock.Text = $"pos Y: {this.ClickedY}";
        }

        public void StartButton_Click(object sender, EventArgs e)
        {
            Interval = Convert.ToInt32(IntervalTextBox.Text);
            Interval = Math.Abs(Interval);
            if (!isClicking)
            {
                isClicking = true;
                clickThread = new Thread(() =>
                {
                    while (isClicking)
                    {
                        AutoClickerHelper.ClickMouse(ClickedX, ClickedY, Interval);
                    }
                });
                clickThread.Start();
            }
        }

        public void StopButton_Click(object sender, EventArgs e)
        {
            isClicking = false;
            if (clickThread != null && clickThread.IsAlive)
            {
                clickThread.Join();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _hotKeyManager.Dispose();
            base.OnClosed(e);
        }
    }
}