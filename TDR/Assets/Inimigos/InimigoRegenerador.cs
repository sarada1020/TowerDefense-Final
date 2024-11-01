using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoRegenerador : InimigoBase
{
    [SerializeField] private float regenerationRate = 1f; // Pontos de vida regenerados por segundo

    public override void Update()
    {
        base.Update();
        RegenerateHealth();
    }

    private void RegenerateHealth()
    {
        hitPoints += Mathf.RoundToInt(regenerationRate * Time.deltaTime);
    }
}

