using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Type : INotifyPropertyChanged
    {
        private string name;
        private string img_src;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }

        }
        public string Img_src
        {
            get => img_src; set
            {
                img_src = value;
                RaisePropertyChanged("Img_src");
            }
        }
        public Type(string namee, string src)
        {
            Name = namee;
            Img_src = src;
        }
        public Type()
        {
            name = string.Empty;
            img_src = string.Empty;
        }
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }




    }
}
