using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDead : MonoBehaviour
{
    private MeshRenderer _thisRenderer;
    private float mult = 3;

    private void Start()
    {
        _thisRenderer = GetComponent<MeshRenderer>();
    }

    private void YouDeadTest()
    {

        _thisRenderer.material.color = new Color(Random.value* mult, Random.value * mult, Random.value * mult);
    }
}
