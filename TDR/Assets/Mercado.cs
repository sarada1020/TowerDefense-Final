using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mercado : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moedaUI; // UI que mostra a quantidade de moedas

    // Método chamado a cada frame para atualizar a UI
    private void OnGUI()
    {
        moedaUI.text = GameManager.main.currency.ToString(); // Atualiza o texto da UI com a quantidade atual de moedas
    }

    // Método para definir a torre selecionada (implementação a ser feita)
    public void SetSelectedTower()
    {
        // Implementação a ser feita para definir a torre selecionada
    }
}
