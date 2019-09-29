using Bogus;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly BindingList<User> _usersList = new BindingList<User>();
        public User SelectedUser { get; set; }

        private DateTime _start;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _start = DateTime.UtcNow;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var faker = new Faker("en");
            const double maxUsers = 100_000d;

            for (var i = 0; i < maxUsers; i++)
            {
                var u = new User()
                {
                    Id = i,
                    Name = i + " " + faker.Lorem.Word(),
                };

                for (var j = 0; j < faker.Random.Number(500); j++)
                {
                    var o = new Data()
                    {
                        Id = faker.Random.Number(1, 100),
                        Name = faker.Lorem.Word(),
                        User = u,
                    };

                    u.Datas.Add(o);
                }

                _usersList.Add(u);

                var progress = Math.Round((i + 1) / maxUsers * 100, 0, MidpointRounding.AwayFromZero);
                backgroundWorker1.ReportProgress((int)progress, u);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString();
            label2.Text = _usersList.Count.ToString();
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is User user)
            {
                BS_Data.DataSource = new BindingList<Data>(user.Datas);
                BS_Data.ResetBindings(false);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BS_User.DataSource = _usersList;
            label1.Text = (DateTime.UtcNow - _start).ToString();
            label2.Text = _usersList.Count.ToString();
        }
    }
}
