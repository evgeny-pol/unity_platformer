using Assets.Scripts.Utils;
using UnityEngine;

[RequireComponent(typeof(Mob))]
[RequireComponent(typeof(AudioSource))]
public class MobSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _alertClip;
    [SerializeField, Min(0f)] private float _alertVolume = 1f;
    [SerializeField] private Cooldown _alertCooldown;

    private Mob _mob;
    private AudioSource _audioSource;

    private void Awake()
    {
        _mob = GetComponent<Mob>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _mob.HeroFound += OnHeroFound;
    }

    private void OnHeroFound()
    {
        if (_alertCooldown.IsReady)
        {
            _audioSource.PlayOneShot(_alertClip, _alertVolume);
            _alertCooldown.Reset();
        }
    }
}
