// <summary>
// A structure to define a possible tower.
// </summary>
// <copyright file="TowerInfo.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Drawing;

/// <summary>
/// A struct to define the possible help pages.
/// </summary>
public struct TowerInfo
{
    /// <summary>
    /// The name of the tower.
    /// </summary>
    public string Name;

    /// <summary>
    /// The cost of the tower, in money.
    /// </summary>
    public int Cost;

    /// <summary>
    /// The amount of damage the tower does.
    /// </summary>
    public int Damage;

    /// <summary>
    /// The interval between attacks in seconds.
    /// </summary>
    public float AttackInterval;

    /// <summary>
    /// The amount of tiles radius the tower can attack around.
    /// </summary>
    public int TowerRange;

    /// <summary>
    /// The tower image.
    /// </summary>
    public Bitmap TowerImage;
}
