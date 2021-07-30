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

            Console.WriteLine(grid);
            CreateGrid();
            Console.WriteLine(grid);
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
                gridImage.BackColor = ((i + y) % 2) == 0 ? Color.Green : Color.Red;

                pnlGame.Controls.Add(gridImage);
                grid[i] = new Grid(i, gridImage);
            }
        }
    }
}
