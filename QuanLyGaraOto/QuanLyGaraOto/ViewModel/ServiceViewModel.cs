using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {
        public ICommand CarReceptionCommand { get; set; }

        public ServiceViewModel()
        {
            CarReceptionCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p)=> {
                CarReceptionWindow carReceptionWindow = new CarReceptionWindow();
                carReceptionWindow.ShowDialog();
            });
        }
    }
}
