using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    protected override void Start () {
        stateManager = new EnemyStateManager(gameObject, stateIndicator);
        TestInit();
    }
	
	// Update is called once per frame
    protected override void Update () {
		stateManager.UpdateState();
	}

    protected override void UpdateStateIndicator()
    {
        if(stateManager.CurrentState is PatrolState)ChangeIndicatorColor(Color.green);
        if(stateManager.CurrentState is AlertState)ChangeIndicatorColor(Color.yellow);
        if(stateManager.CurrentState is ChaseState)ChangeIndicatorColor(Color.red);
    }

    private void ChangeIndicatorColor(Color color)
    {
        stateIndicator.material.color = color;
    }

    public override void Wait(float duration)
    {
        StartCoroutine(WaitRoutine(duration));
    }

    private IEnumerator WaitRoutine(float duration)
    {
        IsWaiting = true;
        yield return new WaitForSeconds(duration);
        IsWaiting = false;
    }

    public void TestInit()
    {
        stateManager.PatrolState.SetToNewPathOrDefault(1);
        stateManager.PatrolState.SetPatrolSpeed(5f);
    }
}
