using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace AutoClicker.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    
    public void ButtonClicked(object source, RoutedEventArgs args)
    {
        if (double.TryParse(celsius.Text, out double C))
        {
            var F = C * (9d / 5d) + 32;
            fahrenheit.Text = F.ToString("0.0");
        }
        else
        {
            celsius.Text = "0";
            fahrenheit.Text = "0";
        }
    }
}