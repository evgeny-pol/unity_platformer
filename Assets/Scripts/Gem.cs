using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Gem : MonoBehaviour
{
    [SerializeField, Min(1)] private int _count = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            hero.CollectGem(_count);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetBool(AnimatorParams.IsCollected, true);
        }
    }

    public void OnDisappeared()
    {
        Destroy(gameObject);

        // todo game over
        // todo game won
    }
}
