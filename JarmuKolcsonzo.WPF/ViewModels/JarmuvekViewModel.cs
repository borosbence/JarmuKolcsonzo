using ApiClient.Repositories;
using CommunityToolkit.Mvvm.Input;
using JarmuKolcsonzo.Model.Entities;
using JarmuKolcsonzo.WPF.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.WPF.ViewModels
{
    public class JarmuvekViewModel : PagerViewModel, IEditCommands<Jarmu>
    {
        private IPagerRepository<Jarmu> _jarmuRepo;
        private IGenericRepository<JarmuTipus> _jarmuTipusRepo;

        private ObservableCollection<Jarmu> _jarmuvek = new();
        public ObservableCollection<Jarmu> Jarmuvek
        {
            get { return _jarmuvek; }
            set { SetProperty(ref _jarmuvek, value); }
        }

        private ObservableCollection<JarmuTipus> _jarmuTipusok = new();
        public ObservableCollection<JarmuTipus> JarmuTipusok
        {
            get { return _jarmuTipusok; }
            set { SetProperty(ref _jarmuTipusok, value); }
        }

        private Jarmu _selectedJarmu = new();
        public Jarmu SelectedJarmu
        {
            get { return _selectedJarmu; }
            set
            {
                SetProperty(ref _selectedJarmu, value);
                DeleteCmdAsync.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand NewCmd { get; }
        public IAsyncRelayCommand<Jarmu> SaveCmdAsync { get; }
        public IAsyncRelayCommand<Jarmu> DeleteCmdAsync { get; }

        public JarmuvekViewModel(IPagerRepository<Jarmu> jarmuRepo, IGenericRepository<JarmuTipus> jarmuTipusRepo)
        {
            _jarmuRepo = jarmuRepo;
            _jarmuTipusRepo = jarmuTipusRepo;
            NewCmd = new RelayCommand(New);
            SaveCmdAsync = new AsyncRelayCommand<Jarmu>(jarmu => SaveAsync(jarmu));
            DeleteCmdAsync = new AsyncRelayCommand<Jarmu>(jarmu => DeleteAsync(jarmu), CanDelete);
            Task.Run(LoadData);
        }

        protected override async Task LoadData()
        {
            var jarmuvek = await _jarmuRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = jarmuvek.TotalItems;
            Jarmuvek = new ObservableCollection<Jarmu>(jarmuvek.Data);
            var tipusok = await _jarmuTipusRepo.GetAllAsync();
            JarmuTipusok = new ObservableCollection<JarmuTipus>(tipusok);
        }

        private void New()
        {
            SelectedJarmu = new Jarmu();
        }

        private async Task SaveAsync(Jarmu jarmu)
        {
            bool exists = await _jarmuRepo.ExistsByIdAsync(jarmu.id);
            if (exists)
            {
                await _jarmuRepo.UpdateAsync(jarmu.id, jarmu);
            }
            else
            {
                await _jarmuRepo.InsertAsync(jarmu);
                Jarmuvek.Insert(0, jarmu);
            }
        }

        private bool CanDelete(Jarmu jarmu)
        {
            if (jarmu != null)
            {
                return jarmu.id > 0;
            }
            return false;
        }

        private async Task DeleteAsync(Jarmu jarmu)
        {
            await _jarmuRepo.DeleteAsync(jarmu.id);
            Jarmuvek.Remove(jarmu);
        }
    }
}