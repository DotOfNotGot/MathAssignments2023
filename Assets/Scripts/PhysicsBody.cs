using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    private AABBCollider _collider;

    [SerializeField]private float _speed = 10.0f;
    [SerializeField]private Vector3 _dir = Vector3.forward;
    

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
        Vector3 velocity = _dir * _speed;
        Vector3 hitNormal;
        SweepBoxBox(_collider, other, velocity, out velocity, out hitNormal);

        _dir = Vector3.Reflect(_dir, hitNormal);
        
        Debug.Log(hitNormal);
    }
    
bool SweepBoxBox( AABBCollider a, AABBCollider b, Vector3 v, out Vector3 outVel, out Vector3 hitNormal )
{
    //Initialise out info
    outVel = v;
    hitNormal = Vector3.zero;


    v = -v;

    float hitTime = 0.0f;
    float outTime = 1.0f;
    Vector3 overlapTime = Vector3.zero;

    // X axis overlap
    if( v.x < 0 )
    {
        if( b.MeshBounds.max.x < a.MeshBounds.min.x ) return false;
        if( b.MeshBounds.max.x > a.MeshBounds.min.x ) outTime = Mathf.Min( (a.MeshBounds.min.x - b.MeshBounds.max.x) / v.x, outTime );

        if( a.MeshBounds.max.x < b.MeshBounds.min.x )
        {
            overlapTime.x = (a.MeshBounds.max.x - b.MeshBounds.min.x) / v.x;
            hitTime = Mathf.Max(overlapTime.x, hitTime);
        }
    }
    else if( v.x > 0 )
    {
        if( b.MeshBounds.min.x > a.MeshBounds.max.x ) return false;
        if( a.MeshBounds.max.x > b.MeshBounds.min.x ) outTime = Mathf.Min( (a.MeshBounds.max.x - b.MeshBounds.min.x) / v.x, outTime );

        if( b.MeshBounds.max.x < a.MeshBounds.min.x )
        {
            overlapTime.x = (a.MeshBounds.min.x - b.MeshBounds.max.x) / v.x;
            hitTime = Mathf.Max(overlapTime.x, hitTime);
        }
    }

    if( hitTime > outTime ) return false;

    // Y axis overlap
    if( v.y < 0 )
    {
        if( b.MeshBounds.max.y < a.MeshBounds.min.y ) return false;
        if( b.MeshBounds.max.y > a.MeshBounds.min.y ) outTime = Mathf.Min( (a.MeshBounds.min.y - b.MeshBounds.max.y) / v.y, outTime );

        if( a.MeshBounds.max.y < b.MeshBounds.min.y )
        {
            overlapTime.y = (a.MeshBounds.max.y - b.MeshBounds.min.y) / v.y;
            hitTime = Mathf.Max(overlapTime.y, hitTime);
        }           
    }
    else if( v.y > 0 )
    {
        if( b.MeshBounds.min.y > a.MeshBounds.max.y ) return false;
        if( a.MeshBounds.max.y > b.MeshBounds.min.y ) outTime = Mathf.Min( (a.MeshBounds.max.y - b.MeshBounds.min.y) / v.y, outTime );

        if( b.MeshBounds.max.y < a.MeshBounds.min.y )
        {
            overlapTime.y = (a.MeshBounds.min.y - b.MeshBounds.max.y) / v.y;
            hitTime = Mathf.Max(overlapTime.y, hitTime);
        }
    }
    
    // Z axis overlap
    if( v.z < 0 )
    {
        if( b.MeshBounds.max.z < a.MeshBounds.min.z ) return false;
        if( b.MeshBounds.max.z > a.MeshBounds.min.z ) outTime = Mathf.Min( (a.MeshBounds.min.z - b.MeshBounds.max.z) / v.z, outTime );

        if( a.MeshBounds.max.z < b.MeshBounds.min.z )
        {
            overlapTime.z = (a.MeshBounds.max.z - b.MeshBounds.min.z) / v.z;
            hitTime = Mathf.Max(overlapTime.z, hitTime);
        }           
    }
    else if( v.z > 0 )
    {
        if( b.MeshBounds.min.z > a.MeshBounds.max.z ) return false;
        if( a.MeshBounds.max.z > b.MeshBounds.min.z ) outTime = Mathf.Min( (a.MeshBounds.max.z - b.MeshBounds.min.z) / v.z, outTime );

        if( b.MeshBounds.max.z < a.MeshBounds.min.z )
        {
            overlapTime.z = (a.MeshBounds.min.z - b.MeshBounds.max.z) / v.z;
            hitTime = Mathf.Max(overlapTime.z, hitTime);
        }
    }

    if( hitTime > outTime ) return false;

    // Scale resulting velocity by normalized hit time
    outVel = -v * hitTime;

    // Hit normal is along axis with the highest overlap time
    if( overlapTime.x >= hitTime)
    {
        hitNormal = new Vector3(Mathf.Sign(v.x), 0, 0);
    }
    else if(overlapTime.y >= hitTime)
    {
        hitNormal = new Vector3(0, Mathf.Sign(v.y), 0);
    }
    else
    {
        hitNormal = new Vector3(0, 0, Mathf.Sign(v.z));
    }

    return true;
}

    private void OnDestroy()
    {
        _collider.OnCollision -= HandleCollision;
    }
}
