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
    public partial class PlaceAdd : Form
    {
        public Int32? PlaceId { get; }
        public PlaceAdd(Int32? placeId)
        {
            PlaceId = placeId;  

            InitializeComponent();
        }

        private void PlaceAdd_Load(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            if (PlaceId == null)
            {
                this.Text = "Добавление места";
            }
            else
            {
                this.Text = "Изменение места";

                using (silverCinema)
                {
                    var place = silverCinema.Места.Where(x => x.Id == PlaceId).FirstOrDefault();

                    numericUpDown1.Value = place.Ряд;
                    numericUpDown2.Value = place.Место;
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlaceId == null)
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var place = silverCinema.Места.Where(x => x.Ряд == numericUpDown1.Value && x.Место == numericUpDown2.Value).FirstOrDefault();
                        if(place != null)
                        {
                            MessageBox.Show("Такое место уже существует.");
                            return;
                        }

                        silverCinema.Места.Add(new Места
                        {
                           Ряд = (Int32)numericUpDown1.Value,
                           Место = (Int32)numericUpDown2.Value
                        });
                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Место создано.");
                }
                else
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var place = silverCinema.Места.Where(x => x.Id == PlaceId).FirstOrDefault();
                        if (place == null)
                        {
                            MessageBox.Show("Место не найдено.");
                            return;
                        }

                        place.Ряд = (Int32)numericUpDown1.Value;
                        place.Место = (Int32)numericUpDown2.Value;

                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Место изменено.");
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
