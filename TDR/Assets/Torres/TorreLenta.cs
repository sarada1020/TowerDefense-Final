using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TorreLenta : TorreBase
{
    [SerializeField] private float slowAmount = 0.99f; // Porcentagem de desaceleração (0 a 1)
    [SerializeField] private float slowRadius = 3f; // Raio de desaceleração
    [SerializeField] private float slowDuration = 2f; // Duração do efeito de desaceleração

    private HashSet<InimigoBase> enemiesInSlowZone = new HashSet<InimigoBase>();
    private Dictionary<InimigoBase, float> originalSpeeds = new Dictionary<InimigoBase, float>();

    private void Update()
    {
        // Adiciona inimigos que entram na zona de desaceleração
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slowRadius, enemyMask);
        foreach (Collider2D collider in colliders)
        {
            InimigoBase enemy = collider.GetComponent<InimigoBase>();
            if (enemy != null && !enemiesInSlowZone.Contains(enemy))
            {
                enemiesInSlowZone.Add(enemy);
                originalSpeeds[enemy] = enemy.GetSpeed(); // Armazena a velocidade original
                enemy.SetSpeed(enemy.GetSpeed() * slowAmount); // Aplica desaceleração imediata
            }
        }

        // Remove inimigos que saíram da zona de desaceleração
        enemiesInSlowZone.RemoveWhere(enemy =>
        {
            bool isOutsideRange = enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > slowRadius;
            if (isOutsideRange && enemy != null)
            {
                // Restaura a velocidade original do inimigo
                enemy.SetSpeed(originalSpeeds[enemy]);
                originalSpeeds.Remove(enemy); // Remove do dicionário de velocidades originais
            }
            return isOutsideRange;
        });
    }




    private IEnumerator ApplySlow(InimigoBase enemy)
    {
        Debug.Log("Aplicando desaceleração para: " + enemy.name); // Log para verificar se o método é chamado
        enemy.SetSpeed(enemy.GetSpeed() * slowAmount);
        yield return new WaitForSeconds(slowDuration);
        enemy.SetSpeed(enemy.GetSpeed() / slowAmount);
        enemiesInSlowZone.Remove(enemy); // Remove do conjunto de inimigos desacelerados
    }

}
