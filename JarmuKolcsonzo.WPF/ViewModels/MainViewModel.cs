using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace JarmuKolcsonzo.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private ObservableObject _selectedViewModel;
        public ObservableObject SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }
        public IRelayCommand<object> UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new RelayCommand<object>(e => Execute(e));
            // SelectedViewModel = new JarmuViewModel();
            // Required = nem lehet null értékű
            SelectedViewModel = App.Current.Services.GetRequiredService<JarmuvekViewModel>();
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Jarmuvek")
            {
                // SelectedViewModel = new JarmuViewModel();
                // DI után:
                SelectedViewModel = App.Current.Services.GetRequiredService<JarmuvekViewModel>();
            }
            else if (parameter.ToString() == "Ugyfelek")
            {
                SelectedViewModel = App.Current.Services.GetRequiredService<UgyfelekViewModel>();
            }
        }
    }
}
