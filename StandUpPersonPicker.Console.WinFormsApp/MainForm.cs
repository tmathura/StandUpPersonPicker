using Microsoft.Extensions.Configuration;
using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;

namespace StandUpPersonPicker.Console.WinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IPersonBl _personBl;
        private int Index { get; set; }

        public MainForm(IPersonBl personBl, IConfigurationRoot configuration)
        {
            _personBl = personBl;

            var settings = new Settings();
            configuration.Bind(settings);

            var personNames = settings.PersonNames;

            InitializeComponent();

            foreach (var personName in personNames)
            {
                clbPersons.Items.Add(personName, CheckState.Checked);
            }

            lblResult.Text = "Press the start button to initiate stand ups.";
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
        }

        private void SetScreenResponse()
        {
            var personResponse = _personBl.GetPersonByIndex(Index);

            if (personResponse.Errors.Any())
            {
                lblResult.Text = string.Join(", ", personResponse.Errors);
                pbResult.ImageLocation = string.Empty;

            }
            else
            {
                lblResult.Text = $"The current person to speak is '{personResponse.Name}' and their character is '{personResponse.Character.Name}'.";
                pbResult.ImageLocation = personResponse.Character.Image;
            }

            btnPrevious.Enabled = !(Index <= 0);
            btnNext.Enabled = Index < clbPersons.CheckedItems.Count - 1;
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPersonName.Text))
            {
                clbPersons.Items.Add(txtPersonName.Text, CheckState.Checked);

                txtPersonName.Clear();
            }

        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            var checkedPerson = (from string clbPersonsCheckedItem in clbPersons.CheckedItems select clbPersonsCheckedItem).ToList();

            await _personBl.CreateCharacterPersonPairs(checkedPerson);
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