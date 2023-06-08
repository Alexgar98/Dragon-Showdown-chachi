using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla un texto que hace scroll hacia arriba
/// <param name="velocidad">Velocidad a la que se mueve el texto</param>
/// <param name="creditsTransform">Posición del texto</param>
/// </summary>
public class Scroll : MonoBehaviour
{
    public float velocidad = 50f;
    private RectTransform creditsTransform;
    
/// <summary>
/// Coge la posición en la que se encuentra el texto al principio
/// </summary>
    void Start()
    {
        creditsTransform = GetComponent<RectTransform>();
    }

/// <summary>
/// Sube el texto a la velocidad indicada y, cuando llega a una posición arbitraria, carga la pantalla del título
/// </summary>
    void Update()
    {
        creditsTransform.Translate(Vector2.up * velocidad * Time.deltaTime);
        if (creditsTransform.anchoredPosition.y >= (Screen.height + 700)) {
            SceneManager.LoadScene("Titulo");
        }
    }
}
