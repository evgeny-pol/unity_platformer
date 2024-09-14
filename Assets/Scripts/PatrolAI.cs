using UnityEngine;

[RequireComponent(typeof(Creature))]
public class PatrolAI : AI
{
    [SerializeField, Min(0f)] private float _waypointApproachDistance = 0.1f;
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypointIndex;

    private Vector3 CurrentWaypoint => _waypoints[_currentWaypointIndex].position;

    private void OnEnable()
    {
        if (_waypoints.Length > 0)
            Creature.MoveTowards(CurrentWaypoint);
        else
            enabled = false;
    }

    private void Update()
    {
        Vector3 toWaypoint = CurrentWaypoint - transform.position;

        if (Mathf.Abs(toWaypoint.x) <= _waypointApproachDistance)
        {
            if (_waypoints.Length == 1)
            {
                Creature.StopMovement();
                enabled = false;
                return;
            }

            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
            Creature.MoveTowards(CurrentWaypoint);
        }
    }

    public void StartPatrol()
    {
        enabled = true;
    }

    public void StopPatrol()
    {
        enabled = false;
    }
}
