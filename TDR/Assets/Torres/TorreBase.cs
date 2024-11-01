using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Classe abstrata base para torres
public abstract class TorreBase : MonoBehaviour
{
    // A distância máxima que a torre pode atingir um inimigo
    [SerializeField] protected float targetingRange = 5f;

    // Máscara de camada que define quais objetos são considerados inimigos
    [SerializeField] protected LayerMask enemyMask;

    // Prefab do projétil que a torre irá disparar
    [SerializeField] protected GameObject bulletPrefab;

    // Ponto de onde os projéteis serão disparados
    [SerializeField] protected Transform firingPoint;

    // Número de disparos por segundo
    [SerializeField] protected float bps = 1f;

    // Transform do alvo atual que a torre está mirando
    protected Transform target;

    // Tempo até que a torre possa disparar novamente
    private float timeUntilFire;

    // Método chamado a cada frame
    private void Update()
    {
        // Se não houver alvo, procura um novo
        if (target == null)
        {
            FindTarget();
            return; // Se não há alvo, sai do método
        }

        // Verifica se o alvo está fora do alcance
        if (!CheckTargetIsInRange())
        {
            target = null; // Se o alvo está fora do alcance, reseta o alvo
        }
        else
        {
            // Acumula o tempo até o próximo disparo
            timeUntilFire += Time.deltaTime;

            // Verifica se é hora de disparar
            if (timeUntilFire >= 1f / bps)
            {
                Shoot(); // Dispara o projétil
                timeUntilFire = 0f; // Reseta o temporizador
            }
        }
    }

    // Método virtual para disparar, pode ser sobrescrito nas classes filhas
    protected virtual void Shoot()
    {
        // Instancia o projétil no ponto de disparo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        // Obtém o script do projétil para definir o alvo
        Tiro bulletScript = bulletObj.GetComponent<Tiro>();
        bulletScript.SetTarget(target); // Define o alvo do projétil
    }

    // Verifica se o alvo está dentro do alcance da torre
    protected bool CheckTargetIsInRange()
    {
        // Retorna verdadeiro se a distância até o alvo é menor ou igual ao alcance
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Método virtual para encontrar um alvo, pode ser sobrescrito nas classes filhas
    protected virtual void FindTarget()
    {
        // Realiza um raio em círculo para encontrar inimigos dentro do alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Se algum inimigo foi encontrado, define o primeiro como alvo
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    // Método para desenhar uma representação visual do alcance da torre no editor
    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue; // Define a cor do gizmo
        // Desenha um disco wireframe ao redor da torre para visualizar o alcance
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
