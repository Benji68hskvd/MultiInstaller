using System.Diagnostics;

namespace MultiInstaller
{
    public partial class Form1 : Form
    {
        public bool Install { get; private set; }
        //private Label statusLabel;

        public Form1()
        {
            InitializeComponent();
            //Install = false;
        }

        //private void InstallButton_Click(object sender, EventArgs e)
        //{
        //    //Install = true;
        //    //Close();
        //    Form1.UpdateStatusLabel("Start install...");
        //    Debug.WriteLine("bouton start d�but install");
        //    Functions func = new();
        //    func.Install(this);
        //}

        private async void InstallButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("bouton start d�but install");

            // D�sactiver tous les contr�les
            SetControlsEnabled(false);

            //Functions func = new();
            //await Task.Run(() => func.Install(this)); // Appeler Install avec `this`

            Functions func = new();
            await func.Install(this); // Appeler Install en respectant son asynchronisme

            // R�activer tous les contr�les apr�s l'installation
            SetControlsEnabled(true);
            UpdateStatusLabel("Installation termin�e.");
        }

        private void CancelButton_Click_1(object sender, EventArgs e)
        {
            //Close();
            Application.Exit();
        }

        //public static void UpdateStatusLabel(string text)
        //{
        //    labelStatus.Text = text;
        //    //labelStatus.Location = new Point((ClientSize.Width - labelStatus.PreferredWidth) / 2, labelStatus.Location.Y);
        //}

        public static void UpdateStatusLabel(string text)
        {
            if (labelStatus.InvokeRequired)
            {
                // Ex�cute la mise � jour sur le thread principal
                labelStatus.Invoke(new Action(() => labelStatus.Text = text));
            }
            else
            {
                // Si on est d�j� sur le thread principal, on peut mettre � jour directement
                labelStatus.Text = text;
            }
        }

        private void SetControlsEnabled(bool isEnabled)
        {
            foreach (Control control in Controls)
            {
                if (control is Button || control is CheckBox) // D�sactiver uniquement les boutons et checkboxes
                {
                    control.Enabled = isEnabled;
                }
            }
        }
    }
}
