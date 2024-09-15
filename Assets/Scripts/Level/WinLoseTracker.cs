using UnityEngine;

public class WinLoseTracker : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private ItemType _winningItemType = ItemType.Gem;
    [SerializeField, Min(1)] private int _winningItemCountTarget = 1;
    [SerializeField] private GameResultPanel _gameWonPanel;
    [SerializeField] private GameResultPanel _gameLostPanel;

    private void Start()
    {
        _hero.ItemCountChanged += OnItemCountChanged;
        _hero.Dead += OnHeroDead;
    }

    private void OnDisable()
    {
        _hero.ItemCountChanged -= OnItemCountChanged;
        _hero.Dead -= OnHeroDead;
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount)
    {
        if (itemType == _winningItemType && _hero.GetItemCount(itemType) >= _winningItemCountTarget)
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
