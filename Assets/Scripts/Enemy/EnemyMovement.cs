using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EnemyManager _enemyManager;

    [SerializeField] private List<Transform> _enemyPathwayPoints;

    [SerializeField] private Transform _enemyRightPathwayPoint;
    [SerializeField] private Transform _enemyLeftPathwayPoint;
    [SerializeField] private Transform _enemyMiddlePathwayPoint;

    [SerializeField] private float _changeTargetPathwayPointTime = 1f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _neglectDistanceToTarget = 1f;

    private Transform _targetPathwayPoint;
    private float _timer = 0f;
    private bool _isChangePositionNeeded = false;

    private void Start()
    {
        transform.position = _enemyMiddlePathwayPoint.position;
        ResumeRandomChangePosition();
        ChangeRandomTargetPathwayPoint();
    }

    private void ChangeRandomTargetPathwayPoint()
    {
        _targetPathwayPoint = _enemyPathwayPoints[Random.Range(0, _enemyPathwayPoints.Count)];
    }

    public void ResumeRandomChangePosition()
    {
        _isChangePositionNeeded = true;
    }

    public void KeepDirection(Direction direction)
    {
        _isChangePositionNeeded = false;

        if (direction == Direction.Left)
            _targetPathwayPoint = _enemyLeftPathwayPoint;
        else if (direction == Direction.Right)
            _targetPathwayPoint = _enemyRightPathwayPoint;
        else
            _targetPathwayPoint = _enemyMiddlePathwayPoint;

    }

    public bool CheckEnemyCloseToTarget()
    {
        return Vector3.Distance(_targetPathwayPoint.position, transform.position) < _neglectDistanceToTarget;
    }

    private void RefreshPosition()
    {
        _timer += Time.deltaTime;

        if (_timer > _changeTargetPathwayPointTime)
        {
            _timer = 0f;

            if (_isChangePositionNeeded == true)
                ChangeRandomTargetPathwayPoint();
        }

        transform.position = Vector3.Lerp(transform.position, _targetPathwayPoint.position, _speed * Time.deltaTime);
    }

    private void Update()
    {
        RefreshPosition();
    }
}
