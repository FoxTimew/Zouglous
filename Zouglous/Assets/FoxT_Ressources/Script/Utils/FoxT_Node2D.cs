using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UnityEngine;
using MathExtension;

public class FoxT_Node2D
{
    public bool walkable;
    public Vector2 worldPosition;
    public Vector2 size;
    public FoxT_Node2D(bool _walkable, Vector2 _worldPos, int gridX, int gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        size = new Vector2(gridX, gridY);
    }

    public bool isWalkable()
    {
        return walkable;
    }

    public Vector3 WorldPosition()
    {
        return Mathe.Vector2To3(worldPosition);
    }
}
