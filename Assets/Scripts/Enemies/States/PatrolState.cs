using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private readonly EnemyStateManager _manager;
    private readonly PathProvider _pathProvider;
    private Transform[] _patrolPath;
    private Transform _currenPatrolDestination;
    private int _currentPathIndex;
    private float _patrolSpeed;
    private float _waitTime = 2f;
    private float _searchRadius = 5f;
    private LayerMask _obstacleMask;

    private const float DestinationReachedTolerance = 2.2f; //Enemy is tall and Destinations are on the ground

    public PatrolState(EnemyStateManager manager)
    {
        _manager = manager;
        _pathProvider = PathProvider.GetInstance();
        _patrolPath = new Transform[] {};
    }

    public override void UpdateState()
    {
        try
        {
            Patrol();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(string.Format("{0}\n{1}", exception.Message, "Setting default path."));
        }
    }

    private void Patrol()
    {
        if (_currenPatrolDestination == null) throw new NullReferenceException();

        FindPlayers();
        if (!_manager.EnemyBaseObject.IsWaiting)
            Move();
        if (IsDestinationReached())
            SetDestinationToNextNode();
    }

    private void Move()
    {
        var enemy = _manager.EnemyGameObject;
        var distanceVector = _currenPatrolDestination.position - enemy.transform.position;
        var distance = distanceVector.magnitude;
        var direction = Vector3.Normalize(distanceVector);
        var minSpeed = _patrolSpeed*0.2f;
        var maxSpeed = _patrolSpeed;
        var currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, distance/maxSpeed);

        direction.y = 0;
        enemy.transform.position = direction*currentSpeed*Time.deltaTime + enemy.transform.position;
    }

    private bool IsDestinationReached()
    {
        var currentPos = _manager.EnemyGameObject.transform.position;
        var destinationPos = _currenPatrolDestination.position;

        return Vector3.Distance(currentPos, destinationPos) <= DestinationReachedTolerance;
    }

    private void SetDestinationToNextNode()
    {
        _manager.EnemyBaseObject.Wait(_waitTime);
        _currentPathIndex = ++_currentPathIndex%_patrolPath.Length;
        _currenPatrolDestination = _patrolPath[_currentPathIndex];
    }

    private Transform FindPlayers()
    {
        var colliders = Physics.OverlapSphere(_manager.EnemyGameObject.transform.position,
            _manager.EnemyBaseObject.SearchRadius);
        var players = colliders.Where(x => x.CompareTag("Player")).Select(x => x.transform).ToArray();
        var results = new List<Transform>();

        var enemyPos = _manager.EnemyGameObject.transform.position;
        var angle = _manager.EnemyBaseObject.SearchAngle;
        var radius = _manager.EnemyBaseObject.SearchRadius;
        var angleLine1 = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad / 2), 0, Mathf.Cos(angle * Mathf.Deg2Rad / 2));
        var angleLine2 = new Vector3(Mathf.Sin(-angle * Mathf.Deg2Rad / 2), 0, Mathf.Cos(-angle * Mathf.Deg2Rad / 2));
        Debug.DrawLine(enemyPos, angleLine1*radius + enemyPos, Color.red);        
        Debug.DrawLine(enemyPos, angleLine2*radius + enemyPos, Color.red);

        for (var i = 0; i < players.Length; i++)
        {
            var player = players[i].transform;
            var enemy = _manager.EnemyGameObject.transform;
            var direction = (player.position - enemy.position).normalized;
            if (Vector3.Angle(enemy.forward, direction) < _manager.EnemyBaseObject.SearchAngle/2)
            {
                var distance = Vector3.Distance(enemy.position, player.position);
                if (!Physics.Raycast(enemy.position, direction, distance, _obstacleMask))
                {
                    //these are all the players we can see
                    Debug.Log("Found player");
                    results.Add(player);
                }
            }
        }

        return results.FirstOrDefault();
    }

    public void SetToNewPathOrDefault(int newPathIndex)
    {
        _patrolPath = _pathProvider.GetPathOrDefault(newPathIndex);
        _currentPathIndex = 0;
        _currenPatrolDestination = _patrolPath[_currentPathIndex];
    }

    public void SetPatrolSpeed(float speed)
    {
        _patrolSpeed = speed;
    }

    public override void OnStateEntered()
    {
        Patrol();
    }

    protected override void TransitionToState(EnemyState state)
    {
        _manager.CurrentState = state;
        state.OnStateEntered();
    }
}
