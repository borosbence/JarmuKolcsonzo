using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JarmuKolcsonzo.WPF.ViewModels
{
    public abstract class PagerViewModel : ObservableObject
    {
        private string? _searchKey;
        public string? SearchKey
        {
            get { return _searchKey; }
            set
            {
                _searchKey = value;
                // Új keresési kulcsszó esetén, visszaugrik az első oldalra
                page = 1;
            }
        }

        private string? _sortBy;
        public string? SortBy
        {
            get { return _sortBy; }
            set
            {
                // Ha még egyszer ugyanazt az értéket kapjuk
                if (value == _sortBy)
                {
                    ascending = !ascending;
                }
                _sortBy = value;
                LoadData();
            }
        }
        public bool ascending;

        #region Oldaltördelés
        private string? _currentPage;
        public string? CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public List<int> IPPList { get; }

        private int itemsPerPage;
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set { SetProperty(ref itemsPerPage, value); LoadData(); }
        }
        protected int page;
        private int pageCount;

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set
            {
                // Összes oldalak száma
                pageCount = (value - 1) / itemsPerPage + 1;
                // Hanyadik oldalnál járok a hányból
                CurrentPage = page + "/" + pageCount;
                // Megjelenítés a View-on
                SetProperty(ref _totalItems, value);
                // Túlcsordulás esetén az utolsó oldalra lépjen
                if (page > pageCount)
                {
                    LastPage();
                }
            }
        }

        public RelayCommand FirstPageCmd { get; }
        public RelayCommand PreviousPageCmd { get; }
        public RelayCommand NextPageCmd { get; }
        public RelayCommand LastPageCmd { get; }
        #endregion

        // Az öröklött osztályokban valósul meg
        public IRelayCommand LoadDataCmd { get; }

        public PagerViewModel()
        {
            LoadDataCmd = new AsyncRelayCommand(LoadData);
            FirstPageCmd = new RelayCommand(FirstPage);
            PreviousPageCmd = new RelayCommand(PrevPage);
            NextPageCmd = new RelayCommand(NextPage);
            LastPageCmd = new RelayCommand(LastPage);
            IPPList = [10, 20, 50];
            page = 1;
            itemsPerPage = 20;
        }
        protected abstract Task LoadData();

        protected void FirstPage()
        {
            page = 1;
            LoadData();
        }

        protected void PrevPage()
        {
            if (page > 1)
            {
                page--;
                LoadData();
            }
        }

        protected void NextPage()
        {
            if (page < pageCount)
            {
                page++;
                LoadData();
            }
        }

        protected void LastPage()
        {
            page = pageCount;
            LoadData();
        }
    }
}
