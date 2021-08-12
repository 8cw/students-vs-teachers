// <summary>
// Handles game sound effects.
// </summary>
// <copyright file="SoundPlayer.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    /// <summary>
    /// Handles game sounds.
    /// </summary>
    internal class SoundPlayer
    {
        /// <summary>
        /// A "swoosh" sound effect for when tower item is placed.
        /// </summary>
        public static readonly System.Media.SoundPlayer TowerPlacement = new System.Media.SoundPlayer(Properties.Resources.tower_placement);

        /// <summary>
        /// A "ding" sound effect for when a tower attacks a teacher.
        /// </summary>
        public static readonly System.Media.SoundPlayer TowerAttack = new System.Media.SoundPlayer(Properties.Resources.tower_attack);
    }
}
