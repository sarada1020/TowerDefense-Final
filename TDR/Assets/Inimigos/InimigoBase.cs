using System.Collections;
using UnityEngine;

public class InimigoBase : MonoBehaviour, IReceberDano
{
    [SerializeField] protected float speed = 3f; // Velocidade padrão
    [SerializeField] protected int hitPoints = 2; // Pontos de vida
    [SerializeField] protected int currencyWorth = 50; // Valor em moeda ao ser destruído

    protected Rigidbody2D rigidbody;
    protected Transform target;
    protected int pontoIndex = 0;

    public virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameManager.main.pontos[pontoIndex];
    }

    public virtual void Update()
    {
        // Verifica se o inimigo chegou ao destino
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pontoIndex++;

            if (pontoIndex == GameManager.main.pontos.Length)
            {
                GameManager.onEnemyDestroy.Invoke();
                // Remove o inimigo da lista de inimigos ativos
                GameManager.main.RemoveEnemyFromList(gameObject);
                Destroy(gameObject);
                return;
            }
            else
            {
                target = GameManager.main.pontos[pontoIndex];
            }
        }
    }

    public virtual void FixedUpdate()
    {
        if (target != null) // Verifica se o target está definido
        {
            Vector2 direcao = (target.position - transform.position).normalized;
            rigidbody.velocity = direcao * speed;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            GameManager.onEnemyDestroy.Invoke();
            GameManager.main.AumentarMoeda(currencyWorth);
            // Remove o inimigo da lista de inimigos ativos
            GameManager.main.RemoveEnemyFromList(gameObject);
            Destroy(gameObject);
        }
    }

    // Método para alterar a velocidade do inimigo
    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Max(0, newSpeed);
        Debug.Log("Velocidade do inimigo alterada para: " + speed); // Verifique se a velocidade é alterada
        if (rigidbody != null)
        {
            Vector2 direcao = (target.position - transform.position).normalized;
            rigidbody.velocity = direcao * speed;
        }
    }

    // Método para obter a velocidade atual do inimigo
    public float GetSpeed()
    {
        return speed;
    }
}
