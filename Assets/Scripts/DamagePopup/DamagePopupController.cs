using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public struct DamagePopupType
{
    public PunchType PunchType;
    public GameObject DamagePopup;
}

public class DamagePopupController : MonoBehaviour
{
    [SerializeField] private List<DamagePopupType> _damagePopups = new();

    public void CreateDamagePopup(RaycastHit hit, PunchData punchData)
    {
        GameObject damagePopupBlueprint = _damagePopups.FirstOrDefault(d => d.PunchType == punchData.PunchType).DamagePopup;
        GameObject newDamagePopupPrefab = ObjectPoolManager.SpawnObject(damagePopupBlueprint, hit.point, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);

        if (newDamagePopupPrefab.TryGetComponent(out DamagePopup damagePopup))
            damagePopup.StartAnimation(punchData.Damage, punchData.ZoneType);
    }
}
