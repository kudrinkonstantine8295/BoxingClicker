using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EmergencyLightStageType
{
    Disabled,
    Delay,
    Active
}

[Serializable]
public struct EmergencyLightStage
{
    public EmergencyLightStageType EmergencyLightStageType;
    public float ActiveTime;
    public float Transparency;
}

public class EmergencyLight : MonoBehaviour
{
    [SerializeField] private List<EmergencyLightStage> _stages;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Transform _pathwayPoint1;
    [SerializeField] private Transform _pathwayPoint2;
    [SerializeField] private Transform _movableLightPart;
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _speedMultiplier = 1.5f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _timerText;

    private float _timer = 0f;
    private Vector3 _targetPathwayPoint;
    private float _maxDistance = 0f;
    private float _speed = 1f;
    private float _maxDeviationAllowed = 0.3f;
    private bool _isTimerStopped = false;

    private int _currentStageIndex = 0;

    public void InitializeNewLight(float maxDeviationAllowed)
    {
        _maxDeviationAllowed = maxDeviationAllowed;
        _targetPathwayPoint = _pathwayPoint1.position;
        _maxDistance = Vector3.Distance(_pathwayPoint1.position, _pathwayPoint2.position);
        _speed = _startSpeed;
        InitializeStage();
    }

    private void InitializeStage()
    {
        _timer = _stages[_currentStageIndex].ActiveTime;
        _canvasGroup.alpha = _stages[_currentStageIndex].Transparency;
    }

    public void Stop()
    {
        _isTimerStopped = true;
    }

    public void Resume()
    {
        _isTimerStopped = false;
    }

    public void SelectPosition()
    {
        if (_stages[_currentStageIndex].EmergencyLightStageType == EmergencyLightStageType.Active)
        {
            float distance = Vector3.Distance(_movableLightPart.transform.position, Vector3.Lerp(_pathwayPoint1.position, _pathwayPoint2.position, 0.5f));
            float relativeDeviation = distance / _maxDistance;

            if (relativeDeviation < _maxDeviationAllowed)
                _speed *= _speedMultiplier;
            else
                _playerManager.ActivateEvasion();

            StepToNextStage();
            InitializeStage();
        }
    }

    private void ChangePosition()
    {
        _movableLightPart.position = Vector3.MoveTowards(_movableLightPart.position, _targetPathwayPoint, Time.deltaTime * _speed);

        if (_movableLightPart.position == _targetPathwayPoint)
        {
            if (_targetPathwayPoint == _pathwayPoint1.position)
                _targetPathwayPoint = _pathwayPoint2.position;
            else
                _targetPathwayPoint = _pathwayPoint1.position;
        }
    }

    private void RefreshTimer()
    {
        if (_isTimerStopped)
            return;

        if (_timer != 0f)
        {
            _timer -= Time.deltaTime;
            _timerText.text = ((int)_timer + 1).ToString();

            if (_timer <= 0f)
            {
                MakeStageAction(_stages[_currentStageIndex].EmergencyLightStageType);
                StepToNextStage();
                InitializeStage();
            }
        }
    }

    private void StepToNextStage()
    {
        _currentStageIndex++;

        if (_currentStageIndex >= _stages.Count)
            _currentStageIndex = 0;
    }

    private void MakeStageAction(EmergencyLightStageType stageType)
    {
        if (stageType == EmergencyLightStageType.Active)
            _playerManager.PrepareToTakePunch();
    }

    private void Update()
    {
        ChangePosition();
        RefreshTimer();
    }
}
