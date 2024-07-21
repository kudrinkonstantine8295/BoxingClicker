using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PunchZoneManager
{
    private List<PunchZone> _punchZones = new();

    public PunchData GetPunchData(PunchZone punchedZone, SaveLoadData saveLoadData, bool isPunchZonesRefreshNeeded)
    {
        PunchType punchType;
        float damage;
        int randomNumber;

        if(isPunchZonesRefreshNeeded)
            _punchZones.Clear();

        PunchZone punchZone;

        if (_punchZones.Count == 0)
        {
            _punchZones.Add(punchedZone);
        }
        else
        {
            punchZone = _punchZones.FirstOrDefault(z => z == punchedZone);

            if (punchZone == null)
                _punchZones.Add(punchedZone);
        }

        var biggestDamageZone = _punchZones.OrderByDescending(z => z.Points).First();

        damage = Random.Range(saveLoadData.MinDamage, saveLoadData.MaxDamage);

        punchType = PunchType.Usual;
        randomNumber = Random.Range(0, 100);

        if (randomNumber < saveLoadData.CritChancePercentage)
        {
            damage *= saveLoadData.CritMultiplier;
            punchType = PunchType.Crit;

            randomNumber = Random.Range(0, 100);

            if (randomNumber < saveLoadData.UltraCritChancePercentage)
            {
                damage *= saveLoadData.UltraCritMultiplier;
                punchType = PunchType.UltraCrit;
            }
        }

        float roundedDamage = Mathf.Round(damage);
        punchedZone.AddPoints(roundedDamage);
        PunchData punchData = new(roundedDamage, punchType, punchedZone.ZoneType);

        return punchData;
    }
}
