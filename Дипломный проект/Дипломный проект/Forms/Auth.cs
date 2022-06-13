using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Дипломный_проект.Forms
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String login = this.login.Text;
            String password = this.password.Text;

            if (String.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Введите логин.");
                return;
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль.");
                return;
            }

            if (login == "admin" && password == "123")
            {
                AdminPanel adminPanel = new AdminPanel();

                adminPanel.Show();
                Close();
                Poster poster = new Poster();
                poster.Close();

            }
            else
            {
                MessageBox.Show("Данные введены не верно.");
                return;
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {
            Poster poster = new Poster();

            poster.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();    
        }
    }
}
