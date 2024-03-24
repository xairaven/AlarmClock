using System.Windows;

namespace AlarmClock.Services;

public static class StartupService
{
    public static SettingsContext ConfigureSettings(Window main)
    {
        SettingsContext context = null!;
        
        try
        {
            context = new SettingsContext();
        }
        catch (Exception)
        {
            MessageBox.Show(messageBoxText: "Error! Settings upload was failed.",
                caption:"Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
            
            Application.Current.Shutdown();
        }

        return context;
    }
}