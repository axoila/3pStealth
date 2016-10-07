using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private readonly EnemyStateManager _manager;

    public ChaseState(EnemyStateManager manager)
    {
        _manager = manager;
    }

    public override void OnStateEntered()
    {
        throw new NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new NotImplementedException();
    }

    protected override void TransitionToState(EnemyState state)
    {
        throw new NotImplementedException();
    }
}
