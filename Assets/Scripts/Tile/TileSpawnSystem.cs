using System.Collections.Generic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TileSpawnSystem : ISystem 
{
    public World World { get; set;}
    private Filter _tileFilter;
    private Filter _tileSpawnerFilter;
    private Stash<TileSpawnComponent> tileSpawnStash;
    private Stash<TransformComponent> transformStash;

    private List<GameObject> tilePrefabs = new List<GameObject>();
    private readonly Queue<GameObject> spawnedTiles = new();

    Entity spawnerEntity;

    public void OnAwake() 
    {

        _tileFilter = World.Filter.With<TileTag>().Build();
        _tileSpawnerFilter = World.Filter.With<TileSpawnComponent>().Build();
        tileSpawnStash = World.GetStash<TileSpawnComponent>();
        transformStash = World.GetStash<TransformComponent>();
        spawnerEntity = _tileSpawnerFilter.GetEntity(0);
        Init();
    }
    public void OnUpdate(float deltaTime) 
    {
        ref var tileSpawnComponent = ref tileSpawnStash.Get(spawnerEntity);
        foreach (var entity in _tileFilter)
        {
            ref var transform = ref transformStash.Get(entity);
            if(entity == null)
                continue;
            if (transform.Transform.position.z < tileSpawnComponent.despawnPos.z)
            {
                Object.Destroy(spawnedTiles.Dequeue());
                World.RemoveEntity(entity);
                SpawnTile(tileSpawnComponent.spawnPos);
            }
        }
    }
    public void Init()
    {
        spawnedTiles.Clear();
        tilePrefabs.Clear();
        ref var tileSpawnComponent = ref tileSpawnStash.Get(spawnerEntity);
        for (int i = 0; i < tileSpawnComponent.prefabs.Count; i++)
        {
            tilePrefabs = tileSpawnComponent.prefabs;
        }

        for (int i = 0; i < tileSpawnComponent.initSpawnCount; i++)
        {
            if(i < 3)
            {
                GameObject go1 = Object.Instantiate(tilePrefabs[0]);
                go1.transform.position = new Vector3(0, -0.5f, i * 10);
                spawnedTiles.Enqueue(go1);
                continue;
            }
            GameObject go = Object.Instantiate(tilePrefabs[Random.Range(0,tilePrefabs.Count)]);
            go.transform.position = new Vector3(0, -0.5f,i * 10);
            spawnedTiles.Enqueue(go);
        }
    }
    public void SpawnTile(Vector3 spawnPos)
    {
        GameObject go = Object.Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Count)]);
        go.transform.position = spawnPos;
        spawnedTiles.Enqueue(go);
    }
    public void Dispose()
    {

    }
}