using System.Windows;

namespace CefSharp.WpfApp;

public partial class MainWindow : Window
{
    private readonly MyProxy _proxy;

    public MainWindow()
    {
        _proxy = new MyProxy();

        InitializeComponent();
    }
}
