using System.Windows.Forms;

namespace BeltGUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object sender, System.EventArgs e)
        {
            GameMenu gameMenu = new();
            gameMenu.Location = Location;
            gameMenu.StartPosition = FormStartPosition.Manual;
            gameMenu.FormClosing += delegate { Show(); };
            gameMenu.Show();
            Hide();
        }
    }
}
