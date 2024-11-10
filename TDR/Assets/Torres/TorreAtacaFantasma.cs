using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreAtacaFantasma : TorreBase
{
    // A torre normal usará o bulletPrefab da classe base
    // O método Shoot permanece o mesmo, pois já está implementado na classe base

    protected override void Shoot()
    {
        // Verifica se há um alvo antes de disparar
        if (target != null)
        {
            // Instancia um projétil para o alvo
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Cria um novo projétil
            Tiro bulletScript = bulletObj.GetComponent<Tiro>(); // Obtém o script do projétil
            bulletScript.SetTarget(target); // Define o alvo do projétil
        }
    }
}
