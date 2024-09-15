using UnityEngine;

[RequireComponent(typeof(Creature))]
public class AI : MonoBehaviour
{
    protected Creature Creature;

    protected virtual void Awake()
    {
        Creature = GetComponent<Creature>();
    }
}
