// <summary>
// Contains the frmGame form.
// </summary>
// <copyright file="FrmGame.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The main game screen.
    /// </summary>
    public partial class FrmGame : Form
    {
        private const int GRID_LENGTH = 32;

        private readonly Grid[] grid = new Grid[1920];
        private uint round = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmGame"/> class.
        /// Ran when the game form is created.
        /// </summary>
        public FrmGame()
        {
            InitializeComponent();

            // add "gameria" font to buttons
            FontLoader.LoadFont(btnToggleState, 12.0F);
            FontLoader.LoadFont(btnSettings, 12.0F);
            FontLoader.LoadFont(btnHelp, 12.0F);
            FontLoader.LoadFont(lblMoney, 16.0F);
            FontLoader.LoadFont(lblLives, 16.0F);

            CreateGrid();
        }

        /// <summary>
        /// Creates the grid-system for items.
        /// </summary>
        private void CreateGrid()
        {
            for (var i = 0; i < (pnlGame.Size.Width / GRID_LENGTH) * (pnlGame.Size.Height / GRID_LENGTH); i += 1)
            {
                var x = i % (pnlGame.Size.Width / GRID_LENGTH);
                var y = (int)Math.Floor((double)(i / (pnlGame.Size.Width / GRID_LENGTH)));

                var gridX = x * GRID_LENGTH;
                var gridY = y * GRID_LENGTH;

                var gridImage = new PictureBox();
                gridImage.Size = new Size(GRID_LENGTH, GRID_LENGTH);
                gridImage.Location = new Point(gridX, gridY);
                gridImage.Visible = true;
                gridImage.BackColor = Color.Transparent;
                gridImage.Image = ((i + y) % 2) == 0 ? Properties.Resources.blue_box : Properties.Resources.red_box;

                pnlGame.Controls.Add(gridImage);
                grid[i] = new Grid(i, gridImage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnHelp_Click(object sender, EventArgs e)
        {
            tmrGameTick.Enabled = false;

            var frmHelp = new FrmHelp();
            frmHelp.ShowDialog();
        }
    }
}
