using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;
using TMPro;

///<summary>
/// Clase utilizada en la pantalla de inicio para pasar al overworld. Está pensada para mostrar un diálogo y resetear la base de datos, pero no funcionan estas dos cosas
/// <param name="botonInicio">Botón que da inicio al juego</param>
/// <param name="botonCreditos">Botón para cargar la pantalla de créditos</param>
/// <param name="pulsarTecla">Texto que parpadea en pantalla dando instrucciones para pulsar cualquier botón</param>
///</summary>
public class Empezar : MonoBehaviour
{
    public Button botonInicio;
    public Button botonCreditos;
    public TextMeshProUGUI pulsarTecla;
/// <summary>   
/// Nada más empezar llamo a QuitarBotones
/// </summary>
    void Start()
    {
        QuitarBotones();
        
    }

/// <summary>
/// Al pulsar cualquier tecla y mientras botonInicio no esté activo, llama a PonerBotones
/// </summary>
    void Update()
    {
        if (!botonInicio.gameObject.activeSelf) {
            if (Input.anyKey) {
            
            PonerBotones();
            }
        }
    }

/// <summary>
/// Función para manipular la base de datos. Primero resetea las tres tablas y luego genera un jugador con un monstruo y sus tres ataques
/// </summary>

    void conectar() {
        String conn = "URI=file:" + Application.dataPath + "/Plugins/Dragon Showdown db.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        
        //Reseteo todo
        String sqlQuery = "DELETE FROM Jugador";
        dbcmd.CommandText = sqlQuery;
        int escrituras = dbcmd.ExecuteNonQuery();
        sqlQuery = "DELETE FROM Monstruo";
        dbcmd.CommandText = sqlQuery;
        escrituras = dbcmd.ExecuteNonQuery();
        sqlQuery = "DELETE FROM Ataque";
        dbcmd.CommandText = sqlQuery;
        escrituras = dbcmd.ExecuteNonQuery();

        //Y ahora meto cosas

        sqlQuery = "INSERT INTO Ataque (Id, Nombre, Potencia, Precision) VALUES (1, \"Flying Kick\", 50, 100), (2, \"Heal\", 0, 100), (3, \"Hot Water\", 70, 100)";
        dbcmd.CommandText = sqlQuery;
        escrituras = dbcmd.ExecuteNonQuery();

        sqlQuery = "INSERT INTO Monstruo (Id, Nombre, Nivel, HP, HP_Actual, Ataque, Defensa, Velocidad, Inteligencia, Cordura, Atributo, Elemento, Id_AtaqueFisico, Id_AtaqueEstado, Id_AtaqueElemental) VALUES (1, \"Pepito\", 5, 120, 120, 35, 25, 60, 30, 60, \"Mar\", \"Hielo\", 1, 2, 3)";
        dbcmd.CommandText = sqlQuery;
        escrituras = dbcmd.ExecuteNonQuery();

        sqlQuery = "INSERT INTO Jugador (Nombre, Dinero, Id_Monstruo1) VALUES (\"Pruebito\", 5000, 1)";
        dbcmd.CommandText = sqlQuery;
        escrituras = dbcmd.ExecuteNonQuery();

        //Tiene que meter al personaje con el inicial y sus ataques

        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }

/// <summary>   
/// Setea varios PlayerPrefs que se necesitan en las mazmorras a "false", llama a conectar() y carga la escena del cuestionario
/// </summary>
    void EmpezarJuego() {
        PlayerPrefs.SetString("HieloDerrotado", "false");
        PlayerPrefs.SetString("RuinasDerrotado", "false");
        PlayerPrefs.SetString("HieloTerminado", "false");
        PlayerPrefs.SetString("RuinasTerminado", "false");
        conectar();
        SceneManager.LoadScene("Cuestionario");
    }

/// <summary>   
/// Carga la escena de los créditos
/// </summary>
    void CargarCreditos() {
        SceneManager.LoadScene("Creditos");
    }

/// <summary>   
/// Desactiva los botones para comenzar el juego o ir a los créditos, y activa el pulsarTecla
/// </summary>
    void QuitarBotones() {
        botonInicio.gameObject.SetActive(false);
        botonCreditos.gameObject.SetActive(false);
        pulsarTecla.gameObject.SetActive(true);
    }

/// <summary>   
/// Desactiva pulsarTecla y activa los dos botones junto con los listener que llaman a EmpezarJuego o CargarCreditos
/// </summary>
    void PonerBotones() {
        botonInicio.gameObject.SetActive(true);
        botonCreditos.gameObject.SetActive(true);
        pulsarTecla.gameObject.SetActive(false);
        botonInicio.onClick.AddListener(EmpezarJuego);
        botonCreditos.onClick.AddListener(CargarCreditos);
    }
}
