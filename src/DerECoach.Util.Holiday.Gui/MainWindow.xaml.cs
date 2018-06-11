using DerECoach.Util.Holiday.Gui.ViewModels.HolidayGrid;
using DerECoach.Util.Holiday.Gui.ViewModels.LocationTree;
using DerECoach.Util.Holiday.Gui.ViewModels.MainWindow;
using DerECoach.Util.Holiday.Gui.Views;

namespace DerECoach.Util.Holiday.Gui
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
