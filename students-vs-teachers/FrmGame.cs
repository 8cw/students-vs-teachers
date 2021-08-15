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

        /// <summary>
        /// The amount of game ticks between each round.
        /// </summary>
        private const int ROUND_DELAY = 240;

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
        private readonly int[] blacklistedGridIds =
        {
            121, 122, 123, 124, 125,
            181, 182, 183, 184, 185,
            241, 242, 243, 244, 245,
            301, 302, 303, 304, 305,

            7, 8, 9, 10,
            66, 67, 68, 69, 70,
            126, 127, 128, 129, 130, 131,
            186, 187, 188, 189, 190, 191,
            246, 247, 248, 249, 250, 251,
            307, 308, 309,
            368,

            132, 133, 134, 135, 136,
            192, 193, 194, 195, 196,
            252, 253, 254, 255, 256,
            312, 313, 314, 315,

            89, 90, 91, 92,
            148, 149, 150, 151, 152,
            208, 209, 210, 211, 212,
            268, 269, 270, 271, 272,
            329, 330, 331, 332,

            94, 95, 96, 97,
            153, 154, 155, 156, 157,
            213, 214, 215, 216, 217,
            273, 274, 275, 276, 277,
            334, 335, 336, 337,

            99, 100, 101,
            158, 159, 160, 161,
            218, 219, 220, 221,
            279, 230,

            44, 45, 46,
            103, 104, 105, 106, 107,
            162, 163, 164, 165, 166, 167, 168,
            222, 223, 224, 225, 226, 227, 228,
            282, 283, 284, 285, 286, 287, 288, 289,
            342, 343, 344, 345, 346, 347, 348, 349,
            402, 403, 404, 405, 406, 407, 408, 409,
            462, 463, 464, 465, 466, 467, 468,
            523, 524, 525, 526, 527,

            111, 112, 113, 114,
            170, 171, 172, 173, 174,
            230, 231, 232, 233, 234,
            290, 291, 292, 293, 294,
            351, 352, 353, 354,

            176, 177, 178, 179,
            235, 236, 237, 238, 239,
            295, 296, 297, 298, 299,
            355, 356, 357, 358, 359,

            // ROW 2
            423, 424, 425, 426,
            482, 483, 484, 485, 486,
            542, 543, 544, 545, 546,
            603, 604, 605, 606,

            // BELOW MAP
            660, 661, 662, 663,
            720, 721, 722, 723,
            781, 782,

            // ROW #4
            1149, 1150, 1151,
            1208, 1209, 1210, 1211, 1212,
            1268, 1269, 1270, 1271, 1272,
            1328, 1329, 1330, 1331, 1332,
            1338, 1389, 1390, 1391, 1392,

            1213, 1214, 1215, 1216,
            1273, 1274, 1275, 1276,
            1333, 1334, 1335, 1336,

            1292, 1293, 1294, 1295,
            1352, 1353, 1354, 1355, 1356,
            1412, 1413, 1414, 1415, 1416,
            1472, 1473, 1474, 1475, 1476,

            1190, 1191, 1192,
            1249, 1250, 1251, 1252, 1253,
            1309, 1310, 1311, 1312, 1313,
            1369, 1370, 1371, 1372, 1373,
            1429, 1430, 1431, 1432,

            // ROW #5
            1625, 1626, 1627, 1628,
            1684, 1685, 1686, 1687, 1688, 1689,
            1744, 1745, 1746, 1747, 1748,
            1805, 1806, 1807, 1808,

            1392, 1393, 1394,
            1451, 1452, 1453, 1454, 1455,
            1511, 1512, 1513, 1514, 1515,
            1569, 1570, 1571, 1572, 1573, 1574, 1575, 1576,
            1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636,
            1689, 1690, 1691, 1692, 1693, 1694, 1695, 1696, 1697,
            1749, 1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757,
            1810, 1811, 1812, 1813, 1814, 1815, 1816,
            1870, 1871, 1872, 1873, 1874, 1875,

            1337, 1338, 1339, 1340,
            1397, 1398, 1399, 1400,
            1457, 1458, 1459, 1460, 1461,
            1517, 1518, 1519, 1520,

            1579, 1580, 1581, 1582,
            1638, 1639, 1640, 1641, 1642,
            1671, 1689, 1699, 1700, 1701, 1702, 1703,
            1758, 1759, 1760, 1761, 1762,

            1585, 1586, 1587, 1588,
            1644, 1645, 1646, 1647, 1648,
            1704, 1705, 1706, 1707, 1708,
            1765, 1766, 1767, 1768,
            1825, 1826, 1827, 1828,

            1488, 1489, 1490, 1491, 1492,
            1547, 1548, 1549, 1550, 1551, 1552, 1553,
            1608, 1609, 1610, 1611,

            1375, 1376, 1377,
            1435, 1436, 1437,
            1495, 1496, 1497,

            1613, 1614, 1615, 1616, 1617,
            1673, 1674, 1675, 1676, 1677,
            1733, 1734, 1735, 1736, 1737,
            1793, 1794, 1795, 1796,
        };

        private readonly Dictionary<int, TowerInfo> towerCosts = new Dictionary<int, TowerInfo>()
        {
            { 0, new TowerInfo { Name = "Year 9", Cost = 100, Damage = 50, AttackInterval = 15, TowerRange = 2, TowerImage = Properties.Resources.tower00 } },
            { 1, new TowerInfo { Name = "Year 10", Cost = 175, Damage = 75, AttackInterval = 15, TowerRange = 3, TowerImage = Properties.Resources.tower01 } },
            { 2, new TowerInfo { Name = "Year 11", Cost = 225, Damage = 75, AttackInterval = 8, TowerRange = 3, TowerImage = Properties.Resources.tower02 } },
            { 3, new TowerInfo { Name = "Year 13", Cost = 400, Damage = 150, AttackInterval = 4, TowerRange = 4, TowerImage = Properties.Resources.tower03 } },
            { 4, new TowerInfo { Name = "Year 12", Cost = 600, Damage = 500, AttackInterval = 30, TowerRange = 12, TowerImage = Properties.Resources.tower04 } },
            { 5, new TowerInfo { Name = "Prefect", Cost = 600, Damage = 400, AttackInterval = 4, TowerRange = 3, TowerImage = Properties.Resources.tower05 } },
            { 6, new TowerInfo { Name = "Head Boy", Cost = 900, Damage = 1000, AttackInterval = 10, TowerRange = 7, TowerImage = Properties.Resources.tower06 } },
            { 7, new TowerInfo { Name = "Top 6", Cost = 5000, Damage = 1000000, AttackInterval = 2, TowerRange = 24, TowerImage = Properties.Resources.tower07 } },
        };

        private readonly Dictionary<int, EnemyInfo> enemyInfo = new Dictionary<int, EnemyInfo>()
        {
            { 0, new EnemyInfo { EnemyType = 0, EnemyHealth = 50, EnemyReward = 3, EnemyLives = 2, } },
            { 1, new EnemyInfo { EnemyType = 1, EnemyHealth = 100, EnemyReward = 6, EnemyLives = 5, } },
            { 2, new EnemyInfo { EnemyType = 2, EnemyHealth = 200, EnemyReward = 14, EnemyLives = 12, } },
            { 3, new EnemyInfo { EnemyType = 3, EnemyHealth = 600, EnemyReward = 26, EnemyLives = 30, } },
            { 4, new EnemyInfo { EnemyType = 4, EnemyHealth = 2400, EnemyReward = 50, EnemyLives = 60, } },
            { 5, new EnemyInfo { EnemyType = 5, EnemyHealth = 8000, EnemyReward = 75, EnemyLives = 65, } },
            { 6, new EnemyInfo { EnemyType = 6, EnemyHealth = 100000000, EnemyReward = 0, EnemyLives = 150, } },
        };

        private readonly EnemyWave[][] enemyRounds = new EnemyWave[][]
        {
            new EnemyWave[]
            {
                new EnemyWave { Spread = 10, Enemies = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, },
                new EnemyWave { Spread = 30, Enemies = new int[] { 1, 1 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 10, Enemies = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, },
                new EnemyWave { Spread = 20, Enemies = new int[] { 1, 0, 1, 0, 1, }, },
                new EnemyWave { Spread = 10, Enemies = new int[] { 1, 1, 1, 1, 1, 1 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 10, Enemies = new int[] { 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, }, },
                new EnemyWave { Spread = 6, Enemies = new int[] { 1, 1, 1, 1, 1, }, },
                new EnemyWave { Spread = 14, Enemies = new int[] { 0, 1, 2, 0, 1, 2 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 10, Enemies = new int[] { 2, 2, 2, 1, 1, 2, 2, 2 }, },
                new EnemyWave { Spread = 18, Enemies = new int[] { 3, 3, }, },
                new EnemyWave { Spread = 6, Enemies = new int[] { 2, 2, 2, }, },
                new EnemyWave { Spread = 18, Enemies = new int[] { 3, 3, }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 10, Enemies = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, },
                new EnemyWave { Spread = 5, Enemies = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, },
                new EnemyWave { Spread = 2, Enemies = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 30, Enemies = new int[] { 4, 4, 4, 4, 4 }, },
                new EnemyWave { Spread = 20, Enemies = new int[] { 4, 3, 4, 3, 4 }, },
                new EnemyWave { Spread = 4, Enemies = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, },
                new EnemyWave { Spread = 12, Enemies = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 4, Enemies = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, },
                new EnemyWave { Spread = 18, Enemies = new int[] { 5, 5, 5, 5, 5, 5 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 12, Enemies = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, },
                new EnemyWave { Spread = 8, Enemies = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, },
            },
            new EnemyWave[]
            {
                new EnemyWave { Spread = 60, Enemies = new int[] { 1 }, },
                new EnemyWave { Spread = 1, Enemies = new int[] { 6 }, },
            },
        };

        private readonly string[] educationalMessages = new string[]
        {
            "Students from the Dominican Republic report the greatest happiness lives in the world.",
            "Teachers from Switzerland have an average salary of $156,000 NZD per year.",
            "If you work 40 hours a week up until 65 years, you will work just over 90,000 hours in your lifetime.",
            "The University of Cambridge charges $470,000 NZD out of students for a Doctor of Business degree.",
            "Teachers report having real lives outside of school.",
            "Being a Dental Hygienist is ranked as the worlds happiest job.",
            "Playing scary and violent video games help children master their fears in real life according to studies.",
            "Google's base salary for a software engineer is at $272,000 NZD.",
            "Video games can lessen disruptive behaviors and enhance positive development in ADHD children",
        };

        private List<Enemy> activeEnemies = new List<Enemy>();
        private uint gameTicks = 0;

        private int? towerPlacing = null;

        /// <summary>
        /// A List that contains all the towers that the user has placed down.
        /// </summary>
        private List<TowerPlaced> towersPlaced = new List<TowerPlaced>();
        private int money = 225;
        private int lives = 200;

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
            tmrGameTick.Tick += new EventHandler(tmrGameTick_Tick);

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

            FontLoader.LoadFont(lblInfo, 12.0F);

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

            lblMoney.Text = $"Money: ${money}";
            lblRound.Text = $"Round: 0/{enemyRounds.Length}";
            lblLives.Text = $"Lives: {lives}";

            // play background music
            SoundPlayer.BackgroundMusic.Play(this);
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
                // var gridLabel = new Label();
                // gridLabel.Size = new Size(GRID_LENGTH, GRID_LENGTH);
                // gridLabel.Location = gridCoordinate;
                // gridLabel.Visible = enemyPath.Any((path) => path.TileIds.Contains(i) || blacklistedGridIds.Contains(i)); // enemyPath.Any((path) => path.TileIds.Contains(i));
                // gridLabel.Text = $"{i}";

                // gridLabel.BackColor = ((i + GetGridCoordinatesForId(i).Y) % 2) == 0 ? Color.Blue : Color.Red;
                // gridLabel.AutoSize = false;

                // pnlGame.Controls.Add(gridLabel);
                // gridLabel.BringToFront();
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
            gameTicks += 1;

            // create new enemy if necessary
            var enemySpawnTicks = 0;
            var roundNum = 0;
            foreach (var round in enemyRounds)
            {
                roundNum += 1;
                enemySpawnTicks += ROUND_DELAY;

                foreach (var wave in round)
                {
                    foreach (var enemyType in wave.Enemies)
                    {
                        enemySpawnTicks += wave.Spread;
                        if (enemySpawnTicks == gameTicks)
                        {
                            // spawn enemy!
                            var enemyGridLocation = GetMapCoordinateForId(enemyPath[0].TileIds[0]);

                            var enemyImage = new PictureBox();
                            var newEnemy = new Enemy(activeEnemies.Count, enemyType, enemyImage, 0, GetEnemyMaxHealth(0));
                            activeEnemies.Add(newEnemy);

                            enemyImage.Size = new Size(Properties.Resources.teacher0_right.Width, Properties.Resources.teacher0_right.Height);
                            enemyImage.Location = new Point(enemyGridLocation.X, enemyGridLocation.Y + (int)(0.5 * GRID_LENGTH));
                            enemyImage.BackgroundImage = ImageRotation.GetEnemyImage(newEnemy, enemyPath[0].Orientation);
                            enemyImage.Visible = true;
                            enemyImage.BackgroundImageLayout = ImageLayout.None;

                            Controls.Add(enemyImage);
                            enemyImage.BringToFront();

                            lblRound.Text = $"Round: {roundNum}/{enemyRounds.Length}";
                            lblInfo.Text = educationalMessages[roundNum - 1];
                        }
                    }
                }
            }

            if (gameTicks > enemySpawnTicks && activeEnemies.Count == 0)
            {
                Visible = false;

                // player defeated all enemies
                tmrGameTick.Stop();
                towerPlacing = null;
                tmrTowerPlacement.Stop();
                pbCancelPlacement.Visible = false;
                pbTowerRange.Visible = false;

                // display win screen
                var frmWin = new FrmGameWin();
                frmWin.ShowDialog();

                Dispose();
                Close();
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

                    SubtractLives(enemyInfo[newEnemy.EnemyType].EnemyLives);

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

                    SoundPlayer.TowerAttack.Play(this);

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
        /// Removes some lives from a player.
        /// </summary>
        /// <param name="amount">The amount of lives to subtract.</param>
        private void SubtractLives(int amount)
        {
            // clamp amount between lives-amount
            lives -= amount > lives ? lives : amount;
            lblLives.Text = $"Lives: {lives}";

            // todo: handle no move lives
            if (lives == 0)
            {
                tmrGameTick.Stop();
                towerPlacing = null;
                tmrTowerPlacement.Stop();
                pbCancelPlacement.Visible = false;
                pbTowerRange.Visible = false;

                // display death screen
                var frmLose = new FrmGameLose();
                Dispose();
                frmLose.ShowDialog();
            }
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
            SoundPlayer.TowerPlacement.Play(this);

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
