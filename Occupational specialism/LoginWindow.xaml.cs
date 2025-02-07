using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Occupational_specialism
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        public class Cryptography
        {
            public static string Decrypt(string ciphertext)
        {
            string EncryptionKey = "@ram@1234xxxxxxxxtttttuuuuuiiiii0";
            ciphertext = ciphertext.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            using (Aes encryptor = Aes.Create())
                {
                   Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                   {
                       0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65,0x76
                   });
               encryptor.Key = pdb.GetBytes(32);
               encryptor.IV = pdb.GetBytes(16);
               using (MemoryStream ms = new MemoryStream())
               {
                   using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                   {
                       cs.Write(cipherBytes, 0, cipherBytes.Length);
                       cs.Close();
                   }
                   ciphertext = Encoding.Unicode.GetString(ms.ToArray());
                    }
            }
            return ciphertext;
        }

            public static string Encrypt(string encryptString)
            {
                string EncryptionKey = "@ram@1234xxxxxxxxtttttuuuuuiiiii0";
                byte[] clearbytes = Encoding.Unicode.GetBytes(encryptString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    {
                       0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65,0x76
                    });

                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearbytes, 0, clearbytes.Length);
                            cs.Close();
                        }
                        encryptString = encryptString = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return encryptString;
            }
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DopeP\\source\\repos\\Occupational specialism\\Occupational specialism\\Database1.mdf\Integrated Security=True");

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string Password = "";
            bool IsExist = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblUserRegistration where UserName='" + username.Text + "'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Password = sdr.GetString(2);
                IsExist = true;
            }

            con.Close();
            if (IsExist)
            {
                if (Cryptography.Decrypt(Password).Equals(password.Text))
                {
                    MessageBox.Show("Login success,", "success", MessageBoxButton.OK);
                    Window1 win1 = new Window1();
                    win1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password is wrong!...", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid credentials.", "Error", MessageBoxButton.OK);
            }
        }
    }
}
