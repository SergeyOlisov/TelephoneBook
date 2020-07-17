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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace TelphoneBook
{
    public partial class MainWindow : Window
    {
        private static MySqlConnection db;
        const string host = "mysql11.hostland.ru";
        const string database = "host1323541_itstep5";
        const string port = "3306";
        const string username = "host1323541_itstep";
        const string pass = "269f43dc";
        const string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
        private List<string> contacts = new List<string>();
        private string contact;
        private int scroll = 0;
        public MainWindow()
        {
            InitializeComponent();
            db = new MySqlConnection(ConnString);
            db.Open();

            var sql = "SELECT First_name, Last_name, Tel FROM TelephoneBook";
            var query = new MySqlCommand { Connection = db, CommandText = sql };
            var result = query.ExecuteReader();
         
            while (result.Read())
            {
                contact = result.GetString("First_name");
                contacts.Add(contact);
                contact = result.GetString("Last_name");
                contacts.Add(contact);
                contact = result.GetString("Tel");
                contacts.Add(contact);
            }
            Firstname.Text = contacts[scroll];
            Lastname.Text = contacts[scroll + 1];
            Tel.Text = contacts[scroll + 2];
            db.Close();
        }



        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
            db.Open();

            db.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.Open();
            if (scroll - 3 <= 0)
            {
                scroll = 0;
                Firstname.Text = contacts[scroll];
                Lastname.Text = contacts[scroll + 1];
                Tel.Text = contacts[scroll + 2];
            } 
            else
            {
                scroll -= 3;
                Firstname.Text = contacts[scroll];
                Lastname.Text = contacts[scroll + 1];
                Tel.Text = contacts[scroll + 2];
            }
            db.Close();
        }

        private void Button_down_Click(object sender, RoutedEventArgs e)
        {
            db.Open();
            if (scroll + 3 >= contacts.ToArray().Length)
            {
                scroll = contacts.ToArray().Length - 3;
                Firstname.Text = contacts[scroll];
                Lastname.Text = contacts[scroll + 1];
                Tel.Text = contacts[scroll + 2];
            }
            else
            {
                scroll += 3;
                Firstname.Text = contacts[scroll];
                Lastname.Text = contacts[scroll + 1];
                Tel.Text = contacts[scroll + 2];
            }
            db.Close();
        }

        private void Add_contact_button_Click(object sender, RoutedEventArgs e)
        {
            var fistname = Add_firstname_textbox.Text;
            var lastname = Add_lastname_textbox.Text;
            var telphone = Add_tel_textbox.Text;
            if (fistname.Length > 1 && lastname.Length > 1 && telphone.Length > 6)
            {
                db.Open();
                var sql = $"INSERT INTO TelephoneBook (First_name, Last_name, Tel) VALUES ('{fistname}', '{lastname}', '{telphone}')";
                var command = new MySqlCommand { Connection = db, CommandText = sql };
                var result = command.ExecuteNonQuery();
                contacts.Add(fistname);
                contacts.Add(lastname);
                contacts.Add(telphone);
                Add_firstname_textbox.Text = null;
                Add_lastname_textbox.Text = null;
                Add_tel_textbox.Text = null;
                db.Close();
            }
        }
    }
}
