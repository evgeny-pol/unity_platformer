using UnityEngine;

public class AI : MonoBehaviour
{
    protected Creature Creature;

    protected virtual void Awake()
    {
        Creature = GetComponent<Creature>();
    }
}
