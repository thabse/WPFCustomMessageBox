using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using WPFCustomMessageBox;

namespace CustomMessageBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void button_StandardMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\n");
        }

        private void button_StandardMessageNew_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show("Hello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\nHello World!\nHello World\n");
        }

        private void button_MessageWithCaption_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World!", "Hello World the title.");
        }

        private void button_MessageWithCaptionNew_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show("Hello world!", "Hello World the title.");
        }

        private void button_MessageWithCaptionAndButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World!", "Hello World the title.", MessageBoxButton.OKCancel);
        }

        private void button_MessageWithCaptionAndButtonNew_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show("Hello World!", "Hello World the title.", MessageBoxButton.OKCancel);
        }

        private void button_MessageWithCaptionButtonImage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Are you sure you want to eject the nuclear fuel rods?",
                "Confirm Fuel Ejection",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Exclamation);
        }

        private void button_MessageWithCaptionButtonImageNew_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show("This is a message.", "This is a caption", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.ShowYesNoCancel(
                "You have unsaved changes.",
                "Unsaved Changes!",
                "Evan Wondrasek",
                "Don't Save",
                "Cancel",
                MessageBoxImage.Exclamation);

            Console.WriteLine(result);
        }

        private void button_StandardMessageWithOwner_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this,
                "Are you sure you want to eject the nuclear fuel rods?",
                "Confirm Fuel Ejection",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Exclamation);
        }

        private void button_CustomMessageWithOwner_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show(this, "This is a message.", "This is a caption", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

        }
        private void button_StandardAutoCloseTest_Click(object sender, RoutedEventArgs e)
        {
            var hiddenOwner = CreateAutoCloseWindow(5000);
            MessageBox.Show(hiddenOwner, "This message will close after 5 seconds.", "I will be gone soon", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void button_AutoCloseTest_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.ShowOK(this, "This message will close after 5 seconds.", "I will be gone soon", "OK", MessageBoxImage.Information, 5000);
        }

        /// <summary>
        /// Creates a window which closes automatically after <paramref name="timeout"/> milliseconds.
        /// See https://stackoverflow.com/a/20098381
        /// </summary>
        /// <param name="timeout">Close this window after x milliseconds</param>
        /// <returns>The already opened window</returns>
        private static Window CreateAutoCloseWindow(int timeout)
        {
            Window window = new Window()
            {
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Background = System.Windows.Media.Brushes.Transparent,
                AllowsTransparency = true,
                ShowInTaskbar = false,
                ShowActivated = true,
                Topmost = true
            };

            window.Show();

            IntPtr handle = new WindowInteropHelper(window).Handle;

            Task.Delay(timeout).ContinueWith(
                t => NativeMethods.SendMessage(handle, 0x10 /*WM_CLOSE*/, IntPtr.Zero, IntPtr.Zero), TaskScheduler.FromCurrentSynchronizationContext());

            return window;
        }

        private static class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

    }
}
