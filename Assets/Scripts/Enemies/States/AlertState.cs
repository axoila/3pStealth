using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : EnemyState
{
    public float MaxAlertDuration { private set; get; }
    public float RemainingAlertDuration { private set; get; }


    private readonly EnemyStateManager _manager;
    private const float DefaultAlertDuration = 10f;

    public AlertState(EnemyStateManager manager)
    {        
        _manager = manager;
        MaxAlertDuration = DefaultAlertDuration;
    }

    public override void UpdateState()
    {
        RemainingAlertDuration -= Time.deltaTime;
        if(RemainingAlertDuration<0f)TransitionToState(_manager.PatrolState);
    }

    public override void OnStateEntered()
    {
        RemainingAlertDuration = MaxAlertDuration;
    }

    protected override void TransitionToState(EnemyState state)
    {
        throw new System.NotImplementedException();
    }

    public void SetTotalAlertTime(float duration)
    {
        MaxAlertDuration = duration;
    }
}
