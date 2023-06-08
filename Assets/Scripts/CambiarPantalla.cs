using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que maneja los cambios entre pantallas
/// </summary>
public class CambiarPantalla : MonoBehaviour
{

/// <summary>
/// Función que cambia de pantalla; además guarda el GameObject del jugador y el layout de la caja de diálogo
/// <param name="pantallaDestino">Pantalla a la que se va</param>
/// <param name="pantallaOrigen">Pantalla de la que se va</param>
/// <param name="personaje">GameObject que representa el personaje del jugador</param>
/// <param name="dialogueUI">Caja de diálogo</param>
/// </summary>
    public static void cambiarAPantalla(String pantallaDestino, String pantallaOrigen, GameObject personaje, DialogueUI dialogueUI){
        
        DontDestroyOnLoad(personaje);
        DontDestroyOnLoad(dialogueUI);
        switch(""+pantallaOrigen+pantallaDestino){
            case "OverworldRuinas":
                SceneManager.LoadScene("Ruinas");
                personaje.transform.position=new Vector2(62,-20);
            break;
            case "RuinasOverworld":
                SceneManager.LoadScene("Overworld");
                personaje.transform.position=new Vector2(-0.3f,-5.6f);
            break;
            case "OverworldHielo":
                SceneManager.LoadScene("Hielo");
                personaje.transform.position=new Vector2(-50,-22);
            break;
            case "HieloOverworld":
                SceneManager.LoadScene("Overworld");
                personaje.transform.position=new Vector2(-11.2f,-25.5f);
            break;
        }
    }
}
