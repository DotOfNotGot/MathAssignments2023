using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private Vector2 RandomGradient(int ix, int iy)
    {
        const uint w = 8 * sizeof(uint);
        const uint s = w / 2;
        uint a = (uint)ix;
        uint b = (uint)iy;
        a *= 3284157443;

    }
    
    
    private float DotGridGradient(int ix, int iy, float x, float y)
    {
        Vector2 gradient = random
    }
    
    float SamplePerlinNoise(float x, float y)
    {
        int x0 = (int)x;
        int y0 = (int)y;
        int x1 = x0 + 1;
        int y1 = y0 + 1;
        
        
        
    }
    

    public static Vector3[,] GeneratePerlinNoise(int width, int height, float noiseScale)
    {
        Vector3[,] noiseGrid = new Vector3[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseGrid[x, y] = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * noiseScale;
            }
        }
        
    }
    
}
