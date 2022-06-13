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
    public partial class CityAdd : Form
    {
        public Int32? CityId { get; }
        public CityAdd(Int32? cityId)
        {
            CityId = cityId;

            InitializeComponent();
        } 
        
        private void CityAdd_Load(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            if (CityId == null)
            {
                this.Text = "Добавление города";
            }
            else
            {
                this.Text = "Изменение города";

                using (silverCinema)
                {
                    var city = silverCinema.Город.Where(x => x.Id == CityId).FirstOrDefault();

                    textBox1.Text = city.Название;
                }
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == null)
            {
                MessageBox.Show("Неверный формат");
                return;
            }
            try
            {
                if (CityId == null)
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        silverCinema.Город.Add(new Город
                        {
                            Название = textBox1.Text
                        });
                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Город добавлен.");
                }
                else
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var city = silverCinema.Город.Where(x => x.Id == CityId).FirstOrDefault();
                        if (city == null)
                        {
                            MessageBox.Show("Город не найден");
                            return;
                        }

                        city.Название = textBox1.Text;

                        silverCinema.SaveChanges();
                    }

                    MessageBox.Show("Город изменен.");
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
