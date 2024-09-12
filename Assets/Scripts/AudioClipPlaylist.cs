using System.Collections;
using UnityEngine;

public class AudioClipPlaylist : MonoBehaviour
{
    [SerializeField] private bool _shuffle;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;

    private WaitForSeconds[] _clipLengths;
    private int _currentClipIndex;
    private Coroutine _playlistCoroutine;

    private void Awake()
    {
        if (_shuffle)
        {
            for (int i = 0; i < _clips.Length; ++i)
            {
                int newIndex = Random.Range(0, _clips.Length);

                if (newIndex != i)
                    (_clips[i], _clips[newIndex]) = (_clips[newIndex], _clips[i]);
            }
        }

        _clipLengths = new WaitForSeconds[_clips.Length];

        for (int i = 0; i < _clips.Length; ++i)
            _clipLengths[i] = new WaitForSeconds(_clips[i].length);
    }

    private void OnEnable()
    {
        if (_currentClipIndex < _clips.Length)
            _playlistCoroutine = StartCoroutine(Play());
    }

    private void OnDisable()
    {
        if (_playlistCoroutine != null)
            StopCoroutine(_playlistCoroutine);

        _audioSource.Stop();
    }

    private IEnumerator Play()
    {
        while (enabled)
        {
            AudioClip clip = _clips[_currentClipIndex];
            _audioSource.clip = clip;
            _audioSource.Play();
            yield return _clipLengths[_currentClipIndex];
            _currentClipIndex = ++_currentClipIndex % _clips.Length;
        }
    }
}
