// <summary>
// Contains the FrmHelp form.
// </summary>
// <copyright file="FrmHelp.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System.Windows.Forms;

    /// <summary>
    /// A helpful screen to aid users in the game.
    /// </summary>
    public partial class FrmHelp : Form
    {
        private static readonly HelpPageInfo[] PageInformation = new[]
        {
        };

        private int pageNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmHelp"/> class.
        /// Creates the help menu.
        /// </summary>
        public FrmHelp()
        {
            InitializeComponent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnNext_Click(object sender, System.EventArgs e)
        {
            pageNumber = (pageNumber + 1) % 3;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnPrevious_Click(object sender, System.EventArgs e)
        {

        }
    }
}
