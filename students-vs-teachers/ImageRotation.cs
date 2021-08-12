﻿// <summary>
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
