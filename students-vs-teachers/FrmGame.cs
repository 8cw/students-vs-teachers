// <summary>
// Contains the frmGame form.
// </summary>
// <copyright file="FrmGame.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// The main game screen.
    /// </summary>
    public partial class FrmGame : Form
    {
        private const int GRID_LENGTH = 32;

        private readonly Grid[] grid = new Grid[1920];
        private readonly int[] enemyPath = { 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 970, 910, 850, 790, 730, 670, 610, 550, 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500, 501, 561, 621, 681, 741, 801, 861, 921, 981, 1041, 1101, 1161, 1221, 1222, 1223, 1224, 1225, 1226, 1227, 1228, 1229, 1230, 1231, 1232, 1233, 1234, 1235, 1236, 1237, 1238, 1178, 1118, 1058, 998, 938, 878, 879, 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890, 891, 892, 893, 894, 895, 896, 897, 898, 899 };
        private readonly Dictionary<int, TowerInfo> towerCosts = new Dictionary<int, TowerInfo>()
        {
            { 0, new TowerInfo { Name = "Year 9", Cost = 100, Damage = 1, AttackInterval = 1F, TowerImage = Properties.Resources.tower00 } },
            { 1, new TowerInfo { Name = "Year 10", Cost = 250, Damage = 2, AttackInterval = 0.8F, TowerImage = Properties.Resources.tower01 } },
            { 2, new TowerInfo { Name = "Year 11", Cost = 600, Damage = 6, AttackInterval = 2F, TowerImage = Properties.Resources.tower02 } },
            { 3, new TowerInfo { Name = "Year 13", Cost = 800, Damage = 8, AttackInterval = 1.6F, TowerImage = Properties.Resources.tower03 } },
            { 4, new TowerInfo { Name = "Year 12", Cost = 1000, Damage = 12, AttackInterval = 1.2F, TowerImage = Properties.Resources.tower04 } },
            { 5, new TowerInfo { Name = "Prefect", Cost = 2000, Damage = 18, AttackInterval = 1F, TowerImage = Properties.Resources.tower05 } },
            { 6, new TowerInfo { Name = "Head Boy", Cost = 2500, Damage = 20, AttackInterval = 0.7F, TowerImage = Properties.Resources.tower06 } },
            { 7, new TowerInfo { Name = "Top 6", Cost = 4000, Damage = 40, AttackInterval = 3F, TowerImage = Properties.Resources.tower07 } },
        };

        private List<Enemy> activeEnemies = new List<Enemy>();
        private int round = 0;
        private uint enemySpawnCount = 0;
        private int? towerPlacing = null;
        private int money = 9999;

        private Timer tmrGameTick = new Timer();
        private Timer tmrTowerPlacement = new Timer();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmGame"/> class.
        /// Ran when the game form is created.
        /// </summary>
        public FrmGame()
        {
            InitializeComponent();

            // create "tmrGameTick"
            tmrGameTick.Enabled = true;
            tmrGameTick.Interval = 33;
            tmrGameTick.Tick += new System.EventHandler(tmrGameTick_Tick);

            // create "tmrTowerPlacement"
            tmrTowerPlacement.Enabled = false;
            tmrTowerPlacement.Interval = 1;
            tmrTowerPlacement.Tick += new EventHandler(tmrTowerPlacement_Tick);

            // add "gameria" font to buttons
            FontLoader.LoadFont(btnToggleState, 12.0F);
            FontLoader.LoadFont(btnSettings, 12.0F);
            FontLoader.LoadFont(btnHelp, 12.0F);

            // add "gameria" font to labels
            FontLoader.LoadFont(lblMoney, 16.0F);
            FontLoader.LoadFont(lblLives, 16.0F);
            FontLoader.LoadFont(lblRound, 16.0F);
            FontLoader.LoadFont(lblTowers, 16.0F);

            CreateGrid();
        }

        /// <summary>
        /// Retrieves the grid x,y coordinate for a given grid ID square.
        /// For example, grid id 1 would return Point{0,1}.
        /// </summary>
        /// <param name="id">The id of the grid square.</param>
        /// <returns>The x,y coordinate of that grid.</returns>
        private Point GetGridCoordinatesForId(int id)
        {
            var x = id % (pnlGame.Size.Width / GRID_LENGTH);
            var y = (int)Math.Floor((double)(id / (pnlGame.Size.Width / GRID_LENGTH)));

            return new Point(x, y);
        }

        /// <summary>
        /// Retrieves the world grid space for a specified grid ID square.
        /// </summary>
        /// <param name="id">The id of the grid square.</param>
        /// <returns>A point representing the world coordinates for that ID grid.</returns>
        private Point GetMapCoordinateForId(int id)
        {
            var gridCoordinate = GetGridCoordinatesForId(id);

            return new Point(gridCoordinate.X * GRID_LENGTH, gridCoordinate.Y * GRID_LENGTH);
        }

        /// <summary>
        /// Retrieves the location an enemy should be given the distance they have travelled.
        /// </summary>
        /// <param name="enemyDistance">An integer representing how many units the enemy has travelled.</param>
        /// <returns>A point representing where the enemy should be.</returns>
        private Point GetEnemyLocation(int enemyDistance)
        {
            var gridNumber = enemyPath.ElementAtOrDefault(enemyDistance / GRID_LENGTH);
            if (gridNumber == 0)
            {
                return new Point(0, 0);
            }
            else
            {
                var gridLocation = GetMapCoordinateForId(gridNumber);
                return new Point(gridLocation.X, gridLocation.Y);
            }
        }

        /// <summary>
        /// Creates the grid-system for items.
        /// </summary>
        private void CreateGrid()
        {
            for (var i = 0; i < (pnlGame.Size.Width / GRID_LENGTH) * (pnlGame.Size.Height / GRID_LENGTH); i += 1)
            {
                var gridCoordinate = GetMapCoordinateForId(i);

                var gridImage = new PictureBox();
                gridImage.Size = new Size(GRID_LENGTH, GRID_LENGTH);
                gridImage.Location = gridCoordinate;
                gridImage.Visible = false;
                gridImage.BackColor = Color.Transparent;

                gridImage.Image = Properties.Resources.blue_box;

                pnlGame.Controls.Add(gridImage);
                grid[i] = new Grid(i, gridImage);

                // TEMP VIEW FOR LABELS
                var gridLabel = new Label();
                gridLabel.Size = new Size(GRID_LENGTH, GRID_LENGTH);
                gridLabel.Location = gridCoordinate;
                gridLabel.Visible = new System.Collections.Generic.HashSet<int>(enemyPath).Contains(i);
                gridLabel.Text = $"{i}";

                // gridLabel.BackColor = ((i + y) % 2) == 0 ? Color.Blue : Color.Red;
                gridLabel.AutoSize = false;

                // pnlGame.Controls.Add(gridLabel);
            }
        }

        private void PlaceTower(int towerId)
        {
            var tower = towerCosts[towerId];

            // validate user has enough money
            if (money < tower.Cost)
            {
                return;
            }

            // start place tower
            towerPlacing = towerId;
            tmrTowerPlacement.Start();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void btnHelp_Click(object sender, EventArgs e)
        {
            // pause game
            tmrGameTick.Enabled = false;
            Visible = false;

            var frmHelp = new FrmHelp(new FrmHelpConstructor { ReturnButtonText = "Game" });
            frmHelp.ShowDialog();

            // resume game
            Visible = true;
            tmrGameTick.Enabled = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void tmrGameTick_Tick(object sender, EventArgs e)
        {
            // create new enemy if necessary
            enemySpawnCount = (enemySpawnCount + 1) % 32;
            if (enemySpawnCount == 0)
            {
                var enemyGridLocation = GetMapCoordinateForId(enemyPath[0]);

                var enemyImage = new PictureBox();
                enemyImage.Size = new Size(Properties.Resources.teacher01.Width, Properties.Resources.teacher01.Height);
                enemyImage.Location = new Point(enemyGridLocation.X, enemyGridLocation.Y + (int)(0.5 * GRID_LENGTH));
                enemyImage.BackgroundImage = Properties.Resources.teacher01;
                enemyImage.Visible = true;
                enemyImage.BackgroundImageLayout = ImageLayout.None;

                Controls.Add(enemyImage);
                enemyImage.BringToFront();

                activeEnemies.Add(new Enemy(activeEnemies.Count, enemyImage, 0));
            }

            // move all enemies
            for (var i = 0; i < activeEnemies.Count; i += 1)
            {
                var oldEnemy = activeEnemies[i];

                var newEnemy = new Enemy(oldEnemy.Id, oldEnemy.EnemyImage, oldEnemy.EnemyDistance + 2);
                activeEnemies[i] = newEnemy;

                var newEnemyLocation = GetEnemyLocation(newEnemy.EnemyDistance);

                if (newEnemy.EnemyImage.Location.X != newEnemyLocation.X || newEnemy.EnemyImage.Location.Y != newEnemyLocation.Y)
                {
                    newEnemy.EnemyImage.Location = newEnemyLocation;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void tmrTowerPlacement_Tick(object sender, EventArgs e)
        {
            var x = MousePosition.X / GRID_LENGTH;
            var y = MousePosition.Y / GRID_LENGTH;
            var gridId = x + (y * (pnlGame.Width / GRID_LENGTH));

            // clear old placed
            foreach (var gridItem in grid)
            {
                // check we are not hiding the currently selected grid
                if (gridId < grid.Length && grid[gridId].Id == gridItem.Id)
                {
                    // check if we should display this grid
                    if (!gridItem.GridImage.Visible)
                    {
                        gridItem.GridImage.Visible = true;
                        gridItem.GridImage.BackgroundImage = towerCosts[towerPlacing ?? 0].TowerImage;

                        // make it red if we are placing on already placed tower:
                        // todo
                        if (gridItem.GridImage.Image != Properties.Resources.blue_box)
                        {
                            gridItem.GridImage.Image = Properties.Resources.blue_box;
                        }
                    }

                    continue;
                }

                // hide (we are no longer selecting this grid square)
                if (gridItem.GridImage.Visible)
                {
                    gridItem.GridImage.Visible = false;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower0_Click(object sender, EventArgs e)
        {
            PlaceTower(0);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower1_Click(object sender, EventArgs e)
        {
            PlaceTower(1);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower2_Click(object sender, EventArgs e)
        {
            PlaceTower(2);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower3_Click(object sender, EventArgs e)
        {
            PlaceTower(3);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower4_Click(object sender, EventArgs e)
        {
            PlaceTower(4);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower5_Click(object sender, EventArgs e)
        {
            PlaceTower(5);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower6_Click(object sender, EventArgs e)
        {
            PlaceTower(6);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbTower7_Click(object sender, EventArgs e)
        {
            PlaceTower(7);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            // handle tower placement
            if (!towerPlacing.HasValue)
            {
                return;
            }
        }
    }
}
