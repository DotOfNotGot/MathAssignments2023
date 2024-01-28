using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStateHandler : MonoBehaviour
{
    enum PositionState
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [SerializeField] private Transform comparisonTransform;
    
    private PositionState _positionState;

    private Material _objectMat;
    
    private float _t = 0.0f;

    private bool _isAdditive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _objectMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _positionState = CheckPositionState(comparisonTransform);

        Color currentColor = _objectMat.color;
        
        if (_isAdditive)
        {
            _t += Time.deltaTime;
        }
        else
        {
            _t -= Time.deltaTime;
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
        
        switch (_positionState)
        {
            case PositionState.TopLeft:
                _objectMat.color = new Color(Interpolator.EaseInSine(_t), currentColor.g, currentColor.b, currentColor.a);
                break;
            case PositionState.TopRight:
                _objectMat.color = new Color(currentColor.r, Interpolator.EaseInSine(_t), currentColor.b, currentColor.a);
                break;
            case PositionState.BottomLeft:
                _objectMat.color = new Color(currentColor.r, currentColor.g, Interpolator.EaseInSine(_t), currentColor.a);
                break;
            case PositionState.BottomRight:
                _objectMat.color = new Color(Interpolator.EaseInSine(_t), Interpolator.EaseInSine(_t), Interpolator.EaseInSine(_t), Interpolator.EaseInSine(_t));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private PositionState CheckPositionState(Transform other)
    {
        Vector3 aPos = transform.position;
        Vector3 bPos = other.position;
        
        if (aPos.x < bPos.x && aPos.z > bPos.z) return PositionState.TopLeft;
        
        if (aPos.x > bPos.x && aPos.z > bPos.z) return PositionState.TopRight;

        if (aPos.x < bPos.x && aPos.z < bPos.z) return PositionState.BottomLeft;

        return PositionState.BottomRight;
    }
}
