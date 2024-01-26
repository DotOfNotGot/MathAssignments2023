using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AABBCollider : MonoBehaviour
{
    public Bounds MeshBounds { get; private set; }
    
    [SerializeField] private Color debugColor;

    public event Action<AABBCollider> OnCollision;
    
    // Start is called before the first frame update
    void Awake()
    {
        var tempBounds = GetComponent<MeshRenderer>().localBounds;

        tempBounds.size = transform.lossyScale;
        tempBounds.center = transform.position;
        
        MeshBounds = tempBounds;
    }

    private void Update()
    {
        var tempBounds = MeshBounds;

        tempBounds.size = transform.lossyScale;
        tempBounds.center = transform.position;
        
        MeshBounds = tempBounds;
    }

    public void InvokeCollision(AABBCollider other)
    {
        OnCollision?.Invoke(other);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawCube(MeshBounds.center, MeshBounds.size);
    }
    
}
