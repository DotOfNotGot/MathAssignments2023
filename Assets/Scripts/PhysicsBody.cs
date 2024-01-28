using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AABBCollider))]
public class PhysicsBody : MonoBehaviour
{
    private AABBCollider _collider;

    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private Vector3 _dir = Vector3.forward;

    [SerializeField]private Vector3 _tempGizmoDebugNormal = Vector3.zero;
    [SerializeField]private Vector3 _tempGizmoDebugCenter = Vector3.zero;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<AABBCollider>();
        _collider.OnCollision += HandleCollision;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _dir * (_speed * Time.deltaTime);
    }

    private void HandleCollision(AABBCollider other)
    {
        Bounds aBounds = _collider.MeshBounds;
        Bounds bBounds = other.MeshBounds;

        Vector3 distances1 = bBounds.min - aBounds.max;
        Vector3 distances2 = aBounds.min - bBounds.max;

        Vector3 distances = Vector3.Max(distances1, distances2);
        float maxDistance = distances.x;

        if (distances.y > maxDistance)
        {
            maxDistance = distances.y;
        }

        if (distances.z > maxDistance)
        {
            maxDistance = distances.z;
        }

        if (maxDistance < 0)
        {
            transform.position += _dir * maxDistance;
        }
        else
        {
            return;
        }

        _speed = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (_dir * _speed));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_tempGizmoDebugCenter, _tempGizmoDebugNormal * 10.0f);
    }

    private void OnDestroy()
    {
        _collider.OnCollision -= HandleCollision;
    }
}