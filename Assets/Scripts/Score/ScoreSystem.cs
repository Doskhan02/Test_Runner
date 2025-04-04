using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class ScoreSystem : ISystem 
{
    public World World { get; set;}
    private Filter _scoreFilter;
    private Stash<ScoreComponent> _scoreStash;
    private Stash<SpeedComponent> speedStash;
    
    public void OnAwake() 
    {
        _scoreFilter = World.Filter.With<ScoreComponent>().With<SpeedComponent>().Build();
        _scoreStash = World.GetStash<ScoreComponent>();
        speedStash = World.GetStash<SpeedComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        Entity entity = _scoreFilter.First();
        ref var scoreComponent = ref _scoreStash.Get(entity);
        ref var speedComponent = ref speedStash.Get(entity);
        scoreComponent.time += deltaTime;
        if (deltaTime == 0)
            return;
        scoreComponent.score += 0.005f *scoreComponent.time * speedComponent.speed;
    }

    public void Dispose()
    {

    }
}