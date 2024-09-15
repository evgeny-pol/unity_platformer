using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField, Min(1)] private int _count = 1;

    private Collider2D _collider;
    private Animator _animator;

    public ItemType Type => _type;

    public int Count => _count;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    public void Collect()
    {
        _collider.enabled = false;
        _animator.SetBool(AnimatorParams.IsCollected, true);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
