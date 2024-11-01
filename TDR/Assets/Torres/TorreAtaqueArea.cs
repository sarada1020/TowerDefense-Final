using UnityEngine;

public class TorreAtaqueArea : TorreBase
{
    [SerializeField] private GameObject fireBulletPrefab;

    // Novo campo para o raio de ataque
    [SerializeField] private float attackRadius = 3f;

    protected override void Shoot()
    {
        // Detecta todos os inimigos dentro do ataque
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(firingPoint.position, attackRadius, enemyMask);

        foreach (Collider2D enemy in enemiesHit)
        {
            // Instancia um projétil para cada inimigo detectado
            GameObject bulletObj = Instantiate(fireBulletPrefab, firingPoint.position, Quaternion.identity);
            Tiro bulletScript = bulletObj.GetComponent<Tiro>();
            bulletScript.SetTarget(enemy.transform);
        }
    }

    // Adiciona Gizmos para visualizar a área de ataque
    private void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected(); // Chama o método base para desenhar o alcance da torre
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firingPoint.position, attackRadius); // Desenha o raio de ataque
    }
}
