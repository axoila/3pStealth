using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState {
    public abstract void UpdateState();
    public abstract void OnStateEntered();
    protected abstract void TransitionToState(EnemyState state);
}
