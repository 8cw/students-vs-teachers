// <summary>
// Contains the frmGame form.
// </summary>
// <copyright file="FrmGame.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The main game screen.
    /// </summary>
    public partial class FrmGame : Form
    {
        private const int GRID_LENGTH = 16;
        private const int GAME_X_LENGTH = 1920;
        private const int GAME_Y_LENGTH = 1024;

        private readonly Grid[] grid = new Grid[7680];

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmGame"/> class.
        /// Ran when the game form is created.
        /// </summary>
        public FrmGame()
        {
            InitializeComponent();
            Console.WriteLine(grid);
            CreateGrid();
            Console.WriteLine(grid);
        }

        /// <summary>
        /// Creates the grid-system for items.
        /// </summary>
        private void CreateGrid()
        {
            for (var i = 0; i < (GAME_X_LENGTH / GRID_LENGTH) * (GAME_Y_LENGTH / GRID_LENGTH); i += 1)
            {
                grid[i] = new Grid(i);
            }
        }
    }
}
