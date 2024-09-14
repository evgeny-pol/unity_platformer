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
        _hero.Inventory.ItemCountChanged += OnItemCountChanged;
    }

    private void OnItemCountChanged(ItemType itemType, int newCount)
    {
        if (_itemClipsDict.TryGetValue(itemType, out ItemClip itemClip))
            _audioSource.PlayOneShot(itemClip.AudioClip, itemClip.Volume);
    }

    [Serializable]
    public class ItemClip
    {
        [SerializeField] public ItemType ItemType;
        [SerializeField] public AudioClip AudioClip;
        [SerializeField, Min(0f)] public float Volume = 1f;
    }
}
