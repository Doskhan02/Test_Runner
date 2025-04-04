using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerMovementSystem : ISystem 
{
    public World World { get; set;}
    private Filter _playerFilter;
    private Stash<TransformComponent> transformStash;
    private Stash<SpeedComponent> speedStash;
    private Stash<ActivePlayerInput> inputStash;
    private const int OFFSET = 5;
    private int currentPosition = 0;
    private float delay = 0;
    private float posX = 0;
    public void OnAwake() 
    {
        _playerFilter = World.Filter
            .With<PlayerTag>()
            .With<TransformComponent>()
            .With<SpeedComponent>()
            .With<ActivePlayerInput>().Build();

        transformStash = World.GetStash<TransformComponent>();
        speedStash = World.GetStash<SpeedComponent>();
        inputStash = World.GetStash<ActivePlayerInput>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in _playerFilter)
        {
            ref var transformComponent = ref transformStash.Get(entity);
            ref var speedComponent = ref speedStash.Get(entity);
            ref var inputComponent = ref inputStash.Get(entity);

            transformComponent.Transform.position =
                UnityEngine.Vector3.Lerp(transformComponent.Transform.position,
                new Vector3(posX, 1, 0),
                5 * deltaTime);

            delay -= deltaTime;
            if (inputComponent.horizontal != 0 && delay <= 0)
            {
                delay = 0.3f;
                if (currentPosition == 0)
                {
                    posX = OFFSET * inputComponent.horizontal;

                    if (inputComponent.horizontal > 0)
                        currentPosition = 1;
                    else
                    {
                        currentPosition = -1;
                    }
                }
                else
                {
                    posX = 0;
                    currentPosition = 0;
                }

            }
        }
    }

    public void Dispose()
    {

    }
}