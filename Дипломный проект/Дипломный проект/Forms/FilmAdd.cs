using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Data;
using Дипломный_проект.Models;
using Extensions;

namespace Дипломный_проект.Forms
{
    public partial class FilmAdd : Form
    {
        public Int32? FilmId { get; }
        byte[] file = null;
        public FilmAdd(Int32? filmId)
        {
            FilmId = filmId;

            InitializeComponent();
        }
        private void FilmAdd_Load(object sender, EventArgs e)
        {
            silverCinemaEntities silverCinema = new silverCinemaEntities();

            comboBox1.DataSource = silverCinema.Жанр.ToList();
            comboBox1.DisplayMember = "Название";
            comboBox1.ValueMember = "Id";

            var ageLimit = Enum.GetValues(typeof(AgeLimit)).Cast<AgeLimit>()
                .Select(value => new
                {
                    Value = value.GetDisplayName(),
                    Key = (Int32)value
                })
                .ToList();

            comboBox2.DataSource = new BindingSource(ageLimit, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";

            if (FilmId == null)
            {
                this.Text = "Добавление фильма";            
            }
            else
            {
                this.Text = "Изменение фильма";
                photo.Text = "Изменить фото";

                using (silverCinema)
                {
                    var film = silverCinema.Фильм.Where(x => x.Id == FilmId).FirstOrDefault();

                    textBox1.Text = film.Название;
                    textBox2.Text = film.Описание;
                    textBox4.Text = film.Длительность;
                    comboBox1.SelectedValue = film.Жанр.Id;
                    var age = ageLimit.Where(x => x.Value == film.Возрастное_ограничение).Select(x => x.Key).ToList();
                    comboBox2.SelectedValue = age[0];
                    numericUpDown1.Value = film.Цена;
                }
            }
        }

        private void photo_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                photo.Text = filename;

                file = File.ReadAllBytes(filename);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) ||
                String.IsNullOrWhiteSpace(textBox2.Text) ||
               String.IsNullOrWhiteSpace(textBox4.Text) ||
               comboBox1.SelectedValue == null ||
               file == null)
            {
                MessageBox.Show("Неверный формат");
                return;
            }
            try
            {
                var ageLimits = Enum.GetValues(typeof(AgeLimit)).Cast<AgeLimit>()
                        .Select(value => new
                        {
                            Value = value.GetDisplayName(),
                            Key = (Int32)value
                        }).ToList();
                    var ageLimit = ageLimits.Where(x => x.Key == (Int32)comboBox2.SelectedValue).FirstOrDefault();
                if (FilmId == null)
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        silverCinema.Фильм.Add(new Фильм
                        {
                            Название = textBox1.Text,
                            Описание = textBox2.Text,                            
                            Возрастное_ограничение = ageLimit.Value,
                            Длительность = textBox4.Text,
                            Id_Жанр = (Int32)comboBox1.SelectedValue,
                            Постер = file,
                            Цена = (Int32)numericUpDown1.Value
                        });
                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Фильм создан.");
                }
                else
                {
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var film = silverCinema.Фильм.Where(x => x.Id == FilmId).FirstOrDefault();
                        if (film == null)
                        {
                            MessageBox.Show("Фильм не найден");
                            return;
                        }

                        film.Название = textBox1.Text;
                        film.Описание = textBox2.Text;
                        film.Возрастное_ограничение = ageLimit.Value;
                        film.Длительность = textBox4.Text;
                        film.Id_Жанр = (Int32)comboBox1.SelectedValue;
                        film.Постер = file;
                        film.Цена = (Int32)numericUpDown1.Value;

                        silverCinema.SaveChanges();
                    }
                    MessageBox.Show("Фильм изменен.");
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
