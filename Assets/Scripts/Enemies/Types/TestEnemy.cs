using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    private DetectorCone _detector;
    private EnemyController _controller;

    protected override void Start()
    {
        base.Start();
        SearchRadius = 10f;
        SearchAngle = 90f;
        _detector = new DetectorCone(this.transform, SearchRadius, SearchAngle);

        _controller = new EnemyController(this, _detector, Agent, PathProvider.GetInstance().GetPathOrDefault(1))
        {
            DestinationReachedTolerance = 3f
        };

    }

    protected override void Update()
    {
        _controller.UpdateController();
        _detector.DrawRangeIndicator();
    }

    public override void Wait(float duration)
    {
        throw new System.NotImplementedException();
    }

    public override void MoveTo(Transform destination)
    {
        Agent.SetDestination(destination.position);
        Debug.Log("MoveTo called");
    }

    public override void Kill(Transform[] targets)
    {
        foreach (var target in targets)
        {
            target.SendMessage("YouDeadTest");
            Debug.Log("Pewpew. You take damage.");
        }
    }
}
