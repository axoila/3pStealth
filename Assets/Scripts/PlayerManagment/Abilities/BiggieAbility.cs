using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Abilities/Biggie", fileName ="BiggieAbility")]
public class BiggieAbility : Ability
{
    public float radius;
    public float force;
    public float yForceScale;
    public override void Activate(GameObject activator)
    {
        Collider[] cols = Physics.OverlapSphere(activator.transform.position, radius);

        foreach(Collider col in cols)
        {
            if(col.CompareTag("Player"))
                if(col.gameObject!=activator)
                {
                    col.GetComponent<Rigidbody>().AddForce((col.gameObject.transform.position - activator.transform.position + Vector3.up*yForceScale).normalized * force);
                }
        }
    }
}
