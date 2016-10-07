using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public bool IsWaiting { protected set; get; }
    public float SearchAngle { protected set; get; }
    public float SearchRadius { protected set; get; }

    [SerializeField] protected MeshRenderer stateIndicator;
    protected EnemyStateManager stateManager;

    public abstract void Wait(float duration);

    protected abstract void Start();
    protected abstract void Update();
    protected abstract void UpdateStateIndicator();
}
