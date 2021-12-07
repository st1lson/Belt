using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using BeltGUI.Properties;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        private readonly Bitmap[] _sprites;
        public GameMenu()
        {
            InitializeComponent();
            _sprites = InitializeSprites();
        }

        private Bitmap[] InitializeSprites()
        {
            Bitmap[] sprites = new Bitmap[53];
            ResourceSet resourceSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, true);
            if (resourceSet is null)
            {
                return default;
            }

            int i = 0;
            foreach (DictionaryEntry entry in resourceSet)
            {
                _sprites[i++] = (Bitmap)entry.Value;
            }

            return sprites;
        }
    }
}
