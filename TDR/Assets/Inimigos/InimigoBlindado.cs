using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBlindado : InimigoBase
{
    public override void TakeDamage(int damage)
    {
        int reducedDamage = Mathf.CeilToInt(damage * 0.5f); // Reduz o dano pela metade
        base.TakeDamage(reducedDamage);
    }
}

