using NetworkService.Pomoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class GraphUpdater : BindableBase
    {
        private double firstBindingPoint;
        public double FirstBindingPoint
        {
            get { return firstBindingPoint; }
            set
            {
                SecondBindingPoint = firstBindingPoint;
                firstBindingPoint = value;
                OnPropertyChanged("FirstBindingPoint");
            }
        }

        private double secondBindingPoint;
        public double SecondBindingPoint
        {
            get { return secondBindingPoint; }
            set
            {
                ThirdBindingPoint = secondBindingPoint;
                secondBindingPoint = value;
                OnPropertyChanged("SecondBindingPoint");
            }
        }

        private double thirdBindingPoint;
        public double ThirdBindingPoint
        {
            get { return thirdBindingPoint; }
            set
            {
                FourthBindingPoint = thirdBindingPoint;
                thirdBindingPoint = value;
                OnPropertyChanged("ThirdBindingPoint");
            }
        }

        private double fourthBindingPoint;
        public double FourthBindingPoint
        {
            get { return fourthBindingPoint; }
            set
            {
                FifthBindingPoint = fourthBindingPoint;
                fourthBindingPoint = value;
                OnPropertyChanged("FourthBindingPoint");
            }
        }

        private double fifthBindingPoint;
        public double FifthBindingPoint
        {
            get { return fifthBindingPoint; }
            set
            {
                fifthBindingPoint = value;
                OnPropertyChanged("FifthBindingPoint");
            }
        }

    }
}
