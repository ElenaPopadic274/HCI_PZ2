using NetworkService.Model;
using NetworkService.Pomoc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkService.ViewModel
{
    class SaobracajViewModel : BindableBase
    {
        public static List<string> SaobracajTypes { get; set; } = new List<string> { "Deonica IA", "Deonica IB" };
        private ObservableCollection<Saobracaj> FilterSaobracaji { get; set; } = new ObservableCollection<Saobracaj>();
        public static ObservableCollection<Saobracaj> Saobracaji { get; set; } = new ObservableCollection<Saobracaj>();
        public static ObservableCollection<Saobracaj> SaobracajiCopy { get; set; } = new ObservableCollection<Saobracaj>();


        //za filter
        private string selectedTypeSaobrac = string.Empty;
        private int lowOrHigh = -1;
        private int inOrOutValues = -1;
        private int idForFilter = -1;
        private string idForFilterText = "";
        private bool filtercan = false;
        //putanja do slike //"name.jpg"
        private string pathFirst = "pack://application:,,,/pictures/";

        private string path = "";


        private Saobracaj selectedSaobracaj;//selektovan u listi 

        private string idText; //za vrednosti polja
        private string nameText;
        // private string valueText;
        private string selectedTypeSaobrac2 = string.Empty;

        public MyICommand DeleteCommand { get; set; }//kontrola komande
        public MyICommand AddCommand { get; set; }
        public MyICommand FilterCommand { get; set; }
        public MyICommand CancelFilterCommand { get; set; }
        public MyICommand LowValueCommand { get; set; }
        public MyICommand OutOfRangeCommand { get; set; }
        public MyICommand ExpectedValuesCommand { get; set; }

        public MyICommand HighValueCommand { get; set; }

        public SaobracajViewModel()
        {
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
            FilterCommand = new MyICommand(OnFilter, CanFilter);
            LowValueCommand = new MyICommand(OnLow);
            HighValueCommand = new MyICommand(OnHigh);
            CancelFilterCommand = new MyICommand(OnCancel, CanCancel);
            OutOfRangeCommand = new MyICommand(OnOut);
            ExpectedValuesCommand = new MyICommand(OnExpected);
        }

        public Saobracaj SelectedSaobracaj
        {
            get
            {
                return selectedSaobracaj;
            }
            set
            {
                selectedSaobracaj = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }

        }


        public string IdText
        {
            get
            {
                return idText;
            }
            set
            {
                if (idText != value)
                {
                    idText = value;
                    OnPropertyChanged("IdText");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string NameText
        {
            get
            {
                return nameText;
            }
            set
            {
                if (nameText != value)
                {
                    nameText = value;
                    OnPropertyChanged("NameText");
                    AddCommand.RaiseCanExecuteChanged();

                }
            }
        }



        public string SelectedTypeSaobrac2
        {
            get => selectedTypeSaobrac2;
            set
            {
                if (selectedTypeSaobrac2 != value)
                {
                    selectedTypeSaobrac2 = value;
                    Path = pathFirst + value.ToString() + ".jpg";
                    OnPropertyChanged("Path");
                    OnPropertyChanged("SelectedTypeSaobracaj2");
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string SelectedTypeSaobrac
        {
            get => selectedTypeSaobrac;
            set
            {
                if (selectedTypeSaobrac != value)
                {
                    selectedTypeSaobrac = value;
                    OnPropertyChanged("SelectedTypeSaobracaj");
                    FilterCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int LowOrHigh { get => lowOrHigh; set => lowOrHigh = value; }
        public int IdForFilter { get => idForFilter; set => idForFilter = value; }
        public string IdForFilterText { get => idForFilterText; set => idForFilterText = value; }
        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public int InOrOutValues { get => inOrOutValues; set => inOrOutValues = value; }

        private bool CanDelete()
        {
            return SelectedSaobracaj != null;
        }
        private void OnDelete()
        {
            Saobracaji.Remove(SelectedSaobracaj);
        }
        private bool CanAdd()
        {
            if (SelectedTypeSaobrac2 != null && IdText != null && NameText != null)
                return true;
            return false;
        }
        private void OnAdd()
        {//validacija
            int tempID = 0;
            try
            {
                tempID = Int32.Parse(IdText);
            }
            catch
            {
                System.Windows.MessageBox.Show("ID must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string tempS = NameText;
            if (string.IsNullOrWhiteSpace(tempS))
            {
                System.Windows.MessageBox.Show("The name must not be a white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool exists = false;
            foreach (Saobracaj s in Saobracaji)
            {
                if (s.Id == tempID)
                {
                    exists = true;
                }
            }
            if (exists)
            {
                System.Windows.MessageBox.Show("ID must be unique!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Model.Type temp = new Model.Type(selectedTypeSaobrac2, pathFirst + SelectedTypeSaobrac2.ToString() + ".png");
            Saobracaji.Add(new Saobracaj { Id = tempID, Name = NameText, Value = 8000, Type = temp });
        }
        //funkcije za postavljanje rezima filtera
        private void OnLow()
        {
            LowOrHigh = 1;
            FilterCommand.RaiseCanExecuteChanged();
        }
        private void OnHigh()
        {
            LowOrHigh = 2;
            FilterCommand.RaiseCanExecuteChanged();
        }
        private bool CanFilter()
        {
            bool filter = false;
            if (((LowOrHigh != -1) || (SelectedTypeSaobrac != string.Empty) || (InOrOutValues != -1)))
                filter = true;
            else
                filter = false;

            return filter;
        }
        private void OnFilter()
        {
            int val;
            FilterSaobracaji.Clear();
            if (IdForFilterText == "")
                val = -1;
            else
            {
                try
                {
                    val = Int32.Parse(IdForFilterText);
                }
                catch
                {
                    System.Windows.MessageBox.Show("ID must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }


            SaobracajiCopy.Clear();
            foreach (Saobracaj s in Saobracaji)
            {
                SaobracajiCopy.Add(s);
            }
            if (LowOrHigh != -1)
            {
                foreach (Saobracaj s in Saobracaji)
                {
                    //filtrirati da li je vece manje od zadatog id-a
                    if (LowOrHigh == 1)
                    {
                        if (val >= s.Id)
                        {
                            FilterSaobracaji.Add(s);
                        }
                    }
                    else if (LowOrHigh == 2)
                    {
                        if (val <= s.Id)
                        {
                            FilterSaobracaji.Add(s);
                        }
                    }
                }
                FilterInSaobracaji();
            }

            if (InOrOutValues != -1)
            {
                foreach (Saobracaj s in Saobracaji)
                {
                    //filtrirati po opsegu
                    if (InOrOutValues == 2)
                    {
                        if (s.Value >= 7000 && s.Value <= 15000)
                        {
                            FilterSaobracaji.Add(s);
                        }
                    }
                    else if (InOrOutValues == 1)
                    {
                        if (s.Value < 7000 && s.Value > 15000)
                        {
                            FilterSaobracaji.Add(s);
                        }
                    }
                }
                FilterInSaobracaji();
            }
            if (SelectedTypeSaobrac!= string.Empty)
            {
                foreach (Saobracaj s in Saobracaji)
                {
                    //filtrirati po opsegu
                    if (selectedTypeSaobrac.Equals(s.Type.Name))
                    {
                        FilterSaobracaji.Add(s);
                    }
                }
                FilterInSaobracaji();
            }
            IdForFilterText = "";
            LowOrHigh = -1;
            idForFilter = -1;
            InOrOutValues = -1;
            SelectedTypeSaobrac = string.Empty;
            OnPropertyChanged("SelectedTypeSaobrac");
            filtercan = true;
            OnPropertyChanged("Saobracaji");
            CancelFilterCommand.RaiseCanExecuteChanged();
        }
        private void FilterInSaobracaji()
        {
            Saobracaji.Clear();
            foreach (Saobracaj s in FilterSaobracaji)
            {
                Saobracaji.Add(s);
            }
            FilterSaobracaji.Clear();
        }
        private bool CanCancel()
        {
            return filtercan;

        }
        private void OnCancel()
        {
            Saobracaji.Clear();
            foreach (Saobracaj s in SaobracajiCopy)
            {
                Saobracaji.Add(s);
            }
            OnPropertyChanged("Saobracaji");
            filtercan = false;
            CancelFilterCommand.RaiseCanExecuteChanged();

        }
        private void OnOut()
        {
            InOrOutValues = 1;
            FilterCommand.RaiseCanExecuteChanged();
        }
        private void OnExpected()
        {
            InOrOutValues = 2;
            FilterCommand.RaiseCanExecuteChanged();
        }

       
    }
}
