// <summary>
// A form that alerts the client that they lost the game.
// </summary>
// <copyright file="FrmGameLose.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System.Windows.Forms;

    /// <summary>
    /// A form that alerts the client that they lost the game.
    /// </summary>
    public partial class FrmGameLose : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmGameLose"/> class.
        /// </summary>
        public FrmGameLose()
        {
            InitializeComponent();

            // Load "gameria" font
            FontLoader.LoadFont(lblTitle, 24.0F);
            FontLoader.LoadFont(lblSubtitle, 12.0F);

            FontLoader.LoadFont(btnHome, 12.0F);
            FontLoader.LoadFont(btnQuit, 12.0F);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnHome_Click(object sender, System.EventArgs e)
        {
            Visible = false;

            var frmMenu = new FrmMenu();
            frmMenu.ShowDialog();

            Dispose();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnQuit_Click(object sender, System.EventArgs e)
        {
            Dispose();
            Close();
        }
    }
}
