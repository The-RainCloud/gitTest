using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Kursovoi.DataBase;

namespace Kursovoi
{

    public partial class Authorisation : Window
    {
        private AppDbContext _dbContext = new AppDbContext();
        public Authorisation()
        {
            InitializeComponent();

           
        }


        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;


            var user = _dbContext.User.FirstOrDefault(u => u.Login == login && u.Password == password);



            if ( user != null)
            {
                
                MessageBox.Show($"Добро пожаловать, {user.Name}!");
                Context.RoleId = user.RoleId;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                
                ErrorTextBlock.Text = "Неверный логин или пароль";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

    }
}
