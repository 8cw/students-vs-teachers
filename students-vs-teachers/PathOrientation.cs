// <summary>
// A struct to define a possible series of tiles the teachers can travel in.
// </summary>
// <copyright file="PathOrientation.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

/// <summary>
/// A possible series of tiles the teachers can travel in.
/// </summary>
internal struct PathOrientation
{
    /// <summary>
    /// An integer representing the degrees the teacher should face in when traveling.
    /// </summary>
    public int Orientation;

    /// <summary>
    /// An array of all the tiles the teacher can travel in.
    /// </summary>
    public int[] TileIds;

    /// <summary>
    /// Returns a printable version of a Grid.
    /// </summary>
    /// <returns>The readable version of a Grid.</returns>
    public override string ToString() => $"PathOrientation[{Orientation}° #{TileIds.Length} Tiles]";
}