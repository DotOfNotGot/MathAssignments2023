using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainNoise : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;

        var meshVerts = mesh.vertices;

        float randomVal = Random.Range(0.5f, 1.5f);
        
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 oldVertPos = mesh.vertices[i];
            meshVerts[i] = new Vector3(mesh.vertices[i].x, mesh.vertices[i].y, Mathf.PerlinNoise((mesh.vertices[i].x + randomVal) * 1000, (mesh.vertices[i].y  + randomVal) * 1000) * 0.1f);
            Debug.Log($"Old Vert pos: {oldVertPos}, New Vert Pos: {meshVerts[i]}");
        }
        mesh.SetVertices(meshVerts);
        mesh.RecalculateNormals();
    }

}
