// <summary>
// A struct to define a possible grid square.
// </summary>
// <copyright file="Grid.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

/// <summary>
/// A grid in the game.
/// </summary>
internal readonly struct Grid
{
    /// <summary>
    /// The grid ID.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> struct.
    /// </summary>
    /// <param name="id">The id of the grid.</param>
    public Grid(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Returns a printable version of a Grid.
    /// </summary>
    /// <returns>The readable version of a Grid.</returns>
    public override string ToString() => $"Grid[#{Id}]";
}