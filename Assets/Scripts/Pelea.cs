using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que carga la pantalla de combate
/// </summary>
public class Pelea : MonoBehaviour
{
    [SerializeField] private GameObject dragon;

/// <summary>
/// Función que carga la pantalla de combate
/// </summary>
    public void AlCombate() {
        PlayerPrefs.SetString("EscenaAnterior", SceneManager.GetActiveScene().name);
        DontDestroyOnLoad(dragon);
        if (PlayerPrefs.GetString("EscenaAnterior") == "Hielo") {
            PlayerPrefs.SetString("HieloDerrotado", "true");
        }
        if (PlayerPrefs.GetString("EscenaAnterior") == "Ruinas") {
            PlayerPrefs.SetString("RuinasDerrotado", "true");
        }
        SceneManager.LoadScene("Combate");

    }
}
