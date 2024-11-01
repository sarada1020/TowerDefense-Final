using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer; // Renderer do sprite do bloco
    [SerializeField] Color hoverColor; // Cor do bloco ao passar o mouse

    GameObject tower; // Referência à torre construída
    Color startColor; // Cor original do bloco

    private void Start()
    {
        startColor = spriteRenderer.color; // Armazena a cor original
    }

}
