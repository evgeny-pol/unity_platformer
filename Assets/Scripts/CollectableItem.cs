using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField, Min(1)] private int _count = 1;

    public ItemType Type => _type;

    public int Count => _count;

    public void Collect()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool(AnimatorParams.IsCollected, true);
    }

    public void OnDisappeared()
    {
        Destroy(gameObject);
    }
}
