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
        private static readonly HelpPageInfo[] PageInformation = new HelpPageInfo[]
        {
            new HelpPageInfo { Image = Properties.Resources._01_helpImage },
            new HelpPageInfo { Image = Properties.Resources._02_helpImage },
        };

        private int pageNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmHelp"/> class.
        /// Creates the help menu.
        /// </summary>
        public FrmHelp()
        {
            InitializeComponent();

            FontLoader.LoadFont(btnNext, 12.0F);
            FontLoader.LoadFont(btnPrevious, 12.0F);

            RefreshHelpImage();
        }

        private void RefreshHelpImage()
        {
            BackgroundImage = PageInformation[pageNumber].Image;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnNext_Click(object sender, System.EventArgs e)
        {
            pageNumber = (pageNumber + 1) % PageInformation.Length;
            RefreshHelpImage();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            pageNumber = (pageNumber - 1 + PageInformation.Length) % PageInformation.Length;
            RefreshHelpImage();
        }
    }
}
