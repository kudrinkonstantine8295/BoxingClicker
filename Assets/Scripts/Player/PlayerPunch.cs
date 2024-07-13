using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private DamagePopupController _damagePopupController;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out PunchZone punchZone))
                {
                    var punchData = _playerManager.MakePunch(punchZone);
                    _damagePopupController.CreateDamagePopup(hit, punchData);
                }
            }
        }
    }
}
