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
            this.InitializeComponent();

            FontLoader.LoadFont(this.lblTitle, 16.0F);
            FontLoader.LoadFont(this.btnPlay, 12.0F);
        }
    }
}
