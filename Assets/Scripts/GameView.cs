using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _gemsCountText;

    private void OnEnable()
    {
        _hero.Hurt += UpdateHeroHealth;
        _hero.Dead += OnHeroDead;
        _hero.GemsCountChanged += UpdateHeroGems;
    }

    private void OnDisable()
    {
        _hero.Hurt -= UpdateHeroHealth;
        _hero.Dead -= OnHeroDead;
        _hero.GemsCountChanged -= UpdateHeroGems;
    }

    private void Start()
    {
        UpdateHeroHealth();
        UpdateHeroGems();
    }

    private void UpdateHeroHealth()
    {
        _healthText.text = _hero.Health.ToString();
    }

    private void UpdateHeroGems()
    {
        _gemsCountText.text = _hero.GemsCount.ToString();
    }

    private void OnHeroDead()
    {
        _healthText.text = _gemsCountText.text = "0";
    }
}
