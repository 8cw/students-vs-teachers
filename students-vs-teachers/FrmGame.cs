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
        private readonly PathOrientation[] enemyPath =
        {
            new PathOrientation
            {
                Orientation = 0, TileIds = new int[] { 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030 },
            },
            new PathOrientation
            {
                Orientation = 270, TileIds = new int[] { 970, 910, 850, 790, 730, 670, 610, 550, 490 },
            },
            new PathOrientation
            {
                Orientation = 0, TileIds = new int[] { 491, 492, 493, 494, 495, 496, 497, 498, 499, 500, 501 },
            },
            new PathOrientation
            {
                Orientation = 90, TileIds = new int[] { 561, 621, 681, 741, 801, 861, 921, 981, 1041, 1101, 1161, 1221 },
            },
            new PathOrientation
            {
                Orientation = 0, TileIds = new int[] { 1222, 1223, 1224, 1225, 1226, 1227, 1228, 1229, 1230, 1231, 1232, 1233, 1234, 1235, 1236, 1237, 1238, },
            },
            new PathOrientation
            {
                Orientation = 270, TileIds = new int[] { 1178, 1118, 1058, 998, 938, 878 },
            },
            new PathOrientation
            {
                Orientation = 0, TileIds = new int[] { 879, 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890, 891, 892, 893, 894, 895, 896, 897, 898, 899 },
            },
        };

        /// <summary>
        /// The grids the user cannot place onto. These are for miscellaneous decorations.
        /// </summary>
        private readonly int[] blacklistedGridIds = { };
        private readonly Dictionary<int, TowerInfo> towerCosts = new Dictionary<int, TowerInfo>()
        {
            { 0, new TowerInfo { Name = "Year 9", Cost = 100, Damage = 1, AttackInterval = 3, TowerRange = 2, TowerImage = Properties.Resources.tower00 } },
            { 1, new TowerInfo { Name = "Year 10", Cost = 250, Damage = 2, AttackInterval = 2, TowerRange = 3, TowerImage = Properties.Resources.tower01 } },
            { 2, new TowerInfo { Name = "Year 11", Cost = 600, Damage = 6, AttackInterval = 6, TowerRange = 2, TowerImage = Properties.Resources.tower02 } },
            { 3, new TowerInfo { Name = "Year 13", Cost = 800, Damage = 8, AttackInterval = 5, TowerRange = 4, TowerImage = Properties.Resources.tower03 } },
            { 4, new TowerInfo { Name = "Year 12", Cost = 1000, Damage = 12, AttackInterval = 4, TowerRange = 5, TowerImage = Properties.Resources.tower04 } },
            { 5, new TowerInfo { Name = "Prefect", Cost = 2000, Damage = 18, AttackInterval = 3, TowerRange = 7, TowerImage = Properties.Resources.tower05 } },
            { 6, new TowerInfo { Name = "Head Boy", Cost = 2500, Damage = 20, AttackInterval = 2, TowerRange = 4, TowerImage = Properties.Resources.tower06 } },
            { 7, new TowerInfo { Name = "Top 6", Cost = 4000, Damage = 40, AttackInterval = 3, TowerRange = 5, TowerImage = Properties.Resources.tower07 } },
        };

        private readonly Dictionary<int, EnemyInfo> enemyInfo = new Dictionary<int, EnemyInfo>()
        {
            { 0, new EnemyInfo { EnemyType = 0, EnemyHealth = 100, EnemyReward = 5 } },
        };

        private List<Enemy> activeEnemies = new List<Enemy>();
        private int round = 0;
        private uint enemySpawnCount = 0;
        private int? towerPlacing = null;

        /// <summary>
        /// A List that contains all the towers that the user has placed down.
        /// </summary>
        private List<TowerPlaced> towersPlaced = new List<TowerPlaced>();
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

            // connect tower placements
            ConnectTowerEvents(0, pbTower0);
            ConnectTowerEvents(1, pbTower1);
            ConnectTowerEvents(2, pbTower2);
            ConnectTowerEvents(3, pbTower3);
            ConnectTowerEvents(4, pbTower4);
            ConnectTowerEvents(5, pbTower5);
            ConnectTowerEvents(6, pbTower6);
            ConnectTowerEvents(7, pbTower7);

            CreateGrid();
        }

        /// <summary>
        /// Initializes a tower icon to be clicked and placed.
        /// This method creates the ToolTip for the tower button, and connects mouse clicks up.
        /// </summary>
        /// <param name="towerId">The ID of the tower.</param>
        /// <param name="towerButton">The PictureBox associated with the tower to click to place it.</param>
        private void ConnectTowerEvents(int towerId, PictureBox towerButton)
        {
            var towerInfo = towerCosts[towerId];

            // create ToolTip
            var towerToolTip = new ToolTip();
            towerToolTip.ToolTipTitle = $"{towerInfo.Name} Tower";
            towerToolTip.SetToolTip(towerButton, $"Costs ${towerInfo.Cost}");
            towerToolTip.AutomaticDelay = 0;
            towerToolTip.InitialDelay = 0;
            towerToolTip.AutoPopDelay = 0;

            // connect to mouse events
            towerButton.MouseClick += new MouseEventHandler((_, __) =>
            {
                PlaceTower(towerId);
            });
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
        /// Retrieves the next path the enemy is going to travel on.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <returns>The path the enemy will travel on, or null if they are not travelling on a path.</returns>
        private PathOrientation? GetEnemyNextPath(Enemy enemy)
        {
            var tileNum = enemy.EnemyDistance / GRID_LENGTH;

            var tilesTravelled = 0;
            for (var i = 0; i < enemyPath.Length; i += 1)
            {
                var path = enemyPath[i];

                if (path.TileIds.Length > (tileNum - tilesTravelled))
                {
                    // somewhere in this list is the element we want
                    if (path.TileIds.Length > (tileNum - tilesTravelled + 1))
                    {
                        // case one: the next tile is in our current path
                        return path;
                    }
                    else if (enemyPath.Length > (i + 1))
                    {
                        // case two: the next tile is in our next path
                        return enemyPath[i + 1];
                    }
                }
                else
                {
                    // must be next path sequence.
                    tilesTravelled += path.TileIds.Length;
                }
            }

            // player is at the last tile.
            return null;
        }

        /// <summary>
        /// Retrieves the ID of the next tile an enemy is travelling to.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <returns>The ID of the tile the enemy is travelling to, or null if they have completed the game.</returns>
        private int? GetEnemyNextTileId(Enemy enemy)
        {
            var tileNum = enemy.EnemyDistance / GRID_LENGTH;

            var tilesTravelled = 0;
            for (var i = 0; i < enemyPath.Length; i += 1)
            {
                var path = enemyPath[i];

                if (path.TileIds.Length > (tileNum - tilesTravelled))
                {
                    // somewhere in this list is the element we want
                    if (path.TileIds.Length > (tileNum - tilesTravelled + 1))
                    {
                        // case one: the next tile is in our current path
                        return path.TileIds[tileNum - tilesTravelled + 1];
                    }
                    else if (enemyPath.Length > (i + 1))
                    {
                        // case two: the next tile is in our next path
                        return enemyPath[i + 1].TileIds[0];
                    }
                }
                else
                {
                    // must be next path sequence.
                    tilesTravelled += path.TileIds.Length;
                }
            }

            // player is at the last tile.
            return null;
        }

        /// <summary>
        /// Retrieves the location an enemy should be given the distance they have traveled.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <returns>A point representing where the enemy should be.</returns>
        private Point? GetEnemyLocation(Enemy enemy)
        {
            var tileNum = enemy.EnemyDistance / GRID_LENGTH;
            double tilePercentage = ((double)enemy.EnemyDistance / GRID_LENGTH) - tileNum;

            var tilesTravelled = 0;
            for (var i = 0; i < enemyPath.Length; i += 1)
            {
                var path = enemyPath[i];

                if (path.TileIds.Length > (tileNum - tilesTravelled))
                {
                    // somewhere in this list is the element we want
                    var currentTile = GetMapCoordinateForId(path.TileIds[tileNum - tilesTravelled]);

                    // grab the next element, so that we can interpolate.
                    var nextTileTest = GetEnemyNextTileId(enemy);

                    if (nextTileTest.HasValue)
                    {
                        // we are not at the last tile
                        var nextTile = GetMapCoordinateForId(nextTileTest.Value);
                        return new Point(
                            (int)(currentTile.X + ((nextTile.X - currentTile.X) * tilePercentage)),
                            (int)(currentTile.Y + ((nextTile.Y - currentTile.Y) * tilePercentage)));
                    }
                    else
                    {
                        // we are at the las tile
                        return currentTile;
                    }
                }
                else
                {
                    // must be next path sequence.
                    tilesTravelled += path.TileIds.Length;
                }
            }

            // player is out of bounds.
            return null;
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
                gridImage.MouseClick += new MouseEventHandler(pnlGame_MouseClick);

                pnlGame.Controls.Add(gridImage);
                gridImage.BringToFront();
                grid[i] = new Grid(i, gridImage);

                // TEMP VIEW FOR LABELS
                var gridLabel = new Label();
                gridLabel.Size = new Size(GRID_LENGTH, GRID_LENGTH);
                gridLabel.Location = gridCoordinate;
                gridLabel.Visible = enemyPath.Select((path) => path.TileIds.Contains(i)) != null;
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
            pbCancelPlacement.Visible = true;

            // display tower range
            pbTowerRange.Size = new Size(
                GRID_LENGTH * ((tower.TowerRange * 2) + 1),
                GRID_LENGTH * ((tower.TowerRange * 2) + 1));
            pbTowerRange.Visible = true;
        }

        private void EnemyOutOfBounds(Enemy enemy)
        {
            enemy.EnemyImage.Dispose();
        }

        /// <summary>
        /// Handles the death of an Enemy.
        /// </summary>
        /// <param name="enemyType">The type ID of the enemy.</param>
        private void EnemyDeath(int enemyType)
        {
            AddMoney(enemyInfo[enemyType].EnemyReward);
        }

        /// <summary>
        /// Returns the amount of max health an enemy has.
        /// </summary>
        /// <param name="enemyType">The ID of the enemy.</param>
        /// <returns>The amount of HP the enemy has at a maximum.</returns>
        private int GetEnemyMaxHealth(int enemyType)
        {
            return enemyInfo[enemyType].EnemyHealth;
        }

        /// <summary>
        /// Attacks an enemy, returning the new Enemy.
        /// </summary>
        /// <param name="enemy">The enemy to attack.</param>
        /// <param name="attackTowerId">The ID of the tower attacking.</param>
        /// <returns>The new enemy to save, or null if the enemy should die.</returns>
        private Enemy? AttackEnemy(Enemy enemy, int attackTowerId)
        {
            var towerDamage = towerCosts[attackTowerId].Damage;

            // check if we need to decrease enemy type by one
            if (enemy.Health - towerDamage <= 0)
            {
                EnemyDeath(enemy.EnemyType);

                if (enemy.EnemyType == 0)
                {
                    return null;
                }

                return new Enemy(enemy.Id, enemy.EnemyType - 1, enemy.EnemyImage, enemy.EnemyDistance, GetEnemyMaxHealth(enemy.EnemyType - 1));
            }

            // return enemy, with more damage
            return new Enemy(enemy.Id, enemy.EnemyType, enemy.EnemyImage, enemy.EnemyDistance, enemy.Health - towerDamage);
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
                var enemyGridLocation = GetMapCoordinateForId(enemyPath[0].TileIds[0]);

                var enemyImage = new PictureBox();
                var newEnemy = new Enemy(activeEnemies.Count, 0, enemyImage, 0, GetEnemyMaxHealth(0));
                activeEnemies.Add(newEnemy);

                enemyImage.Size = new Size(Properties.Resources.teacher0_right.Width, Properties.Resources.teacher0_right.Height);
                enemyImage.Location = new Point(enemyGridLocation.X, enemyGridLocation.Y + (int)(0.5 * GRID_LENGTH));
                enemyImage.BackgroundImage = ImageRotation.GetEnemyImage(newEnemy, enemyPath[0].Orientation);
                enemyImage.Visible = true;
                enemyImage.BackgroundImageLayout = ImageLayout.None;

                Controls.Add(enemyImage);
                enemyImage.BringToFront();
            }

            // move all enemies
            for (var i = 0; i < activeEnemies.Count; i += 1)
            {
                var oldEnemy = activeEnemies[i];

                var newEnemy = new Enemy(oldEnemy.Id, oldEnemy.EnemyType, oldEnemy.EnemyImage, oldEnemy.EnemyDistance + 4, oldEnemy.Health);
                activeEnemies[i] = newEnemy;

                var newEnemyLocationTest = GetEnemyLocation(newEnemy);

                if (!newEnemyLocationTest.HasValue)
                {
                    // enemy has gone out of boundaries
                    EnemyOutOfBounds(oldEnemy);

                    var n = activeEnemies.Count - 1;
                    activeEnemies[i] = activeEnemies[n];
                    activeEnemies.RemoveAt(n);

                    // i += 1;
                    continue;
                }

                // assert that value exists
                var newEnemyLocation = newEnemyLocationTest.Value;

                // move enemy if necessary
                if (newEnemy.EnemyImage.Location.X != newEnemyLocation.X || newEnemy.EnemyImage.Location.Y != newEnemyLocation.Y)
                {
                    newEnemy.EnemyImage.Location = newEnemyLocation;
                }

                // rotate enemy if necessary
                var enemyImage = ImageRotation.GetEnemyImage(newEnemy, GetEnemyNextPath(newEnemy)?.Orientation ?? 0);
                if (newEnemy.EnemyImage.BackgroundImage != enemyImage)
                {
                    newEnemy.EnemyImage.BackgroundImage = enemyImage;
                }
            }

            // attack enemies
            for (var i = 0; i < towersPlaced.Count; i += 1)
            {
                var tower = towersPlaced[i];
                if (tower.AttackDebounce == towerCosts[tower.TowerId].AttackInterval)
                {
                    var towerLocation = GetMapCoordinateForId(tower.GridId);

                    // attack!
                    int? closestEnemyIndex = null;
                    var closestEnemyDistance = int.MaxValue;
                    for (var j = 0; j < activeEnemies.Count; j += 1)
                    {
                        var enemyCheck = activeEnemies[j];
                        var distance = (int)Math.Pow(enemyCheck.EnemyImage.Location.X - towerLocation.X, 2) + (int)Math.Pow(enemyCheck.EnemyImage.Location.Y - towerLocation.Y, 2);
                        if (distance < closestEnemyDistance)
                        {
                            closestEnemyDistance = distance;
                            closestEnemyIndex = j;
                        }
                    }

                    // check there is an enemy that exists
                    if (!closestEnemyIndex.HasValue)
                    {
                        continue;
                    }

                    var closestEnemy = activeEnemies[closestEnemyIndex ?? 0];

                    // convert grid attack range to square units
                    var towerAttackRange = (int)Math.Pow(towerCosts[tower.TowerId].TowerRange * GRID_LENGTH, 2);

                    // validate they are in range
                    if (closestEnemyDistance > towerAttackRange)
                    {
                        continue;
                    }

                    // attack enemy!
                    var newEnemyCheck = AttackEnemy(closestEnemy, tower.TowerId);
                    if (!newEnemyCheck.HasValue)
                    {
                        // we killed the enemy
                        EnemyOutOfBounds(closestEnemy);

                        var n = activeEnemies.Count - 1;
                        activeEnemies[closestEnemyIndex ?? 0] = activeEnemies[n];
                        activeEnemies.RemoveAt(n);
                    }
                    else
                    {
                        // we damaged the enemy
                        activeEnemies[closestEnemyIndex ?? 0] = newEnemyCheck.Value;
                    }

                    towersPlaced[i] = new TowerPlaced
                    {
                        GridId = tower.GridId,
                        AttackDebounce = 0,
                        TowerId = tower.TowerId,
                    };
                }
                else
                {
                    towersPlaced[i] = new TowerPlaced
                    {
                        AttackDebounce = tower.AttackDebounce + 1,
                        GridId = tower.GridId,
                        TowerId = tower.TowerId,
                    };
                }
            }
        }

        /// <summary>
        /// Retrieves the current grid the user is hovering over.
        /// </summary>
        /// <returns>The grid Id the user is hovering over.</returns>
        private int GetCurrentHoveredGrid()
        {
            var x = MousePosition.X / GRID_LENGTH;
            var y = MousePosition.Y / GRID_LENGTH;
            return x + (y * (pnlGame.Width / GRID_LENGTH));
        }

        /// <summary>
        /// Adds some money from a user and updates the money label.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        private void AddMoney(int amount)
        {
            money += amount;
            lblMoney.Text = $"Money: ${money}";
        }

        /// <summary>
        /// Removes some money from a user and updates the money label.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        private void SubtractMoney(int amount)
        {
            AddMoney(-amount);
        }

        /// <summary>
        /// Cancels tower placement.
        /// </summary>
        private void CancelPlacemet()
        {
            towerPlacing = null;
            tmrTowerPlacement.Stop();
            pbCancelPlacement.Visible = false;
            pbTowerRange.Visible = false;

            // hide the grid hovered over if it exists
            var gridId = GetCurrentHoveredGrid();
            if (gridId < 0 || gridId > grid.Length)
            {
                return;
            }

            var gridSquare = grid[gridId];
            if (gridSquare.GridImage.Visible)
            {
                gridSquare.GridImage.Visible = false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void tmrTowerPlacement_Tick(object sender, EventArgs e)
        {
            var gridId = GetCurrentHoveredGrid();

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
                    }
                    else
                    {
                        // make it red if we are placing on already placed tower:
                        if (enemyPath.Any((path) => path.TileIds.Contains(gridId)) || blacklistedGridIds.Contains(gridId) || towersPlaced.Any((tower) => tower.GridId == gridId))
                        {
                            if (pbTowerRange.BackgroundImage != Properties.Resources.placement_circle_illegal)
                            {
                                pbTowerRange.BackgroundImage = Properties.Resources.placement_circle_illegal;
                            }
                        }
                        else
                        {
                            if (pbTowerRange.BackgroundImage != Properties.Resources.placement_circle)
                            {
                                pbTowerRange.BackgroundImage = Properties.Resources.placement_circle;
                            }
                        }
                    }

                    continue;
                }

                // hide (we are no longer selecting this grid square)
                if (gridItem.GridImage.Visible && !towersPlaced.Any((tower) => tower.GridId == gridItem.Id))
                {
                    gridItem.GridImage.Visible = false;
                }
            }

            // update range PictureBox
            pbTowerRange.Location = new Point(
                ((MousePosition.X / GRID_LENGTH) * GRID_LENGTH) - (pbTowerRange.Size.Width / 2) + (GRID_LENGTH / 2),
                ((MousePosition.Y / GRID_LENGTH) * GRID_LENGTH) - (pbTowerRange.Size.Height / 2) + (GRID_LENGTH / 2));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pnlGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CancelPlacemet();
                return;
            }

            // handle tower placement
            if (!towerPlacing.HasValue)
            {
                return;
            }

            // check that we are not hovering over a grid already have an item
            // check that we are not placing on an invalid grid
            var gridId = GetCurrentHoveredGrid();
            if (enemyPath.Any((path) => path.TileIds.Contains(gridId)) || blacklistedGridIds.Contains(gridId) || towersPlaced.Any((tower) => tower.GridId == gridId))
            {
                return;
            }

            // place tower
            var towerInfo = towerCosts[towerPlacing ?? 0];

            grid[gridId].GridImage.BackgroundImage = towerInfo.TowerImage;
            grid[gridId].GridImage.Visible = true;
            towersPlaced.Add(new TowerPlaced
            {
                AttackDebounce = 0,
                GridId = gridId,
                TowerId = towerPlacing ?? 0,
            });
            SoundPlayer.TowerPlacement.Play();

            // subtract money
            SubtractMoney(towerInfo.Cost);

            // set placing tower to null
            towerPlacing = null;
            tmrTowerPlacement.Stop();
            pbCancelPlacement.Visible = false;
            pbTowerRange.Visible = false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Goes against form naming convention.")]
        private void pbCancelPlacement_Click(object sender, EventArgs e)
        {
            CancelPlacemet();
        }
    }
}
