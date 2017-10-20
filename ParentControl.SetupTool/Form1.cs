using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using ParentControl.DTO;
using ParentControl.Infrastructure.Configuration;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service;
using ParentControl.Infrastructure.Service.Model;
using Device = ParentControl.Infrastructure.Service.Model.Device;
using Timesheet = ParentControl.DTO.Timesheet;

namespace ParentControl.SetupTool
{
    public partial class SetupTool : Form
    {
        private IHttpService _httpService;
        private IConfiguration _configService;
        private IParentControlService _parentControlService;
        private Schedule _schedule;
        public SetupTool()
        {
            var server = ConfigurationManager.AppSettings["ServerUri"];
            if (string.IsNullOrEmpty(server))
            {
                throw new Exception("No server in app config.");
            }
            _configService = new ClientConfiguration();
            _parentControlService = new ParentControlService();
            _httpService = new HttpService(_configService);
            InitializeComponent();
            RefreshControls();
        }

        private void RefreshControls()
        {
            tbPassword.Text = _configService.Config.AuthenticationData?.Password;
            tbLogin.Text = _configService.Config.AuthenticationData?.Username;
            tbApi.Text = _configService.Config.ServerAddress;
            if (_configService.Config.AuthenticationData != null)
            {
                tbDevice.Text = _parentControlService.DeviceService.GenerateDeviceId().ToString();
                tbDeviceName.Text = _configService.Config.Device?.DeviceName;
            }

            if (_parentControlService.IsConnected) { 
                lConnected.Text = "Connected";
                tbToken.Text = _httpService.Token;
            }

            if (_parentControlService.IsConnected)
            { 
                try
                {
                    _schedule = _parentControlService.ScheduleService.GetDeviceSchedule(tbDevice.Text);
                    _schedule.Timesheets?.ToList().ForEach(s => listBox1.Items.Add($"From: {s.DateFrom.ToString()} -> To: {s.DateTo?.ToString()} \t Time: {s.Time.Minutes} min"));
                }
                catch(Exception ex)
                {
                    
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                var token = _httpService.Authenticate(tbLogin.Text, tbPassword.Text);
                if (string.IsNullOrEmpty(token.AccessToken))
                {
                    MessageBox.Show("Empty");
                    return;
                }
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnAuthSave_Click(object sender, EventArgs e)
        {
            _configService.SaveAuthentication(new AuthenticationData()
            {
                Username = tbLogin.Text,
                Password = tbPassword.Text
            });

            MessageBox.Show("Ok");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void bApi_Click(object sender, EventArgs e)
        {
            _configService.SaveServerAddress(tbApi.Text);
            _configService.SaveDevice(tbDevice.Text,tbDeviceName.Text);

            MessageBox.Show("Ok");
        }

        private void tbRegDevice_Click(object sender, EventArgs e)
        {
            _parentControlService.DeviceService.RegisterDevice(new Device()
            {
                DeviceId = tbDevice.Text,
                DeviceName = tbDeviceName.Text
            });
            _schedule = _parentControlService.ScheduleService.GetDeviceSchedule(tbDevice.Text);
        }

        private void btnAddTimesheet_Click(object sender, EventArgs e)
        {
            if (_schedule == null)
            {
                MessageBox.Show("No schedule created!");
            }
            var timesheet = new Timesheet()
            {
                DateFrom = dtFrom.Value,
                DateTo = dtTo.Enabled ? dtTo.Value : (DateTime?) null,
                Time = new TimeSpan(0,int.Parse(nTime.Value.ToString()), 0),
                ScheduleId = _schedule.Id
            };

            _parentControlService.ScheduleService.AddTimesheet(timesheet);
        }

        private void cbNoToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtTo.Enabled = cbNoToDate.Checked;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
