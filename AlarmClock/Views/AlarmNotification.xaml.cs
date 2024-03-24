using System.Windows;

namespace AlarmClock.Views;

public partial class AlarmNotification : Window
{
    private SettingsContext _context;
    
    public AlarmNotification(SettingsContext context)
    {
        _context = context;
        
        InitializeComponent();
    }
}