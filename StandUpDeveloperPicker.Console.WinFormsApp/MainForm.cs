using Microsoft.VisualBasic;
using StandUpDeveloperPicker.Core.Interfaces;

namespace StandUpDeveloperPicker.Console.WinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IDeveloperBl _developerBl;
        private int Index { get; set; }

        public MainForm(IDeveloperBl developerBl)
        {
            _developerBl = developerBl;
            InitializeComponent();

            SetScreenResponse();
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
            btnNext.Enabled = Index < _developerBl.DeveloperCount - 1;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Index--;
            SetScreenResponse();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Index++;
            SetScreenResponse();

        }
    }
}