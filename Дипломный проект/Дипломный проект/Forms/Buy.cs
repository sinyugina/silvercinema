using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Дипломный_проект.Models
{
    public partial class Buy : Form
    {
        public SessionViewModel Session { get; }
        public Buy(SessionViewModel session)
        {
            Session = session;

            InitializeComponent();
        }

        private void Buy_Load(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            var tickets = silverCinema.Билеты.Where(x=>x.Id_Сеанс == Session.Id).ToList();

            var allPlaces = silverCinema.Места.ToList();

            foreach (var ticket in tickets) 
            {
                var placeDelete = silverCinema.Места.Where(x => x.Id == ticket.Id_Места).FirstOrDefault();
                allPlaces.Remove(placeDelete);
            }
            var places = allPlaces.ToList();

            var row = places.GroupBy(x=>x.Ряд).ToList();
            
            comboBox1.DataSource = row;
            comboBox1.DisplayMember = "Ряд";
            comboBox1.ValueMember = "Key";

            Int32 selectecValue = (Int32)comboBox1.SelectedValue;

            var place = places.Where(x => x.Ряд == selectecValue).ToList();

            comboBox2.DataSource = place;
            comboBox2.DisplayMember = "Место";
            comboBox2.ValueMember = "Id";

            label5.Text = Session.FilmName;
            label6.Text = Session.Date +""+ Session.Time; 
            label7.Text = Session.Price;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (silverCinemaEntities silverCinema = new silverCinemaEntities())
            {
                silverCinema.Билеты.Add(new Билеты
                {
                   Id_Сеанс = Session.Id,
                   Id_Места = (Int32)comboBox2.SelectedValue
                });
                silverCinema.SaveChanges();
            }
            MessageBox.Show("Билет куплен.");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            var tickets = silverCinema.Билеты.Where(x => x.Id_Сеанс == Session.Id).ToList();

            var allPlaces = silverCinema.Места.ToList();

            foreach (var ticket in tickets)
            {
                var placeDelete = silverCinema.Места.Where(x => x.Id == ticket.Id_Места).FirstOrDefault();
                allPlaces.Remove(placeDelete);
            }
            var places = allPlaces.ToList();

            var place = places.Where(x => x.Ряд == (Int32)comboBox1.SelectedValue).ToList();

            comboBox2.DataSource = place;
            comboBox2.DisplayMember = "Место";
            comboBox2.ValueMember = "Id";
        }
    }
}
