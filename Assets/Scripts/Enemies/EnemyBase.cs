using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent Agent;

    public bool IsWaiting { protected set; get; }
    public float SearchAngle { protected set; get; }
    public float SearchRadius { protected set; get; }
    public float WalkSpeed { protected set; get; }
    public float TurnSpeed { protected set; get; }

    public abstract void Wait(float duration);
    public abstract void MoveTo(Transform destination);
    public abstract void Kill(Transform[] targets);

    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    protected abstract void Update();
}
