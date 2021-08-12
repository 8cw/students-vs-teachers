// <summary>
// A structure to define a possible group of enemies (a wave).
// </summary>
// <copyright file="EnemyWave.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Drawing;

/// <summary>
/// A struct to define a possible enemy wave.
/// </summary>
public struct EnemyWave
{
    /// <summary>
    /// The amount of game ticks to spread the enemies between.
    /// </summary>
    public int Spread;

    /// <summary>
    /// The ID of the enemies that are attacking.
    /// </summary>
    public int[] Enemies;
}
