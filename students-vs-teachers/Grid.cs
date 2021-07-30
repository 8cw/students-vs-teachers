// <summary>
// A struct to define a possible grid square.
// </summary>
// <copyright file="Grid.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

using System.Windows.Forms;

/// <summary>
/// A grid in the game.
/// </summary>
internal struct Grid
{
    /// <summary>
    /// The grid ID.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// The image associated with this grid.
    /// </summary>
    public readonly PictureBox GridImage;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> struct.
    /// </summary>
    /// <param name="id">The id of the grid.</param>
    /// <param name="gridImage"> The image of the grid.</param>
    public Grid(int id, PictureBox gridImage)
    {
        Id = id;
        GridImage = gridImage;
    }

    /// <summary>
    /// Returns a printable version of a Grid.
    /// </summary>
    /// <returns>The readable version of a Grid.</returns>
    public override string ToString() => $"Grid[#{Id}]";
}