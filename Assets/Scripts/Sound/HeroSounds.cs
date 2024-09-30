using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Hero))]
[RequireComponent(typeof(AudioSource))]
public class HeroSounds : MonoBehaviour
{
    [SerializeField] private ItemClip[] _itemClips;

    private Hero _hero;
    private AudioSource _audioSource;
    private Dictionary<ItemType, ItemClip> _itemClipsDict;

    private void Awake()
    {
        _hero = GetComponent<Hero>();
        _audioSource = GetComponent<AudioSource>();
        _itemClipsDict = _itemClips.ToDictionary(clip => clip.ItemType);
    }

    private void Start()
    {
        _hero.ItemCountChanged += OnItemCountChanged;
    }

    private void OnDisable()
    {
        _hero.ItemCountChanged -= OnItemCountChanged;
    }

    private void OnItemCountChanged(ItemType itemType, int changeAmount)
    {
        if (changeAmount > 0 && _itemClipsDict.TryGetValue(itemType, out ItemClip itemClip))
            _audioSource.PlayOneShot(itemClip.AudioClip, itemClip.Volume);
    }

    [Serializable]
    public class ItemClip
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField, Min(0f)] private float _volume = 1f;

        public ItemType ItemType => _itemType;

        public AudioClip AudioClip => _audioClip;

        public float Volume => _volume;
    }
}
