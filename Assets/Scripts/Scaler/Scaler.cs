using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _speed = 5f;

    private Vector3 _startScale;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void StartScaling()
    {
        transform.localScale = _targetScale;
    }

    private void Scale()
    {
        if (_startScale != transform.localScale)
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale, _speed*Time.deltaTime);
    }

    private void Update()
    {
        Scale();
    }
}
