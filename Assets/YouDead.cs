using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDead : MonoBehaviour
{
    private MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    void YouDeadTest()
    {
        renderer.material.color = Random.ColorHSV();
    }
}
