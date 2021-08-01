﻿// <summary>
// A struct to define a possible enemy.
// </summary>
// <copyright file="Enemy.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Windows.Forms;

/// <summary>
/// A grid in the game.
/// </summary>
internal struct Enemy
{
    /// <summary>
    /// The grid ID.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// The image associated with this grid.
    /// </summary>
    public readonly PictureBox EnemyImage;

    /// <summary>
    /// The amount of distance the enemy has travelled.
    /// </summary>
    public readonly int EnemyDistance;

    /// <summary>
    /// Initializes a new instance of the <see cref="Enemy"/> struct.
    /// </summary>
    /// <param name="id">The id of the enemy.</param>
    /// <param name="enemyImage"> The image of the enemy.</param>
    /// <param name="enemyDistance"> How far the enemy has travelled since the start.</param>
    public Enemy(int id, PictureBox enemyImage, int enemyDistance)
    {
        Id = id;
        EnemyImage = enemyImage;

        EnemyDistance = enemyDistance;
    }

    /// <summary>
    /// Returns a printable version of a Enemy.
    /// </summary>
    /// <returns>The readable version of a Enemy.</returns>
    public override string ToString() => $"Enemy[#{Id}@{EnemyImage.Location}]";
}