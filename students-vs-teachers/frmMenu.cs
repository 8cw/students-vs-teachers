using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using System.Drawing.Text;
namespace students_vs_teachers
{
    public partial class frmMenu : Form
    {
        // import Graphics Device Interface
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        /// <summary>
        /// The collection which will hold all our custom fonts
        /// </summary>
        private PrivateFontCollection gameFonts = new PrivateFontCollection();

        /// <summary>
        /// The gameria font.
        /// </summary>
        Font gameria;

        /// <summary>
        /// Ran when the menu form is created.
        /// </summary>
        public frmMenu()
        {
            InitializeComponent();

            // create the font data and add it to our font family.
            byte[] fontData = Properties.Resources.GAMERIA;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            gameFonts.AddMemoryFont(fontPtr, Properties.Resources.GAMERIA.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.GAMERIA.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            gameria = new Font(gameFonts.Families[0], 16.0F);

            lblTitle.Font = gameria;
        }
    }
}
