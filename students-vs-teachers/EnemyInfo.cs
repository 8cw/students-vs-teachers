// <summary>
// A structure to define enemy metadata.
// </summary>
// <copyright file="EnemyInfo.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

/// <summary>
/// A struct to define enemy metadata.
/// </summary>
public struct EnemyInfo
{
    /// <summary>
    /// The ID of the enemy type.
    /// </summary>
    public int EnemyType;

    /// <summary>
    /// The amount of health an Enemy can have.
    /// </summary>
    public int EnemyHealth;

    /// <summary>
    /// The amount of money to reward after killing this Enemy.
    /// </summary>
    public int EnemyReward;
}
