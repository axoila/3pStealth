using System;
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
    private float _waitTime;

    private const float DestinationReachedTolerance = 2.2f; //Enemy is tall and Destinations are on the ground

    public PatrolState(EnemyStateManager manager)
    {
        _manager = manager;
        _pathProvider = PathProvider.GetInstance();   
        _patrolPath=new Transform[] {};
        _waitTime = 2f;
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
        if(_currenPatrolDestination==null)throw new NullReferenceException();

        if (!_manager.EnemyBaseObject.IsWaiting)
            Move();
        if(IsDestinationReached())            
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
        Debug.Log(currentSpeed);
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
        _currentPathIndex = ++_currentPathIndex % _patrolPath.Length;
        _currenPatrolDestination = _patrolPath[_currentPathIndex];
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
