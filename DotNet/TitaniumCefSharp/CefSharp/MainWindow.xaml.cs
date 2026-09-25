using System.Windows;

namespace CefSharp.WpfApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Browser.RequestHandler = new ImageRequestHandler(@"C:\Users\User\Documents\app_data\miv\temp");
        Browser.Address = "https://www.google.com";
    }
}
