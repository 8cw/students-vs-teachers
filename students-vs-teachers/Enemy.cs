// <summary>
// A struct to define a possible enemy.
// </summary>
// <copyright file="Enemy.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Windows.Forms;

/// <summary>
/// An enemy in the game.
/// </summary>
internal struct Enemy
{
    /// <summary>
    /// The enemy Id.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// The Id of the enemy type.
    /// </summary>
    public readonly int EnemyType;

    /// <summary>
    /// The image associated with this grid.
    /// </summary>
    public readonly PictureBox EnemyImage;

    /// <summary>
    /// The amount of distance the enemy has travelled.
    /// </summary>
    public readonly int EnemyDistance;

    /// <summary>
    /// The amount of health the enemy has.
    /// </summary>
    public readonly int Health;

    /// <summary>
    /// Initializes a new instance of the <see cref="Enemy"/> struct.
    /// </summary>
    /// <param name="id">The id of the enemy.</param>
    /// <param name="enemyType">The type of enemy.</param>
    /// <param name="enemyImage">The image of the enemy.</param>
    /// <param name="enemyDistance">How far the enemy has travelled since the start.</param>
    /// <param name="health">The amount of health the enemy has.</param>
    public Enemy(int id, int enemyType, PictureBox enemyImage, int enemyDistance, int health)
    {
        Id = id;
        EnemyType = enemyType;
        EnemyImage = enemyImage;
        EnemyDistance = enemyDistance;
        Health = health;
    }

    /// <summary>
    /// Returns a printable version of a Enemy.
    /// </summary>
    /// <returns>The readable version of a Enemy.</returns>
    public override string ToString() => $"Enemy[Type={EnemyType}#{Id}@{EnemyImage.Location}]";
}