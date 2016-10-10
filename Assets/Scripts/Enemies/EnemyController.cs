using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    public float DestinationReachedTolerance { get; set; }

    private Transform[] _foundPlayers;
    private Transform _currentDestination;
    private int _currentDestinationIndex;

    private readonly EnemyBase _enemy;
    private readonly DetectorCone _detector;
    private readonly NavMeshAgent _agent;
    private readonly Transform[] _path;

    public EnemyController(EnemyBase enemy, DetectorCone detector, NavMeshAgent agent, Transform[] path)
    {
        _enemy = enemy;
        _detector = detector;
        _agent = agent;
        _path = path;
        InitPatrol();
    }

    private void InitPatrol()
    {
        _currentDestinationIndex = 0;
        _currentDestination = _path[0];
        MoveToDestination();
    }

    public void UpdateController()
    {
        if (IsDestinationReached())
        {
            SetDestinationToNext();
            MoveToDestination();
        }
        FindPlayersInRange();
        KillFoundPlayers();
    }

    private bool IsDestinationReached()
    {
        var currentPos = _agent.transform.position;
        var targetPos = _currentDestination.position;
        var isReached = Vector3.Distance(currentPos, targetPos) < DestinationReachedTolerance;

        return isReached;
    }

    private void MoveToDestination()
    {
        _enemy.MoveTo(_currentDestination);
    }

    private void SetDestinationToNext()
    {
        _currentDestination = _path[++_currentDestinationIndex % _path.Length];
    }

    private void FindPlayersInRange()
    {
        _foundPlayers = _detector.GetPlayerTransformsInRange();
    }

    private void KillFoundPlayers()
    {
        _enemy.Kill(_foundPlayers);
    }
}
