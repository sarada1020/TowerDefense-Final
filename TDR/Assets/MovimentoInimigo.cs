using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigo : InimigoBase
{
    // Método chamado no início
    public override void Start()
    {
        base.Start(); // Chama o método Start da classe base
    }

    // Método chamado a cada frame
    public override void Update()
    {
        base.Update(); // Chama o método Update da classe base
    }

    // Método chamado a cada FixedFrame (usado para física)
    public override void FixedUpdate()
    {
        base.FixedUpdate(); // Chama o método FixedUpdate da classe base
    }
}
