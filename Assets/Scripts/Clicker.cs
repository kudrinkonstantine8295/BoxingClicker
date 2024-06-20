using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Progress _progress;
    [SerializeField] private ClickEffect _clickEffectPrefab;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<ClickZone>())
                {
                    _progress.AddClick();

                   ClickEffect newClickEffect= Instantiate(_clickEffectPrefab, hit.point, Quaternion.identity);
                    newClickEffect.Setup(_progress.CoinsPerCLick);
                }
            }
        }
    }
}
