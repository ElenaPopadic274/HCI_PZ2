using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Saobracaj : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private double value;
        private Type type;

        public Saobracaj()
        {
        }
        public Saobracaj(int id)
        {
            this.id = id;
        }
        public Saobracaj(Saobracaj s) {
            id = s.id;
            name = s.name;
            value = s.value;
            type = s.type;
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    RaisePropertyChanged("Value");
                    ViewModel.GraphViewModel.ElementHeights.FirstBindingPoint = ViewModel.GraphViewModel.CalculateElementHeight(value);
                }
            }
        }

        public Type Type
        {
            get => type;
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
