using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Progress _progress;
    [SerializeField] private GameObject _clickEffectPrefab;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Shaker _shaker;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<ClickZone>())
                {
                    _progress.AddClick();
                    GameObject newPrefab = ObjectPoolManager.SpawnObject(_clickEffectPrefab, hit.point, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);
                    newPrefab.TryGetComponent(out ClickEffect clickEffect);

                    if (clickEffect != null)
                        clickEffect.Setup(_progress.CoinsPerCLick);

                    _shaker.Shake();
                    _levelManager.AddClick();
                }
            }
        }
    }
}
