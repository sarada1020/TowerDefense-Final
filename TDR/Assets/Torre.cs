using System;
using UnityEngine;

// Classe que representa uma torre
[Serializable]
public class Torre
{
    public string nome; // Nome da torre
    public int custo; // Custo da torre
    public GameObject prefab; // Prefab da torre

    // Construtor da classe Torre
    public Torre(string _nome, int _custo, GameObject _prefab)
    {
        nome = _nome; // Inicializa o nome da torre
        custo = _custo; // Inicializa o custo da torre   
        prefab = _prefab; // Inicializa o prefab da torre
    }
}
