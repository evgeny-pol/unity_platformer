using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Creature))]
public class PatrolAI : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _waypointApproachDistance = 0.1f;
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypointIndex;
    private Creature _creature;

    private Vector3 CurrentWaypoint => _waypoints[_currentWaypointIndex].position;

    private void Awake()
    {
        _creature = GetComponent<Creature>();
    }

    private void OnEnable()
    {
        if (_waypoints.Length > 0)
            UpdateMovementDirection();
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
                _creature.StopMovement();
                enabled = false;
                return;
            }

            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
            UpdateMovementDirection();
        }
    }

    private void UpdateMovementDirection()
    {
        if (CurrentWaypoint.x > transform.position.x)
            _creature.MoveRight();
        else
            _creature.MoveLeft();
    }
}
