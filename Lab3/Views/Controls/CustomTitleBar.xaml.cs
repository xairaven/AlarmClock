﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Lab3.Views.Controls;

public partial class CustomTitleBar : UserControl
{
    [Description("Window Title"),Category("Title")] 
    public string WindowTitle {
        get => (string) WindowTitleLabel.Content;
        set => WindowTitleLabel.Content = value;
    }
    
    public CustomTitleBar()
    {
        InitializeComponent();
    }
    
    private void Exit_Window(object sender, RoutedEventArgs e)
    {
        var window = GetWindow();
        window.Close();
    }

    private void Resize_Window(object sender, RoutedEventArgs e)
    {
        var window = GetWindow();
        window.WindowState = window.WindowState == WindowState.Maximized 
            ? WindowState.Normal : WindowState.Maximized;
    }

    private void Minimize_Window(object sender, RoutedEventArgs e)
    {
        var window = GetWindow();
        window.WindowState = WindowState.Minimized;
    }

    private Window GetWindow()
    {
        var window = Window.GetWindow(this);

        if (window is null) ErrorShutdown();
        
        return window!;
    }

    private void ErrorShutdown()
    {
        MessageBox.Show("Error. Unable to get current window (Custom title bar)",
            caption: "Error!",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error);
                
        Application.Current.Shutdown();
    }
}