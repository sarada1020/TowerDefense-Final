using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [Header("Configurações de Spawn de Inimigos")]
    [SerializeField] List<GameObject> prefabInimigo;
    [SerializeField] int inimigosBase = 8;
    [SerializeField] float inimigosPorSegundo = 0.5f;
    [SerializeField] float tempoEntreOndas = 5f;
    [SerializeField] float dificuldadeEscalonamento = 0.75f;

    [Header("Configurações de Pontos")]
    public Transform pontoInicial;
    public Transform[] pontos;

    [Header("Configurações de Moeda")]
    public int currency = 100;

    int ondaAtual = 1;
    float tempoDesdeUltimoSpawn;
    int inimigosVivos;
    int inimigosRestantesParaSpawn;
    bool isSpawning = false;

    private bool interstitialPodePular = true;

    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private List<GameObject> inimigosAtivos = new List<GameObject>();

    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(IniciarOnda());
    }

    private void Update()
    {
        if (!isSpawning) return;

        tempoDesdeUltimoSpawn += Time.deltaTime;

        if (tempoDesdeUltimoSpawn >= (1f / inimigosPorSegundo) && inimigosRestantesParaSpawn > 0)
        {
            SpawnarInimigo();
            inimigosRestantesParaSpawn--;
            inimigosVivos++;
            tempoDesdeUltimoSpawn = 0f;
        }

        if (inimigosVivos == 0 && inimigosRestantesParaSpawn == 0)
        {
            TerminarOnda();
        }
    }

    private void EnemyDestroyed()
    {
        inimigosVivos--;
    }

    void SpawnarInimigo()
    {
        int randomIndex = Random.Range(0, prefabInimigo.Count);
        GameObject prefabParaSpawnar = prefabInimigo[randomIndex];
        GameObject novoInimigo = Instantiate(prefabParaSpawnar, pontoInicial.position, Quaternion.identity);
        inimigosAtivos.Add(novoInimigo);
    }

    IEnumerator IniciarOnda()
    {
        yield return new WaitForSeconds(tempoEntreOndas);

        AdsManager.main.Interstitial(interstitialPodePular);

        while (AdsManager.main.exibindoIntersticial)
        {
            yield return null;
        }

        interstitialPodePular = !interstitialPodePular;

        isSpawning = true;
        inimigosRestantesParaSpawn = CalcularInimigosPorOnda();
    }

    int CalcularInimigosPorOnda()
    {
        return Mathf.RoundToInt(inimigosBase * Mathf.Pow(ondaAtual, dificuldadeEscalonamento));
    }

    void TerminarOnda()
    {
        isSpawning = false;
        tempoDesdeUltimoSpawn = 0f;
        ondaAtual++;
        StartCoroutine(IniciarOnda());
    }

    public void AumentarMoeda(int quantia)
    {
        currency += quantia;
    }

    public bool GastarMoeda(int quantia)
    {
        if (quantia <= currency)
        {
            currency -= quantia;
            return true;
        }
        else
        {
            Debug.Log("Você não tem moedas suficientes para essa compra.");
            return false;
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        inimigosAtivos.Remove(enemy);
    }
}
