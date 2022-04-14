using NetworkService.Model;
using NetworkService.Pomoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace NetworkService.ViewModel
{
    public class GraphViewModel : BindableBase
    {
        public static GraphUpdater ElementHeights { get; set; } = new GraphUpdater();

        public static double CalculateElementHeight(double value)
        {
            return 10 * 0.4;
        }
    }
}
