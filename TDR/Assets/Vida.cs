using UnityEngine;

public class Vida : MonoBehaviour, IReceberDano
{
    [SerializeField] int pontosDeVida = 2; // Pontos de vida do objeto
    [SerializeField] int valorEmMoeda = 50; // Valor em moeda ao ser destruído
    bool estaDestruido = false; // Indica se o objeto já foi destruído

    // Método para receber dano
    public void TakeDamage(int dano)
    {
        pontosDeVida -= dano; // Reduz os pontos de vida pelo dano recebido

        // Verifica se os pontos de vida são menores ou iguais a zero e se não foi destruído
        if (pontosDeVida <= 0 && !estaDestruido)
        {
            GameManager.onEnemyDestroy.Invoke(); // Invoca o evento de destruição de inimigo
            GameManager.main.AumentarMoeda(valorEmMoeda); // Aumenta a moeda do jogador
            estaDestruido = true; // Marca como destruído
            Destroy(gameObject); // Destroi o objeto
        }
    }
}
