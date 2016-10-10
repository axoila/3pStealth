using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCone
{
    private readonly Transform _detectorTransform;
    private readonly float _detectorRadius;
    private readonly float _detectorAngle;
    private readonly LayerMask _obstacleMask;

    public DetectorCone(Transform detectorTransform, float detectorRadius, float detectorAngle)
    {
        _detectorTransform = detectorTransform;
        _detectorRadius = detectorRadius;
        _detectorAngle = detectorAngle;
        //_obstacleMask=LayerMask.NameToLayer("Obstacle");
        _obstacleMask = 8;
    }

    public bool IsPlayerInRange()
    {
        throw new NotImplementedException();
    }

    public Transform[] GetPlayerTransformsInRange()
    {
        
        var colliders = Physics.OverlapSphere(_detectorTransform.position, _detectorRadius);
        var players = colliders.Where(x => x.CompareTag("Player")).Select(x => x.transform).ToArray();
        var results = new List<Transform>();

        for (var i = 0; i < players.Length; i++)
        {
            var player = players[i].transform;
            var enemy = _detectorTransform;
            var direction = (player.position - enemy.position).normalized;
            if (Vector3.Angle(enemy.forward, direction) < _detectorAngle / 2)
            {
                var distance = Vector3.Distance(enemy.position, player.position);

                // TODO FIX THIS
                //doesn't work, should work
                if (!Physics.Raycast(enemy.position, direction, distance, _obstacleMask))
                {
                    results.Add(player);
                }

                //RaycastHit hit;
                //Ray ray = new Ray(enemy.position, direction);
                //if (Physics.Raycast(ray, out hit, distance))
                //{
                //    if(hit.collider.CompareTag("Player"))results.Add(player);
                //}
            }
        }

        return results.ToArray();
    }

    public void DrawRangeIndicator()
    {
        var leftHandle = Quaternion.Euler(0, _detectorAngle / 2, 0) * (_detectorTransform.forward * _detectorRadius) + _detectorTransform.position;
        var rightHandle = Quaternion.Euler(0, -_detectorAngle / 2, 0) * (_detectorTransform.forward * _detectorRadius) + _detectorTransform.position;

        Debug.DrawLine(_detectorTransform.position, leftHandle, Color.blue);
        Debug.DrawLine(_detectorTransform.position, rightHandle, Color.blue);
    }
}
