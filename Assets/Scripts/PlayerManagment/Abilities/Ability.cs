using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public int cooldown;

    public abstract void Activate(GameObject activator);
}
