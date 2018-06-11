using DerECoach.Util.Holiday.App.ViewModels.HolidayGrid;

namespace DerECoach.Util.Holiday.App.Views
{
    /// <summary>
    /// Interaction logic for HolidayGrid.xaml
    /// </summary>
    internal partial class HolidayGrid
    {
        public HolidayGrid()
        {
            InitializeComponent();
        }

        public HolidayGrid(IHolidayGridViewModel holidayGridViewModel)
        {
            InitializeComponent();
            DataContext = holidayGridViewModel;
        }
    }
}
