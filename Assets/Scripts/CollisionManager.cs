using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private List<AABBCollider> _collisionObjects;
    [SerializeField] private List<PhysicsBody> _physicsBodies;

    // Start is called before the first frame update
    void Awake()
    {
        _collisionObjects = new List<AABBCollider>();
        _physicsBodies = new List<PhysicsBody>();
        
        _collisionObjects.AddRange(FindObjectsOfType<AABBCollider>());
        _physicsBodies.AddRange(FindObjectsOfType<PhysicsBody>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < _collisionObjects.Count; i++)
        {
            for (int j = 0; j < _collisionObjects.Count; j++)
            {
                if (j == i || !Intersect(_collisionObjects[i], _collisionObjects[j])) continue;

                _collisionObjects[i].InvokeCollision(_collisionObjects[j]);
            }
        }
    }

    private bool Intersect(AABBCollider a, AABBCollider b)
    {
        var aBounds = a.MeshBounds;
        var bBounds = b.MeshBounds;

        return (aBounds.min.x <= bBounds.max.x &&
                aBounds.max.x >= bBounds.min.x &&
                aBounds.min.y <= bBounds.max.y &&
                aBounds.max.y >= bBounds.min.y &&
                aBounds.min.z <= bBounds.max.z &&
                aBounds.max.z >= bBounds.min.z);
    }
}