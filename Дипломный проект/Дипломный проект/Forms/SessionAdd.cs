using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Дипломный_проект.Models;

namespace Дипломный_проект.Forms
{
    public partial class SessionAdd : Form
    {
        public Int32? SessionId { get; }
        public SessionAdd(Int32? sessionId)
        {
            SessionId = sessionId;

            InitializeComponent();
        }

        private void SessionAdd_Load(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            comboBox1.DataSource = silverCinema.Фильм.ToList();
            comboBox1.DisplayMember = "Название";
            comboBox1.ValueMember = "Id";

            comboBox2.DataSource = silverCinema.Город.ToList();
            comboBox2.DisplayMember = "Название";
            comboBox2.ValueMember = "Id";

            List<TimeAddModel> dateTime = new List<TimeAddModel>() ;

            foreach (var item in silverCinema.Время)
            {
                String dateTimeString = item.Время1.ToString("c") + "  " + item.Дата.ToString("dd.mm.yyyy");
                dateTime.Add(new TimeAddModel { Id = item.Id, DateTime = dateTimeString });
            };

            comboBox3.DataSource = dateTime;
            comboBox3.DisplayMember = "DateTime";
            comboBox3.ValueMember = "Id";

            if (SessionId == null)
            {
                this.Text = "Добавление сеанса";
            }
            else
            {
                this.Text = "Изменение сеанса";

                using (silverCinema)
                {
                    var session = silverCinema.Сеанс.Where(x => x.Id == SessionId).FirstOrDefault();

                    comboBox1.SelectedValue = session.Фильм.Id;
                    comboBox2.SelectedValue = session.Город.Id;
                    comboBox3.SelectedValue = session.Время.Id;
                }
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionId == null)
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        silverCinema.Сеанс.Add(new Сеанс
                        {
                            Id_Фильм = (Int32)comboBox1.SelectedValue,
                            Id_Город = (Int32)comboBox2.SelectedValue,
                            Id_Время = (Int32)comboBox3.SelectedValue
                        });
                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Сеанс создан.");
                }
                else
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var session = silverCinema.Сеанс.Where(x => x.Id == SessionId).FirstOrDefault();
                        if (session == null)
                        {
                            MessageBox.Show("Сеанс не найден");
                            return;
                        }

                        session.Id_Фильм = (Int32)comboBox1.SelectedValue;
                        session.Id_Город = (Int32)comboBox2.SelectedValue;
                        session.Id_Время = (Int32)comboBox3.SelectedValue;

                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Сеанс изменен.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
