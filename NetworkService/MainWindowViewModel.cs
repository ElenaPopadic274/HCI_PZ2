using NetworkService.Pomoc;
using NetworkService.ViewModel;
using NetworkService.VML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private SaobracajViewModel saobracajModelView = new SaobracajViewModel();
        private DropDownViewModel networkViewModel = new DropDownViewModel();
        private GraphViewModel graphViewModel = new GraphViewModel();
        private BindableBase currentViewModel;
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<String>(OnNav);
            CurrentViewModel = saobracajModelView;
        }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "Network Data":
                    CurrentViewModel = saobracajModelView;
                    break;
                case "Network View":
                    CurrentViewModel = networkViewModel;
                    break;
                case "Data Chart":
                    CurrentViewModel = graphViewModel;
                    break;

            }
        }
    }
}
