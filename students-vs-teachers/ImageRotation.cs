// <summary>
// Contains the ImageRotation class.
// </summary>
// <copyright file="ImageRotation.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A class that contains helpful messages for retrieving the relevant image for an enemy given a rotation.
    /// </summary>
    internal class ImageRotation
    {
        private static readonly Dictionary<int, Bitmap[]> EnemyImages = new Dictionary<int, Bitmap[]>()
        {
            { 0, new Bitmap[] { Properties.Resources.teacher0_right, Properties.Resources.teacher0_down, Properties.Resources.teacher0_left, Properties.Resources.teacher0_up } },
            { 1, new Bitmap[] { Properties.Resources.teacher1_right, Properties.Resources.teacher1_down, Properties.Resources.teacher1_left, Properties.Resources.teacher1_up } },
            { 2, new Bitmap[] { Properties.Resources.teacher2_right, Properties.Resources.teacher2_down, Properties.Resources.teacher2_left, Properties.Resources.teacher2_up } },
            { 3, new Bitmap[] { Properties.Resources.teacher3_right, Properties.Resources.teacher3_down, Properties.Resources.teacher3_left, Properties.Resources.teacher3_up } },
            { 4, new Bitmap[] { Properties.Resources.teacher4_right, Properties.Resources.teacher4_down, Properties.Resources.teacher4_left, Properties.Resources.teacher4_up } },
            { 5, new Bitmap[] { Properties.Resources.teacher5_right, Properties.Resources.teacher5_down, Properties.Resources.teacher5_left, Properties.Resources.teacher5_up } },
            { 6, new Bitmap[] { Properties.Resources.teacher6_right, Properties.Resources.teacher6_down, Properties.Resources.teacher6_left, Properties.Resources.teacher6_up } },
        };

        /// <summary>
        /// Retrieves the enemy image for a given enemy and rotation.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <param name="rotation">The clockwise rotation, in degree from origin where 0 degrees = facing right.</param>
        /// <returns>The relevant enemy image.</returns>
        public static Bitmap GetEnemyImage(Enemy enemy, int rotation)
        {
            return EnemyImages[enemy.EnemyType][rotation / 90];
        }
    }
}
