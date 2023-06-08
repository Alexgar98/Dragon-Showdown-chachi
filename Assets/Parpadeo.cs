using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase para poner y quitar repetidamente un texto
/// <param name="Empezar">Texto que se va a activar y desactivar</param>
/// </summary>
public class Parpadeo : MonoBehaviour
{
    public TextMeshProUGUI Empezar;

/// <summary>
/// Cada dos segundos llama a QuitarTexto y PonerTexto, de manera que van con un segundo de separación entre sí
/// </summary>
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("QuitarTexto", 1, 2);
        InvokeRepeating("PonerTexto", 2, 2);
    }

/// <summary>
/// Función que desactiva el texto
/// </summary>
    void QuitarTexto() {
        Empezar.enabled = false;
    }

/// <summary>
/// Función que activa el texto
/// </summary>
    void PonerTexto() {
        Empezar.enabled = true;
    }
}
