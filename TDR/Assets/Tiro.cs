using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    [SerializeField] private float velocidadeBala = 5f; // Velocidade da bala
    [SerializeField] private int dano = 1; // Dano que a bala causa

    private Transform alvo; // O alvo da bala

    // Método para definir o alvo da bala
    public void SetTarget(Transform _alvo)
    {
        alvo = _alvo; // Atribui o alvo
    }

    private void FixedUpdate()
    {
        // Verifica se o alvo ainda existe
        if (!alvo)
        {
            Destroy(gameObject); // Destroi a bala se não houver alvo
            return;
        }

        // Calcula a direção em que a bala deve se mover
        Vector2 direcao = (alvo.position - transform.position).normalized;
        transform.position += (Vector3)direcao * velocidadeBala * Time.fixedDeltaTime; // Move a bala em direção ao alvo
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Tenta obter o componente IReceberDano do objeto colidido
        IReceberDano danavel = colisao.gameObject.GetComponent<IReceberDano>();

        // Se o objeto colidido pode receber dano
        if (danavel != null)
        {
            danavel.TakeDamage(dano); // Aplica dano ao objeto
            Destroy(gameObject); // Destroi a bala após causar dano
        }
    }
}
