using System;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Room Prefab", menuName = "LD50/Room Prefab")]
public class RoomPrefab : ScriptableObject
{
    public string Name;
    public Tilemap MetaTiles;
    public Tilemap PaletteTiles;

    private void Awake()
    {
        // Map MetaTiles to PaletteTiles.
    }
}
