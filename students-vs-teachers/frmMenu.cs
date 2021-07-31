// <summary>
// Contains the frmMenu form.
// </summary>
// <copyright file="frmMenu.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System.Windows.Forms;

    /// <summary>
    /// The main menu screen.
    /// </summary>
    public partial class FrmMenu : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMenu"/> class.
        /// Ran when the menu form is created.
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();

            FontLoader.LoadFont(lblTitle, 24.0F);

            FontLoader.LoadFont(btnPlay, 12.0F);
            FontLoader.LoadFont(btnHelp, 12.0F);
            FontLoader.LoadFont(btnQuit, 12.0F);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnPlay_Click(object sender, System.EventArgs e)
        {
            var frmGame = new FrmGame();
            frmGame.ShowDialog();
            Dispose();
            Close();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnQuit_Click(object sender, System.EventArgs e)
        {
            Dispose();
            Close();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            var frmHelp = new FrmHelp();
            frmHelp.ShowDialog();
            Dispose();
            Close();
        }
    }
}
