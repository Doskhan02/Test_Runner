using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class SpeedOverTimeSystem : ISystem 
{
    public World World { get; set;}
    private Filter _speedOverTimeFilter;
    private Stash<SpeedComponent> speedStash;
    public float globalSpeed = 5f;

    public void OnAwake() 
    {
        _speedOverTimeFilter = World.Filter.With<SpeedComponent>().Build();
        speedStash = World.GetStash<SpeedComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        globalSpeed += 0.3f * deltaTime;
        foreach (var entity in _speedOverTimeFilter)
        {
            ref var speedComponent = ref speedStash.Get(entity);
            speedComponent.speed = globalSpeed;
        }
    }

    public void Dispose()
    {

    }
}