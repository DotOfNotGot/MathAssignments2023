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
    
    public Vector3 Velocity => _speed * _dir;


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

        Vector3 prevPosition = transform.position + -_dir * (_speed * Time.deltaTime);

        Bounds prevPosBounds = new Bounds(prevPosition, _collider.MeshBounds.size);

        _dir = Vector3.Reflect(_dir, GetCollisionNormal(prevPosBounds, other.MeshBounds, Velocity * Time.deltaTime));
        
        // _speed = 0;
    }

    private Vector3 GetCollisionNormal(Bounds a, Bounds b, Vector3 velocity)
    {
        Vector3 dirFrac = Vector3.zero;

        Vector3 rayDir = velocity.normalized;
        
        dirFrac.x = 1.0f / rayDir.x;
        dirFrac.y = 1.0f / rayDir.y;
        dirFrac.z = 1.0f / rayDir.z;

        float t1 = (b.min.x - a.center.x) * dirFrac.x;
        float t2 = (b.max.x - a.center.x) * dirFrac.x;
        float t3 = (b.min.y - a.center.y) * dirFrac.y;
        float t4 = (b.max.y - a.center.y) * dirFrac.y;
        float t5 = (b.min.z - a.center.z) * dirFrac.z;
        float t6 = (b.max.z - a.center.z) * dirFrac.z;

        float tMin = Mathf.Max(Mathf.Max(Mathf.Min(t1, t2), Mathf.Min(t3, t4), Mathf.Min(t5, t6)));
        // float tMax = Mathf.Min(Mathf.Min(Mathf.Max(t1, t2), Mathf.Max(t3, t4), Mathf.Max(t5, t6)));

        float t;

        // t = tMin;
        if (tMin == t1) return Vector3.left;
        if (tMin == t2) return Vector3.right;
        if (tMin == t3) return Vector3.down;
        if (tMin == t4) return Vector3.up;
        if (tMin == t5) return Vector3.back;
        if (tMin == t6) return Vector3.forward;

        return Vector3.zero;
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