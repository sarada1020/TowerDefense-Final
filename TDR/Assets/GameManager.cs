using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main; // Instância única do GerenciadorJogo

    [Header("Configurações de Spawn de Inimigos")]
    [SerializeField] List<GameObject> prefabInimigo; // Lista de prefabs de inimigos
    [SerializeField] int inimigosBase = 8; // Número base de inimigos por onda
    [SerializeField] float inimigosPorSegundo = 0.5f; // Taxa de spawn de inimigos
    [SerializeField] float tempoEntreOndas = 5f; // Tempo entre ondas de inimigos
    [SerializeField] float dificuldadeEscalonamento = 0.75f; // Escalonamento da dificuldade

    [Header("Configurações de Pontos")]
    public Transform pontoInicial; // Ponto de spawn inicial dos inimigos
    public Transform[] pontos; // Array de pontos de spawn

    [Header("Configurações de Moeda")]
    public int currency = 100; // Quantidade inicial de moedas

    int ondaAtual = 1; // Onda atual de inimigos
    float tempoDesdeUltimoSpawn; // Tempo desde o último spawn de inimigos
    int inimigosVivos; // Número de inimigos vivos
    int inimigosRestantesParaSpawn; // Inimigos restantes para spawnar
    bool isSpawning = false; // Estado do spawn

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento acionado ao destruir um inimigo
    private List<GameObject> inimigosAtivos = new List<GameObject>(); // Lista para armazenar inimigos ativos

    private void Awake()
    {
        main = this; // Inicializa a instância única
        onEnemyDestroy.AddListener(EnemyDestroyed); // Adiciona listener ao evento de destruição de inimigo
    }

    private void Start()
    {
        StartCoroutine(IniciarOnda()); // Inicia a primeira onda de inimigos
    }

    private void Update()
    {
        if (!isSpawning) return; // Retorna se não estiver spawnando

        tempoDesdeUltimoSpawn += Time.deltaTime; // Atualiza o tempo desde o último spawn

        if (tempoDesdeUltimoSpawn >= (1f / inimigosPorSegundo) && inimigosRestantesParaSpawn > 0)
        {
            SpawnarInimigo(); // Spawn um novo inimigo
            inimigosRestantesParaSpawn--; // Reduz a quantidade de inimigos restantes para spawnar
            inimigosVivos++; // Incrementa o número de inimigos vivos
            tempoDesdeUltimoSpawn = 0f; // Reseta o tempo desde o último spawn
        }

        if (inimigosVivos == 0 && inimigosRestantesParaSpawn == 0)
        {
            TerminarOnda(); // Termina a onda se não houver inimigos vivos
        }
    }

}
