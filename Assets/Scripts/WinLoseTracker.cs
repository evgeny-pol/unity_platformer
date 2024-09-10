using UnityEngine;

public class WinLoseTracker : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField, Min(1)] private int _gemCountTarget = 1;
    [SerializeField] private GameObject _gameWonPanel;
    [SerializeField] private GameObject _gameLostPanel;

    private void Start()
    {
        _hero.GemsCountChanged += OnGemsCountChanged;
        _hero.Dead += OnHeroDead;
    }

    private void OnGemsCountChanged()
    {
        if (_hero.GemsCount == _gemCountTarget)
            StopGame(_gameWonPanel);

        // todo
    }

    private void OnHeroDead()
    {
        StopGame(_gameLostPanel);

        // todo
    }

    private void StopGame(GameObject gameResultPanel)
    {
        gameResultPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // todo
}
