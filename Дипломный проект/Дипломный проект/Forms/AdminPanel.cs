using Extensions;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Дипломный_проект.Models;

namespace Дипломный_проект.Forms
{
    public partial class AdminPanel : Form
    {
        silverCinemaEntities silverCinema = new silverCinemaEntities();
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            Data_Load();
        }

        private void Data_Load(String searchByName = "")
        {
            String currentTab = TabControl.SelectedTab.Text;

            using (silverCinemaEntities silverCinema = new silverCinemaEntities())
            {
                switch (currentTab)
                {
                    case "Фильмы":

                        textBox1.Visible = true;
                        label1.Visible = true;
                        button4.Visible = true;
                        groupBox1.Visible = false;

                        var filmbd = silverCinema.Фильм.ToList();

                        if (!String.IsNullOrWhiteSpace(searchByName))
                            filmbd = filmbd.Where(x => x.Название.Contains(searchByName)).ToList();

                        var films = filmbd.Select(film => new FilmViewModel
                        {
                            Id = film.Id,
                            Name = film.Название,
                            Description = film.Описание,
                            Price = film.Цена,
                            AgeLimit = film.Возрастное_ограничение,
                            Duration = film.Длительность,
                            Genre = film.Жанр.Название,
                            Poster = film.Постер
                        });

                        dataGridView1.DataSource = films.ToList();

                        break;
                    case "Города":

                        textBox1.Visible = true;
                        label1.Visible = true;
                        button4.Visible = true;
                        groupBox1.Visible = false;

                        var citybd = silverCinema.Город.ToList();

                        if (!String.IsNullOrWhiteSpace(searchByName))
                            citybd = citybd.Where(x => x.Название.Contains(searchByName)).ToList();

                        var cities = citybd.Select(city => new CityViewModel
                        {
                            Id = city.Id,
                            Name = city.Название
                        });

                        dataGridView2.DataSource = cities.ToList();

                        break;
                    case "Сеансы":

                        textBox1.Visible = false;
                        label1.Visible = false;
                        button4.Visible = false;
                        groupBox1.Visible = true;

                        comboBox1.DataSource = silverCinema.Фильм.ToList();
                        comboBox1.DisplayMember = "Название";
                        comboBox1.ValueMember = "Id";

                        comboBox2.DataSource = silverCinema.Город.ToList();
                        comboBox2.DisplayMember = "Название";
                        comboBox2.ValueMember = "Id";

                        var sessions = silverCinema.Сеанс.Select(session => new SessionViewModel
                        {
                            Id = session.Id,
                            FilmName = session.Фильм.Название,
                            City = session.Город.Название,
                            Time= session.Время.Время1.ToString(),
                            Date  = session.Время.Дата.ToString(),
                            AgeLimit = session.Фильм.Возрастное_ограничение,
                            Description = session.Фильм.Описание,
                            Poster = session.Фильм.Постер,
                            Price = session.Фильм.Цена.ToString()
                        });

                        dataGridView4.DataSource = sessions.ToList(); ;

                        break;
                    case "Время":

                        textBox1.Visible = false;
                        label1.Visible = false;
                        button4.Visible = false;
                        groupBox1.Visible = false;

                        var Time = silverCinema.Время.Select(time => new TimeViewModel
                        {
                            Id = time.Id,
                            Date = time.Дата,
                            Time = time.Время1
                        });

                        dataGridView3.DataSource = Time.ToList();

                        break;
                    case "Места":

                        textBox1.Visible = false;
                        label1.Visible = false;
                        button4.Visible = false;
                        groupBox1.Visible = false;

                        var place = silverCinema.Места.Select(time => new PlaceViewModel
                        {
                            Id = time.Id,
                            Row = time.Ряд,
                            Place = time.Место
                        });

                        dataGridView5.DataSource = place.ToList();

                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String currentTab = TabControl.SelectedTab.Text;

            switch (currentTab)
            {
                case "Фильмы":
                    FilmAdd filmAdd = new FilmAdd(null);
                    filmAdd.ShowDialog();

                    break;
                case "Города":
                    CityAdd cityAdd = new CityAdd(null);
                    cityAdd.ShowDialog();

                    break;
                case "Сеансы":
                    SessionAdd sessionAdd = new SessionAdd(null);
                    sessionAdd.ShowDialog();

                    break;
                case "Время":
                    TimeAdd timeAdd = new TimeAdd(null);
                    timeAdd.ShowDialog();

                    break;
                case "Места":
                    PlaceAdd placeAdd = new PlaceAdd(null);
                    placeAdd.ShowDialog();
                    break;
            }

            Data_Load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String currentTab = TabControl.SelectedTab.Text;

            switch (currentTab)
            {
                case "Фильмы":
                    Int32 selectedFilmtIndex = dataGridView1.CurrentCell.RowIndex;
                    FilmViewModel filmViewModel = dataGridView1.Rows[selectedFilmtIndex].DataBoundItem as FilmViewModel;
                    FilmAdd filmAdd = new FilmAdd(filmViewModel.Id);
                    filmAdd.ShowDialog();

                    break;
                case "Города":
                    Int32 selectedCityIndex = dataGridView2.CurrentCell.RowIndex;
                    CityViewModel cityViewModel = dataGridView2.Rows[selectedCityIndex].DataBoundItem as CityViewModel;
                    CityAdd cityAdd = new CityAdd(cityViewModel.Id);
                    cityAdd.ShowDialog();

                    break;
                case "Сеансы":
                    Int32 selectedSessionIndex = dataGridView4.CurrentCell.RowIndex;
                    SessionViewModel sessionViewModel = dataGridView4.Rows[selectedSessionIndex].DataBoundItem as SessionViewModel;
                    SessionAdd sessionAdd = new SessionAdd(sessionViewModel.Id);
                    sessionAdd.ShowDialog();

                    break;
                case "Время":
                    Int32 selectedTimetIndex = dataGridView3.CurrentCell.RowIndex;
                    TimeViewModel timeViewModal = dataGridView3.Rows[selectedTimetIndex].DataBoundItem as TimeViewModel;
                    TimeAdd timeAdd = new TimeAdd(timeViewModal.Id);
                    timeAdd.ShowDialog();

                    break;
                case "Места":
                    Int32 selectedPlaceIndex = dataGridView5.CurrentCell.RowIndex;
                    PlaceViewModel placeViewModel = dataGridView5.Rows[selectedPlaceIndex].DataBoundItem as PlaceViewModel;
                    PlaceAdd placeAdd = new PlaceAdd(placeViewModel.Id);
                    placeAdd.ShowDialog();

                    break;
            }

            Data_Load();
        }


        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Data_Load();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            String currentTab = TabControl.SelectedTab.Text;

            switch (currentTab)
            {
                case "Фильмы":
                    Int32 selectedFilmtIndex = dataGridView1.CurrentCell.RowIndex;
                    FilmViewModel filmViewModel = dataGridView1.Rows[selectedFilmtIndex].DataBoundItem as FilmViewModel;
                    
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var film = silverCinema.Фильм.Where(x => x.Id == filmViewModel.Id).FirstOrDefault();

                        if (film.Сеанс.Count > 0)
                        {
                            MessageBox.Show("Выбранный фильм внесен в сеансы. Удаление невозможно");
                            return;
                        }

                        DialogResult result = MessageBox.Show($"Удалить продукт {film.Название}?", "Удаление", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            silverCinema.Фильм.Remove(film);
                            silverCinema.SaveChanges();
                        }
                    }

                    break;
                case "Города":
                    Int32 selectedCityIndex = dataGridView2.CurrentCell.RowIndex;
                    CityViewModel cityViewModel = dataGridView2.Rows[selectedCityIndex].DataBoundItem as CityViewModel;
                    
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var city = silverCinema.Город.Where(x => x.Id == cityViewModel.Id).FirstOrDefault();

                        if (city.Сеанс.Count > 0)
                        {
                            MessageBox.Show("Выбранный город внесен в сеансы. Удаление невозможно");
                            return;
                        }

                        DialogResult result = MessageBox.Show($"Удалить город {city.Название}?", "Удаление", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            silverCinema.Город.Remove(city);
                            silverCinema.SaveChanges();
                        }
                    }

                    break;
                case "Сеансы":
                    Int32 selectedSessionIndex = dataGridView4.CurrentCell.RowIndex;
                    SessionViewModel sessionViewModel = dataGridView4.Rows[selectedSessionIndex].DataBoundItem as SessionViewModel;

                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var session = silverCinema.Сеанс.Where(x => x.Id == sessionViewModel.Id).FirstOrDefault();

                        DialogResult result = MessageBox.Show($"Удалить сеанс фильма {session.Фильм.Название}?", "Удаление", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            silverCinema.Сеанс.Remove(session);
                            silverCinema.SaveChanges();
                        }
                    }

                    break;
                case "Время":
                    Int32 selectedTimetIndex = dataGridView3.CurrentCell.RowIndex;
                    TimeViewModel timeViewModal = dataGridView3.Rows[selectedTimetIndex].DataBoundItem as TimeViewModel;
                   
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var time = silverCinema.Время.Where(x => x.Id == timeViewModal.Id).FirstOrDefault();

                        if (time.Сеанс.Count > 0)
                        {
                            MessageBox.Show("Выбранное время используется в сеансах. Удаление невозможно");
                            return;
                        }

                        DialogResult result = MessageBox.Show($"Удалить выбранную дату {time.Дата}  {time.Время1} ?", "Удаление", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            silverCinema.Время.Remove(time);
                            silverCinema.SaveChanges();
                        }
                    }

                    break;
                case "Места":
                    Int32 selectedPlaceIndex = dataGridView5.CurrentCell.RowIndex;
                    PlaceViewModel placeViewModel = dataGridView5.Rows[selectedPlaceIndex].DataBoundItem as PlaceViewModel;
                   
                    using (silverCinemaEntities silverCinema = new silverCinemaEntities())
                    {
                        var place = silverCinema.Места.Where(x => x.Id == placeViewModel.Id).FirstOrDefault();

                        if (place.Билеты.Count >0)
                        {
                            MessageBox.Show("Выбранное место уже куплено. Удаление невозможно");
                            return;
                        }

                        DialogResult result = MessageBox.Show($"Удалить выбранное место? Ряд: {place.Ряд}  Место: {place.Место} ?", "Удаление", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            silverCinema.Места.Remove(place);
                            silverCinema.SaveChanges();
                        }
                    }

                    break;
            }
            Data_Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String searchByName = textBox1.Text;
            Data_Load(searchByName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Int32 film = (Int32)comboBox1.SelectedValue;
            Int32 city = (Int32)comboBox2.SelectedValue;

            using (silverCinemaEntities silverCinema = new silverCinemaEntities())
            {
                var sessions = silverCinema.Сеанс.Where(x => x.Фильм.Id == film && x.Город.Id == city).ToList();

                var sessionsList = sessions.Select(session => new SessionViewModel
                {
                    Id = session.Id,
                    FilmName = session.Фильм.Название,
                    City = session.Город.Название,
                    Time = session.Время.Время1.ToString(),
                    Date = session.Время.Дата.ToString()
                }).ToList();

                dataGridView4.DataSource = sessionsList;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
