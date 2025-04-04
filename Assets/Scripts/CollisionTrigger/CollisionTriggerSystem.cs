using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class CollisionTriggerSystem : ISystem 
{
    public World World { get; set;}
    private Filter _playerColliderFilter;
    private Filter _obstacleColliderFilter;
    private Stash<TriggerComponent> _triggerStash;
    private MeshCollider playerMeshCollider;
    private List<MeshCollider> obstacleColliders = new List<MeshCollider>();
    public void OnAwake() 
    {
        _obstacleColliderFilter = World.Filter.With<ObstacleTag>().With<TriggerComponent>().Build();
        _playerColliderFilter = World.Filter.With<PlayerTag>().With<TriggerComponent>().Build();
        _triggerStash = World.GetStash<TriggerComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in _playerColliderFilter)
        {
            ref var triggerComponent = ref _triggerStash.Get(entity);
            playerMeshCollider = triggerComponent.meshCollider;
        }
        _obstacleColliderFilter = World.Filter.With<ObstacleTag>().With<TriggerComponent>().Build();
        obstacleColliders.Clear();
        foreach (var entity in _obstacleColliderFilter)
        {
            ref var triggerComponent = ref _triggerStash.Get(entity);
            obstacleColliders.Add(triggerComponent.meshCollider);
        }
        foreach (var obstacleCollider in obstacleColliders)
        {
            if (playerMeshCollider.bounds.Intersects(obstacleCollider.bounds))
            {
                UnityEngine.Time.timeScale = 0;
            }
        }
    }

    public void Dispose()
    {

    }
}