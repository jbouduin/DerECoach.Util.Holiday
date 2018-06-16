using DerECoach.App.Holiday.ViewModels.HolidayGrid;
using DerECoach.App.Holiday.ViewModels.LocationTree;
using DerECoach.App.Holiday.ViewModels.MainWindow;
using DerECoach.App.Holiday.Views;

namespace DerECoach.App.Holiday
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var holidayGridViewModel = new HolidayGridViewModel();
            var locationTreeViewModel = new LocationTreeViewModel(holidayGridViewModel);
            DataContext = new MainWindowViewModel(locationTreeViewModel);
            TreeViewGrid.Children.Add(new LocationTreeView(locationTreeViewModel));
            ContentGrid.Children.Add(new HolidayGrid(holidayGridViewModel));

        }

        
    }
}
