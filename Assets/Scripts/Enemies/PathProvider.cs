using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class Path
{
    //Set them in unity
    public Transform[] nodes;
}

public class PathProvider : MonoBehaviour
{
    public Transform[] defaultPath;

    [SerializeField]
    private Path[] _paths;
    private enum Patrol { Path1, Path2}
    private static PathProvider _instance;
    private static Transform[] _defaultPath;

    private void Awake()
    {
        Debug.Assert(_instance == null, "Shush. Don't make multiple instances of this, it's confusing.");
        _instance = this;
        _defaultPath = defaultPath ?? new Transform[] {new GameObject().transform};
    }

    public static PathProvider GetInstance()
    {
        return _instance;
    }

    public Transform[] GetPathOrDefault(int pathIndex)
    {
        if (_paths == null)
        {
            Debug.Log("No paths existing, returning default path");
            return _defaultPath;
        }

        switch (pathIndex)
        {
            case 0:
                return _paths[(int)Patrol.Path1].nodes;
            case 1:
                return _paths[(int)Patrol.Path2].nodes;
            default:
                return _defaultPath;
        }
    }
}
