using UnityEngine;

[RequireComponent(typeof(Creature))]
public class PatrolAI : AI
{
    [SerializeField, Min(0f)] private float _waypointApproachDistance = 0.1f;
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypointIndex;

    private Transform CurrentWaypoint => _waypoints[_currentWaypointIndex];

    private void OnEnable()
    {
        MoveToCurrentWaypoint();
    }

    private void Update()
    {
        Vector3 toWaypoint = CurrentWaypoint.position - transform.position;

        if (Mathf.Abs(toWaypoint.x) <= _waypointApproachDistance)
        {
            if (_waypoints.Length == 1)
            {
                Creature.StopMovement();
                enabled = false;
                return;
            }

            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
            MoveToCurrentWaypoint();
        }
    }

    private void MoveToCurrentWaypoint()
    {
        if (_currentWaypointIndex >= _waypoints.Length)
        {
            StopPatrol();
            return;
        }

        Transform currentWaypoint = CurrentWaypoint;

        if (currentWaypoint == null)
        {
            StopPatrol();
            return;
        }

        Creature.MoveTowards(currentWaypoint.position);
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
