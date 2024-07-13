using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PunchZoneManager
{
    private List<PunchZone> _punchZones = new();

    public PunchData GetPunchData(PunchZone punchedZone, SaveLoadData saveLoadData, bool isPunchZonesRefreshNeeded)
    {
        bool isCritRestricted = false;
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

        var biggestDamageZone = _punchZones.OrderByDescending(z => z.DamageTaken).First();

        if (biggestDamageZone == punchedZone)
            isCritRestricted = true;

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

        //if (isCritRestricted)
        //    punchType = PunchType.Usual;

        float roundedDamage = Mathf.Round(damage);
        punchedZone.AddTakenDamage(roundedDamage);
        PunchData punchData = new PunchData(roundedDamage, punchType, punchedZone.ZoneType);

        return punchData;
    }
}
