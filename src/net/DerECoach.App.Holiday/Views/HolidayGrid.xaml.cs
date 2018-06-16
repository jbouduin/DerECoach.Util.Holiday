using DerECoach.App.Holiday.ViewModels.HolidayGrid;

namespace DerECoach.App.Holiday.Views
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
