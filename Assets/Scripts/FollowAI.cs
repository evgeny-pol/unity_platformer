using UnityEngine;

[RequireComponent(typeof(Creature))]
public class FollowAI : AI
{
    [SerializeField] private Transform _target;

    private void OnEnable()
    {
        if (_target == null)
            enabled = false;
    }

    private void Update()
    {
        if (_target == null)
            StopFollowing();
        else
            Creature.MoveTowards(_target.position);
    }

    public void StartFollowing(Transform target)
    {
        _target = target;
        enabled = true;
    }

    public void StopFollowing()
    {
        Creature.StopMovement();
        _target = null;
        enabled = false;
    }
}
