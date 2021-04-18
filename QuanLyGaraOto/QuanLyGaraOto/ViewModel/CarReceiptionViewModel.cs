using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace QuanLyGaraOto.ViewModel
{
    public class CarReceiptionViewModel :BaseViewModel
    {
        public ICommand CloseCommand { get; set; }
        public CarReceiptionViewModel()
        {
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();
            });

        }
    }
}
