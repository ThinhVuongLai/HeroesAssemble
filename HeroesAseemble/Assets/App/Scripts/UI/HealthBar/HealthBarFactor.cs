using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class HealthBarFactor
    {
        public IHealthBar CreateBar(HealthBarType healthBarType, Transform parent)
        {
            GameObject barPrefab = null;

            switch(healthBarType)
            {
                case HealthBarType.Health:
                    {
                        barPrefab = GlobalInfor.Instance.HeathBarPrefab;
                        var currentBar = Object.Instantiate(barPrefab, parent: parent);
                        return currentBar.GetComponent<HealthBar>();
                    }
                case HealthBarType.Strenght:
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }

    }

    public enum HealthBarType
    {
        Health,
        Strenght,
    }
}