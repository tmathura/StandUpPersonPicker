using Microsoft.Extensions.Configuration;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;

namespace StandUpDeveloperPicker.Console.WinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IDeveloperBl _developerBl;
        private int Index { get; set; }

        public MainForm(IDeveloperBl developerBl, IConfigurationRoot configuration)
        {
            _developerBl = developerBl;

            var settings = new Settings();
            configuration.Bind(settings);

            var developerNames = settings.DeveloperNames;

            InitializeComponent();

            foreach (var developerName in developerNames)
            {
                clbDevelopers.Items.Add(developerName, CheckState.Checked);
            }

            lblResult.Text = "Press the start button to iniate stand ups.";
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
        }

        private void SetScreenResponse()
        {
            var developerResponse = _developerBl.GetDeveloperByIndex(Index);

            if (developerResponse.Errors.Any())
            {
                lblResult.Text = string.Join(", ", developerResponse.Errors);
                pbResult.ImageLocation = string.Empty;

            }
            else
            {
                lblResult.Text = $"The current person to speak is '{developerResponse.Name}' and their character is '{developerResponse.Character.Name}'.";
                pbResult.ImageLocation = developerResponse.Character.Image;
            }

            btnPrevious.Enabled = !(Index <= 0);
            btnNext.Enabled = Index < clbDevelopers.CheckedItems.Count - 1;
        }

        private void BtnAddDeveloper_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDeveloperName.Text))
            {
                clbDevelopers.Items.Add(txtDeveloperName.Text, CheckState.Checked);

                txtDeveloperName.Clear();
            }

        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            var checkedDeveloper = (from string clbDevelopersCheckedItem in clbDevelopers.CheckedItems select clbDevelopersCheckedItem).ToList();

            await _developerBl.CreateCharacterDeveloperPairs(checkedDeveloper);
            SetScreenResponse();

            btnStart.Text = "Restart";
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            Index--;
            SetScreenResponse();

        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            Index++;
            SetScreenResponse();

        }
    }
}