using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager {

    public EnemyState CurrentState { set; get; }
    public PatrolState PatrolState { private set; get; }
    public ChaseState ChaseState { private set; get; }
    public AlertState AlertState { private set; get; }

    public GameObject EnemyGameObject { private set; get; }
    public EnemyBase EnemyBaseObject { private set; get; }

    private MeshRenderer _stateIndicator;

    public EnemyStateManager(GameObject enemyGameObject, MeshRenderer stateIndicator)
    {
        PatrolState = new PatrolState(this);
        ChaseState = new ChaseState(this);
        AlertState = new AlertState(this);
        EnemyGameObject = enemyGameObject;
        EnemyBaseObject = enemyGameObject.GetComponent<EnemyBase>();
        _stateIndicator = stateIndicator;

        CurrentState = PatrolState;
    }

    public void UpdateState()
    {
        CurrentState.UpdateState();
    }
}
