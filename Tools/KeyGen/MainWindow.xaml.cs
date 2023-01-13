using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KeyGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int keyLength;
            if (size.Text == "")
            {
                MessageBox.Show("Veuillez renseigner une taille de clé");
            }
            else if (!Int32.TryParse(size.Text, out keyLength))
            {
                MessageBox.Show("Veuillez renseigner un nombre");
            }
            else
            {
                Int32.TryParse(size.Text, out keyLength);
                if(keyLength > 50)
                {
                    MessageBox.Show("La taille maximale acceptée est de 50 " +
                                    "\nvotre taille renseignée est supérieure" +
                                    "\nelle a été ramenée à 50");
                }
                keyLength = keyLength <= 50 ? keyLength : 50;
                string displayString = "";
                Random rnd = new Random();
                string allCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
                char[] charactersArray = allCharacters.ToCharArray();
                for (var i = 0; i < keyLength; i++)
                {
                    displayString += charactersArray[rnd.Next(0, charactersArray.Length)];
                }
                secretKey.Text = displayString;
            }
            
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
