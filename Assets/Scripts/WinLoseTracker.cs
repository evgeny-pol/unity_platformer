using UnityEngine;

public class WinLoseTracker : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField, Min(1)] private int _gemCountTarget = 1;
    [SerializeField] private GameResultPanel _gameWonPanel;
    [SerializeField] private GameResultPanel _gameLostPanel;

    private void Start()
    {
        _hero.Inventory.ItemCountChanged += OnItemCountChanged;
        _hero.Dead += OnHeroDead;
    }

    private void OnDisable()
    {
        _hero.Inventory.ItemCountChanged -= OnItemCountChanged;
        _hero.Dead -= OnHeroDead;
    }

    private void OnItemCountChanged(ItemType itemType, int newCount)
    {
        if (itemType == ItemType.Gem && newCount >= _gemCountTarget)
            ShowGameResult(_gameWonPanel, true);
    }

    private void OnHeroDead()
    {
        ShowGameResult(_gameLostPanel, false);
    }

    private void ShowGameResult(GameResultPanel gameResultPanel, bool stopGame)
    {
        gameResultPanel.Show();

        if (stopGame)
            Time.timeScale = 0;
    }
}
