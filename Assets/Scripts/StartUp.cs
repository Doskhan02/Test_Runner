using Scellecs.Morpeh;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] GameObject player;

    private World world;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        player.SetActive(true);
        this.world = World.Default;

        var systemsGroup = this.world.CreateSystemsGroup();
        systemsGroup.AddSystem(new TileSpawnSystem());
        systemsGroup.AddSystem(new PlayerMovementSystem());
        systemsGroup.AddSystem(new SpeedOverTimeSystem());
        systemsGroup.AddSystem(new CollisionTriggerSystem());
        systemsGroup.AddSystem(new TileMovementSystem());
        systemsGroup.AddSystem(new ScoreSystem());

        this.world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
