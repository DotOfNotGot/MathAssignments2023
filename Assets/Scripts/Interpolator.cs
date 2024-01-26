using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolator : MonoBehaviour
{

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private float speed = 1.0f;
    
    private float _t = 0.0f;

    private bool _isAdditive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition += transform.position;
        endPosition += transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAdditive)
        {
            _t += Time.deltaTime * speed;
        }
        else
        {
            _t -= Time.deltaTime * speed;
        }

        if (_t > 1.0f)
        {
            _t = 1.0f;
            _isAdditive = false;
        }
        else if (_t < 0.0f)
        {
            _t = 0.0f;
            _isAdditive = true;
        }

        transform.position = Vector3.Lerp(startPosition, endPosition, EaseOutBounce(_t));
    }

    private float EaseOutBounce(float t)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1 / d1)
        {
            return n1 * t * t;
        }
        else if (t < 2 / d1)
        {
            return n1 * (t -= 1.5f / d1) * t + 0.75f;
        }
        else if (t < 2.5f / d1)
        {
            return n1 * (t -= 2.25f / d1) * t + 0.9375f;
        }
        else
        {
            return n1 * (t -= 2.625f / d1) * t + 0.984375f;
        }
    }
}


