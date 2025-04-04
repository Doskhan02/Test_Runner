using System.Diagnostics;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class InputSystem : ISystem 
{
    public World World { get; set;}
    Filter _inputFilter;
    Stash<ActivePlayerInput> _activePlayerInputStash;
    public void OnAwake() 
    {
        _inputFilter = World.Filter.With<ActivePlayerInput>().Build();
        _activePlayerInputStash = World.GetStash<ActivePlayerInput>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in _inputFilter)
        {
            ref var activePlayerInput = ref _activePlayerInputStash.Get(entity);
            activePlayerInput.horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        }
    }

    public void Dispose()
    {

    }
}