using System;
using System.Collections.Generic;
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
using System.Windows.Controls;
using static Kursovoi.DataBase;
using System.Collections.ObjectModel;

namespace Kursovoi
{
    
    public partial class ManageUsers : Window
    {
        private AppDbContext _dbContext = new AppDbContext();
        private List<User> User { get; set; } = new List<User>();


        public ManageUsers()
        {
            InitializeComponent();
            DataContext = this;
            LoadUsers();
        }

        private void LoadUsers()
        {

            User.Clear();
            User = _dbContext.User.ToList();
            UsersDataGrid.ItemsSource = User;

        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var row = UsersDataGrid.SelectedItem as User;
            _dbContext.User.Add(row);
            _dbContext.SaveChanges();
            MessageBox.Show("Changes saved successfully!");
            LoadUsers();
        }
        private void DeleteUsers_click(object sender, RoutedEventArgs e)
        {
            var row = UsersDataGrid.SelectedItem as User;
            _dbContext.User.Remove(
                _dbContext.User.FirstOrDefault(x => x.Id == row.Id));
            _dbContext.SaveChanges();
            LoadUsers();
            MessageBox.Show("mya");
        }

    }
}