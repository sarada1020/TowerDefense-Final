using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoFantasma : InimigoBase
{
    public override void Start()
    {
        speed = 4f; // Define uma velocidade diferente para este inimigo
        base.Start(); // Chama o método Start da classe base
    }
}
