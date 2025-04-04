using UnityEngine;
using TMPro;
using Scellecs.Morpeh;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameStartButton;
    [SerializeField] private TMP_Text scoreText;

    Filter _scoreFilter;

    Stash<ScoreComponent> _scoreStash;

    private void Start()
    {
        _scoreFilter = World.Default.Filter.With<ScoreComponent>().With<SpeedComponent>().Build();
        _scoreStash = World.Default.GetStash<ScoreComponent>();
    }
    void Update()
    {
        if (_scoreFilter.IsEmpty())
            return;
        Entity entity = _scoreFilter.First();
        ref var scoreComponent = ref _scoreStash.Get(entity);
        scoreText.text = "Score: " + "\n" + (int)scoreComponent.score;
    }
    public void StartGame()
    {
        gameStartButton.SetActive(false);
    }
}
