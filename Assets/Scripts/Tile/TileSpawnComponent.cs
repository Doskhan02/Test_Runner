using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using System.Collections.Generic;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct TileSpawnComponent : IComponent 
{
    public List<GameObject> prefabs;
    public Vector3 spawnPos;
    public Vector3 despawnPos;
    public int initSpawnCount;
}