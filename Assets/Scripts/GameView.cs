using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _gemsCountText;

    private void Start()
    {
        _hero.HealthChanged += OnHealthChanged;
        _hero.Dead += OnHeroDead;
        _hero.Inventory.ItemCountChanged += OnItemCountChanged;
        OnHealthChanged(0);
        UpdateHeroGems(_hero.Inventory.GetCount(ItemType.Gem));
    }

    private void OnDisable()
    {
        _hero.HealthChanged -= OnHealthChanged;
        _hero.Dead -= OnHeroDead;
        _hero.Inventory.ItemCountChanged -= OnItemCountChanged;
    }

    private void OnHealthChanged(int changeAmount)
    {
        _healthText.text = _hero.Health.ToString();
    }

    private void OnItemCountChanged(ItemType itemType, int newCount)
    {
        if (itemType == ItemType.Gem)
            UpdateHeroGems(newCount);
    }

    private void UpdateHeroGems(int newCount)
    {
        _gemsCountText.text = newCount.ToString();
    }

    private void OnHeroDead()
    {
        _healthText.text = _gemsCountText.text = "0";
    }
}
