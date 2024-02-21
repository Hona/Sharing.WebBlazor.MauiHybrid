using Microsoft.Maui.Controls;

namespace MyShop.UI.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}