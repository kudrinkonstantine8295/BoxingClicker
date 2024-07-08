using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemyPathwayPoints;
    [SerializeField] private Transform _mainPathwayPoint;
    [SerializeField] private float _changeTargetPathwayPointTime = 1f;
    [SerializeField] private float _speed = 10f;

    private Transform _targetPathwayPoint;
    private float _timer = 0f;

    private void Start()
    {
        transform.position = _mainPathwayPoint.position;
        ChangeTargetPathwayPoint();
    }

    private void ChangeTargetPathwayPoint()
    {
        _targetPathwayPoint = _enemyPathwayPoints[Random.Range(0, _enemyPathwayPoints.Count)];
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _changeTargetPathwayPointTime)
        {
            _timer = 0f;
            ChangeTargetPathwayPoint();
        }

        transform.position = Vector3.Lerp(transform.position, _targetPathwayPoint.position, _speed * Time.deltaTime);
    }

}
