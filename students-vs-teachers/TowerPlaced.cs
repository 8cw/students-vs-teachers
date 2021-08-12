// <summary>
// A structure to define a possible tower that has been placed.
// </summary>
// <copyright file="TowerPlaced.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Drawing;

/// <summary>
/// A struct to define a possible tower placed down.
/// </summary>
public struct TowerPlaced
{
    /// <summary>
    /// The ID of the tower placed down.
    /// </summary>
    public int TowerId;

    /// <summary>
    /// The amount of game ticks that have passed since the tower last shot.
    /// </summary>
    public int AttackDebounce;

    /// <summary>
    /// The grid ID the tower consumes.
    /// </summary>
    public int GridId;
}
