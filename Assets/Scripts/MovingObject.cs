using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
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
            transform.position = Vector3.Lerp(startPosition, endPosition, Interpolator.EaseOutBounce(_t));
        }
        else
        {
            _t -= Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, Interpolator.EaseInElastic(_t));
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
    }
}
