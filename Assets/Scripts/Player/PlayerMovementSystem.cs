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
    private Stash<PlayerMovementComponent> playerMovementStash;
    public void OnAwake() 
    {
        _playerFilter = World.Filter
            .With<PlayerTag>()
            .With<PlayerMovementComponent>()
            .With<TransformComponent>()
            .With<SpeedComponent>()
            .With<ActivePlayerInput>().Build();

        transformStash = World.GetStash<TransformComponent>();
        speedStash = World.GetStash<SpeedComponent>();
        inputStash = World.GetStash<ActivePlayerInput>();
        playerMovementStash = World.GetStash<PlayerMovementComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in _playerFilter)
        {
            ref var transformComponent = ref transformStash.Get(entity);
            ref var speedComponent = ref speedStash.Get(entity);
            ref var inputComponent = ref inputStash.Get(entity);
            ref var playerMovementComponent = ref playerMovementStash.Get(entity);

            transformComponent.Transform.position =
                UnityEngine.Vector3.Lerp(transformComponent.Transform.position,
                new Vector3(playerMovementComponent.posX, 1, 0),
                5 * deltaTime);

            playerMovementComponent.delay -= deltaTime;
            if (inputComponent.horizontal != 0 && playerMovementComponent.delay <= 0)
            {
                playerMovementComponent.delay = 0.3f;
                if (playerMovementComponent.currentPosition == 0)
                {
                    playerMovementComponent.posX = playerMovementComponent.offset * inputComponent.horizontal;

                    if (inputComponent.horizontal > 0)
                        playerMovementComponent.currentPosition = 1;
                    else
                    {
                        playerMovementComponent.currentPosition = -1;
                    }
                }
                else
                {
                    if((playerMovementComponent.currentPosition == 1 && inputComponent.horizontal > 0) ||
                        (playerMovementComponent.currentPosition == -1 && inputComponent.horizontal < 0))
                    {
                        return;
                    }
                    else
                    {
                        playerMovementComponent.posX = 0;
                        playerMovementComponent.currentPosition = 0;
                    }
                }

            }
        }
    }

    public void Dispose()
    {

    }
}