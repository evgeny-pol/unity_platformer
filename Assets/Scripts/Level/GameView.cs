using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private Image _healthImage;
    [SerializeField] private ItemCountText[] _itemCountTexts;

    private void Start()
    {
        _hero.HealthChanged += OnHealthChanged;
        _hero.Dead += OnHeroDead;
        _hero.ItemCountChanged += OnItemCountChanged;
        OnHealthChanged(0);
        UpdateItemCounts();
    }

    private void OnDisable()
    {
        _hero.HealthChanged -= OnHealthChanged;
        _hero.Dead -= OnHeroDead;
        _hero.ItemCountChanged -= OnItemCountChanged;
    }

    private void OnHealthChanged(float changeAmount)
    {
        _healthImage.fillAmount = _hero.Health / _hero.HealthMax;
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount)
    {
        ItemCountText itemCountText = _itemCountTexts.FirstOrDefault(text => text.ItemType == itemType);

        if (itemCountText != null)
            itemCountText.TextField.text = _hero.GetItemCount(itemType).ToString();
    }

    private void OnHeroDead()
    {
        _healthImage.fillAmount = 0;
    }

    private void UpdateItemCounts()
    {
        foreach (ItemCountText itemCountText in _itemCountTexts)
            itemCountText.TextField.text = _hero.GetItemCount(itemCountText.ItemType).ToString();
    }

    [Serializable]
    public class ItemCountText
    {
        [SerializeField] public ItemType ItemType;
        [SerializeField] public Text TextField;
    }
}
