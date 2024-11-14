using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicio : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Game");
    }
}
