// <summary>
// Handles game sound effects.
// </summary>
// <copyright file="SoundPlayer.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;

    /// <summary>
    /// Handles game sounds.
    /// </summary>
    internal class SoundPlayer
    {
        /// <summary>
        /// A "swoosh" sound effect for when tower item is placed.
        /// </summary>
        public static readonly Media TowerPlacement = new Media($@"{AppDomain.CurrentDomain.BaseDirectory}\Resources\tower_placement.wav");

        /// <summary>
        /// A "ding" sound effect for when a tower attacks a teacher.
        /// </summary>
        public static readonly Media TowerAttack = new Media($@"{AppDomain.CurrentDomain.BaseDirectory}\Resources\tower_attack.wav");

        /// <summary>
        /// The background song while the game is played.
        /// </summary>
        public static readonly Media BackgroundMusic = new Media($@"{AppDomain.CurrentDomain.BaseDirectory}\Resources\background_music.wav");
    }
}
