using JarmuKolcsonzo.WPF.ViewModels;
using System.ComponentModel;
using System.Windows.Controls;

namespace JarmuKolcsonzo.WPF.Views
{
    /// <summary>
    /// Interaction logic for JarmuvekView.xaml
    /// </summary>
    public partial class JarmuvekView : UserControl
    {
        public JarmuvekView()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;
            JarmuvekViewModel? viewModel = DataContext as JarmuvekViewModel;
            if (viewModel is not null)
            {
                viewModel.SortBy = column.SortMemberPath;
                column.SortDirection = viewModel.ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            }
            // e.Handled = true;
        }
    }
}
