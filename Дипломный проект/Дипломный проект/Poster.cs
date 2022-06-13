using Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Дипломный_проект.Forms;
using Дипломный_проект.Models;

namespace Дипломный_проект
{

    public partial class Poster : Form
    {
        public Int32 TotalItems { get; set; }//всего объектов
        public Int32 PageNumber { get; set; }//номер текущей страницы
        public Int32 TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / 3); }
        }

        List<SessionViewModel> SessionsList = new List<SessionViewModel>();

        silverCinemaEntities silverCinema = new silverCinemaEntities();

        String AgeLimit = null;
        Int32 Genre = -1;

        public Poster()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = silverCinema.Город.ToList();
            comboBox1.DisplayMember = "Название";
            comboBox1.ValueMember = "Id";

            comboBox2.DataSource = silverCinema.Жанр.ToList();
            comboBox2.DisplayMember = "Название";
            comboBox2.ValueMember = "Id";

            var ageLimit = Enum.GetValues(typeof(AgeLimit)).Cast<AgeLimit>()
                .Select(value => new
                {
                    Value = value.GetDisplayName(),
                    Key = (Int32)value
                })
                .ToList();

            comboBox4.DataSource = new BindingSource(ageLimit, null);
            comboBox4.DisplayMember = "Value";
            comboBox4.ValueMember = "Key";

            PageNumber = 1;

            DateTime date = dateTimePicker1.Value.Date;
            Int32 city = (Int32)comboBox1.SelectedValue;

            Data_Load(city, date);
        }

        private void Data_Load(Int32 city, DateTime date)
        {
            SessionsList = new List<SessionViewModel>();

            var sessions = silverCinema.Сеанс.Where(x => x.Город.Id == city && x.Время.Дата == date).ToList(); 
            if (AgeLimit != null && Genre != -1)
            { 
                 sessions = sessions.Where(x=> x.Фильм.Возрастное_ограничение == AgeLimit && x.Фильм.Жанр.Id == Genre).ToList(); 
            }

            if (sessions.Count == 0)
            {
                label9.Text = "Сеансы не найдены.";
                label9.Visible = true;
                groupBox2.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                button14.Visible = false;
                button13.Visible = false;
            }
            else
            {
                label9.Visible = false;

                var sessionsGroup = sessions.GroupBy(x => x.Id_Фильм).ToList();
                TotalItems = sessionsGroup.Count();
                label16.Text = TotalPages.ToString();

                if (sessionsGroup.Count > 3)
                {
                    button14.Visible=true;
                    button13.Visible=true; 
                    var sessionList = sessionsGroup.Skip((PageNumber-1)*3).Take(3).ToList();

                    for (int i = 0; i < sessionList.Count; i++)
                    {
                        var session = sessionList[i].ToList()[0];

                        SessionViewModel sessionViewModel = new SessionViewModel
                        {
                            Id = session.Id,
                            FilmName = session.Фильм.Название,
                            City = session.Город.Название,
                            Time = session.Время.Время1.ToString(),
                            Price = session.Фильм.Цена.ToString(),
                            Date = session.Время.Дата.ToString(),
                            AgeLimit = session.Фильм.Возрастное_ограничение,
                            Description = session.Фильм.Описание,
                            Poster = session.Фильм.Постер
                        };

                        outputMovie(sessionViewModel, i);

                        SessionsList.Add(sessionViewModel);

                        List<TimeViewModel> timeList = sessions.Where(x => x.Фильм.Название == session.Фильм.Название).Select(x => new TimeViewModel
                        {
                            Id = x.Id,
                            Date = x.Время.Дата,
                            Time = x.Время.Время1
                        }).ToList();

                        outputTime(timeList, i);
                    }
                }
                else
                {
                    button14.Enabled = true;
                    label16.Visible = false;

                    for (int i = 0; i < sessionsGroup.Count; i++)
                    {
                        var session = sessionsGroup[i].ToList()[0];

                        SessionViewModel sessionViewModel = new SessionViewModel
                        {
                            Id = session.Id,
                            FilmName = session.Фильм.Название,
                            City = session.Город.Название,
                            Time = session.Время.Время1.ToString(),
                            Price = session.Фильм.Цена.ToString(),
                            Date = session.Время.Дата.ToString(),
                            AgeLimit = session.Фильм.Возрастное_ограничение,
                            Description = session.Фильм.Описание,
                            Poster = session.Фильм.Постер
                        };
                        outputMovie(sessionViewModel, i);

                        List<TimeViewModel> timeList = sessions.Where(x => x.Фильм.Название == session.Фильм.Название).Select(x => new TimeViewModel
                        {
                            Id = x.Id,
                            Date = x.Время.Дата,
                            Time = x.Время.Время1
                        }).ToList();

                        outputTime(timeList, i);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
        }

        private void outputTime(List<TimeViewModel> time, Int32 number) 
        {
            switch (number)
            {
                case 0:
                    comboBox3.DataSource = time;
                    comboBox3.DisplayMember = "Time";
                    comboBox3.ValueMember = "Id";

                    break;
                case 1:
                    comboBox5.DataSource = time;
                    comboBox5.DisplayMember = "Time";
                    comboBox5.ValueMember = "Id";

                    break;
                case 2:
                    comboBox6.DataSource = time;
                    comboBox6.DisplayMember = "Time";
                    comboBox6.ValueMember = "Id";

                    break;
            }
        }

        private void outputMovie(SessionViewModel session, Int32 number)
        {
            switch (number)
            {
                case 0:
                    groupBox4.Visible = true;
                    pictureBox1.Image = (Bitmap)((new ImageConverter()).ConvertFrom(session.Poster));
                    label6.Text = session.FilmName;
                    label7.Text = session.AgeLimit;
                    label8.Text = session.Description;
                    label17.Text = session.Price + " руб.";

                    break;
                case 1:
                    groupBox5.Visible = true;
                    pictureBox2.Image = (Bitmap)((new ImageConverter()).ConvertFrom(session.Poster));
                    label12.Text = session.FilmName;
                    label11.Text = session.AgeLimit;
                    label10.Text = session.Description;
                    label18.Text = session.Price + " руб.";

                    break;
                case 2:
                    groupBox2.Visible = true;
                    pictureBox3.Image = (Bitmap)((new ImageConverter()).ConvertFrom(session.Poster));
                    label15.Text = session.FilmName;
                    label14.Text = session.AgeLimit;
                    label13.Text = session.Description;
                    label19.Text = session.Price + " руб.";

                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            AgeLimit = null;
            Genre = -1;

            DateTime date = dateTimePicker1.Value.Date;
            Int32 city = (Int32)comboBox1.SelectedValue;

            Data_Load(city, date);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            button13.Enabled = true;

            PageNumber += 1;
            DateTime date = dateTimePicker1.Value.Date;
            Int32 city = (Int32)comboBox1.SelectedValue;
            Data_Load(city, date);
            if (TotalPages >= PageNumber) button14.Enabled = false;
            else button14.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            button14.Enabled = true;

            PageNumber -= 1;
            DateTime date = dateTimePicker1.Value.Date;
            Int32 city = (Int32)comboBox1.SelectedValue;
            Data_Load(city, date);
            if(PageNumber<=1) button13.Enabled = false;
            else button13.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

            var ageLimits = Enum.GetValues(typeof(AgeLimit)).Cast<AgeLimit>()
                       .Select(value => new
                       {
                           Value = value.GetDisplayName(),
                           Key = (Int32)value
                       }).ToList();
            var ageLimit = ageLimits.Where(x => x.Key == (Int32)comboBox4.SelectedValue).FirstOrDefault();

            AgeLimit = ageLimit.Value;
            Genre = (Int32)comboBox1.SelectedValue;
            DateTime date = dateTimePicker1.Value.Date;
            Int32 city = (Int32)comboBox1.SelectedValue;

            Data_Load(city, date);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String name = label6.Text;
            String date = dateTimePicker1.Value.Date.ToString().Remove(10,8);
            String time = comboBox3.Text;
            String price = label17.Text;
            Buy buy = new Buy(SessionsList[0]);
            buy.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String name = label12.Text;
            String date = dateTimePicker1.Value.Date.ToString().Remove(10, 8);
            String time = comboBox5.Text;
            String price = label18.Text;

            Buy buy = new Buy(SessionsList[1]);
            buy.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String name = label15.Text;
            String date = dateTimePicker1.Value.Date.ToString().Remove(10, 8);
            String time = comboBox6.Text;
            String price = label19.Text;

            Buy buy = new Buy(SessionsList[2]);
            buy.Show();
        }
    }
}
