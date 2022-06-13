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
    public partial class TimeAdd : Form
    {
        public Int32? TimeId { get; }

        public TimeAdd(Int32? timeId)
        {
            TimeId = timeId;

            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Now)
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            try
            {
                if (TimeId == null)
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        silverCinema.Время.Add(new Время
                        {
                            Дата = dateTimePicker1.Value,
                            Время1= dateTimePicker2.Value.TimeOfDay
                        });
                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Время создано.");
                }
                else
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var time = silverCinema.Время.Where(x => x.Id == TimeId).FirstOrDefault();
                        if (time == null)
                        {
                            MessageBox.Show("Фильм не найден");
                            return;
                        }

                        time.Дата = dateTimePicker1.Value;
                        time.Время1 = dateTimePicker2.Value.TimeOfDay;

                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Время изменено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат");
                return;
            }

            Close();
        }

        private void TimeAdd_Load(object sender, EventArgs e)
        {
            if (TimeId == null)
            {
                this.Text = "Добавление времени";
            }
            else
            {
                this.Text = "Изменение времени";

                using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                {
                    var time = silverCinema.Время.Where(x => x.Id == TimeId).FirstOrDefault();

                    dateTimePicker1.Value = time.Дата;
                    dateTimePicker2.Text = time.Время1.ToString();
                }
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
