using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _intensity = 3f;
    [SerializeField] private float _speed = 5f;

    private Vector3 _startPosition;
    private float _timer = 0f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void StartShaking()
    {
        _timer = _duration;
    }

    private void Shake()
    {
        if (_timer > 0f)
        {
            transform.position = _startPosition + Random.insideUnitSphere * _intensity;
            _timer -= Time.deltaTime * _speed;

            if (_timer <= 0f)
            {
                _timer = 0f;
                transform.position = _startPosition;
            }
        }
    }

    private void Update()
    {
        Shake();
    }
}
