using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class TileMovementSystem : ISystem 
{
    public World World { get; set;}
    private Filter _tileFilter;
    private Stash<SpeedComponent> speedStash;
    private Stash<TransformComponent> transformStash;

    public void OnAwake() 
    {
        _tileFilter = World.Filter.
            With<TileTag>().
            With<SpeedComponent>().
            With<TransformComponent>().Build();

        speedStash = World.GetStash<SpeedComponent>();
        transformStash = World.GetStash<TransformComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in _tileFilter)
        {
            ref var speed = ref speedStash.Get(entity);
            ref var transform = ref transformStash.Get(entity);
            transform.Transform.position = 
                UnityEngine.Vector3.Lerp(transform.Transform.position,
                new UnityEngine.Vector3(0, -0.5f, transform.Transform.position.z),
                deltaTime);
            transform.Transform.position += deltaTime * speed.speed * UnityEngine.Vector3.back;
        }
    }

    public void Dispose()
    {

    }
}