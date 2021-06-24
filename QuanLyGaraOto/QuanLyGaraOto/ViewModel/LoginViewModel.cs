using QuanLyGaraOto.Convert;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{

    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private USER _User;
        public USER User { get => _User; set { _User = value; OnPropertyChanged(); } }
        public ICommand PasswordChangedCommand { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public LoginViewModel()
        {
            IsClose = true;

            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                
                Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                    p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }
            HashConvert hash = new HashConvert();
            string passEncode = hash.GetHash(Password);
            var accCount = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName && x.Password == passEncode).Count();

            if (accCount > 0)
            {
                User = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName && x.Password == passEncode).SingleOrDefault();
                IsLogin = true;
                IsClose = false;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản và mật khẩu!");
            }

        }
        
    }
}