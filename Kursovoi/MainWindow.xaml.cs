using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using Microsoft.EntityFrameworkCore;
using static Kursovoi.DataBase;
using MessageX = System.Windows.MessageBox;


namespace Kursovoi
{

    public partial class MainWindow : Window
    {
        private AppDbContext _dbContext = new AppDbContext();
        public ObservableCollection<string> FileNames { get; set; } = new ObservableCollection<string>();
        public void Update()
        {
            comboBoxFiles.Items.Clear();
            foreach (var file in _dbContext.FileData)
            {
                comboBoxFiles.Items.Add(file.FilName);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            switch (Context.RoleId)
            {
                case 0:
                    VisButton.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    VisButton.Visibility= Visibility.Visible;
                    break;
                default:
                    MessageX.Show("Неопределена роль пользователя или даныный профиль заблокирован.");
                    this.Close();
                    break;               
            }
            Update();
        }
        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsers manageUsersWindow = new ManageUsers();
            manageUsersWindow.ShowDialog();           
            Update();
        }
        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFileName = comboBoxFiles.SelectedItem as string;

            if (selectedFileName != null)
            {
                // Получаем данные файла из БД
                var fileData = _dbContext.FileData.FirstOrDefault(f => f.FilName == selectedFileName);

                if (fileData != null)
                {
                    // Диалог сохранения файла
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = fileData.FilName;
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            System.IO.File.WriteAllBytes(saveFileDialog.FileName + "."+fileData.Ext, fileData.Fil);
                            MessageX.Show("Файл успешно скачан!");
                        }
                        catch (Exception ex)
                        {
                            MessageX.Show($"Ошибка при сохранении файла: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageX.Show("Файл не найден в базе данных.");
                }
            }
            else
            {
                MessageX.Show("Выберите файл для скачивания.");
            }
        }
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {          
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);
                byte[] fileData = System.IO.File.ReadAllBytes(filePath);

                var splitted = fileName.Split(".");
                string fileExtention = splitted[splitted.Length - 1];

                
                var newFile = new FileData 
                {
                    FilName = fileName,
                    Fil = fileData,
                    Ext = fileExtention
                };              
                _dbContext.FileData.Add(newFile);
                _dbContext.SaveChanges();               
                FileNames.Add(fileName);

                MessageX.Show("Файл успешно загружен в базу данных!");
            }
            Update();
        }
      
    }
}