using App;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int n, m;
        private int[] encryptedValues;

        public MainWindow()
        {
            InitializeComponent();
            KeyDimension.ItemsSource = Enumerable.Range(1, 150);
        }

        private void GenerationButton_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            KeyDimension.SelectedItem = rnd.Next(1, 150);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            var encoder = new KnapsackEncoder((int)KeyDimension.SelectedItem, InitialText.Text);
            var encrypted = encoder.Encrypt();
            encryptedValues = encrypted.Item1;
            n = encrypted.Item2;
            m = encrypted.Item3; 
            ConvertedText.Text = string.Join(" ", encryptedValues);
        }
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            var encoder = new KnapsackEncoder((int)KeyDimension.SelectedItem, InitialText.Text);
            InitialText.Text = ConvertedText.Text;
            ConvertedText.Text = encoder.Decrypt(encryptedValues, n, m);
        }

        private void LoadTextFromFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != true) return;
            try
            {
                var filePath = openFileDialog.FileName;

                var encoding = DetectFileEncoding(filePath);

                var fileContent = File.ReadAllText(filePath, encoding);

                InitialText.Text = fileContent;
                ConvertedText.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке файла: {ex.Message}");
            }
        }

        private Encoding DetectFileEncoding(string filePath)
        {
            // Определите кодировку файла на основе его содержимого
            // Можно использовать различные методы, например, на основе магических байтов файла

            var buffer = new byte[4];
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileStream.Read(buffer, 0, 4);
            }

            return buffer[0] switch
            {
                0xef when buffer[1] == 0xbb && buffer[2] == 0xbf => Encoding.UTF8,
                0xff when buffer[1] == 0xfe => Encoding.Unicode,
                0xfe when buffer[1] == 0xff => Encoding.BigEndianUnicode,
                _ => Encoding.Default
            };
        }

        private void SaveTextInFile(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем текст из TextBox
                string textToSave = ConvertedText.Text;

                // Открываем диалоговое окно для выбора места сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Получаем путь к выбранному файлу
                    string filePath = saveFileDialog.FileName;

                    // Сохраняем текст в файл
                    File.WriteAllText(filePath, textToSave);

                    MessageBox.Show("Текст успешно сохранен в файл.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}");
            }
        }

        private void KeyDimensionChanged(object sender, RoutedEventArgs e)
        {
            ValidateEncyptDecryptButton();
        }

        private void InitialTextChanged(object sender, RoutedEventArgs e)
        {
            ValidateEncyptDecryptButton();
        }

        private void ValidateEncyptDecryptButton()
        {
            var state = KeyDimension.SelectedItem is not null && !string.IsNullOrEmpty(InitialText.Text);
            EncryptButton.IsEnabled = state;
            DecryptButton.IsEnabled = state;
        }
    }
}