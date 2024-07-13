using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

[Serializable]
public struct ZoneAngle
{
    public ZoneType ZoneType;
    public float Angle;
}

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _deactivateSeconds = 5f;
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] float _minDistance = 2f;
    [SerializeField] private float _maxDistance = 3f;
    [SerializeField] private float _angleSpread = 10f;
    [SerializeField] private List<ZoneAngle> _zonesAngle;

    private bool _isAnimationStarted = false;
    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private float _timer = 0f;
    private float _scaleMultiplier = 1f;

    private void Update()
    {
        ChangeAnimationValues();

        if (_timer > _deactivateSeconds)
        {
            ResetAnimation();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    public void StartAnimation(double value, ZoneType zoneType)
    {
        ZoneAngle zoneAngle = _zonesAngle.FirstOrDefault(z => z.ZoneType == zoneType);

        _text.text = Formater.Format(value);
        float direction = UnityEngine.Random.Range(zoneAngle.Angle - _angleSpread, zoneAngle.Angle + _angleSpread);
        _initialPosition = transform.position;
        float distance = UnityEngine.Random.Range(_minDistance, _maxDistance);
        _targetPosition = _initialPosition + Quaternion.Euler(0f, 0f, direction) * new Vector3(distance, distance, 0f);
        _isAnimationStarted = true;
    }

    private void ChangeAnimationValues()
    {
        if (_isAnimationStarted == false)
            return;

        _timer += Time.deltaTime;
        _scaleMultiplier = _scaleCurve.Evaluate(_timer / _deactivateSeconds);
        transform.localScale = Vector3.one * _scaleMultiplier;
        _text.alpha = _alphaCurve.Evaluate(_timer / _deactivateSeconds);
        transform.position = Vector3.Lerp(_initialPosition, _targetPosition, _timer / _deactivateSeconds);
    }

    private void ResetAnimation()
    {
        _text.text = null;
        _initialPosition = Vector3.zero;
        _targetPosition = Vector3.zero;
        _isAnimationStarted = false;
        _timer = 0f;
        _scaleMultiplier = 1f;
    }
}
