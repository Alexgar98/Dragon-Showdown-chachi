using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class DestruirDragon : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;
    string escenaActual;
    
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable {get; set;}
    
    void Start()
    {
        escenaActual = SceneManager.GetActiveScene().name;
        
        
        if (escenaActual == "Hielo") {
            if (PlayerPrefs.GetString("HieloTerminado") == "true") {
            GameObject dragonDerrotado = GameObject.FindGameObjectWithTag("NPC");
                if (dragonDerrotado != null)
                    {
                        Destroy(dragonDerrotado);
                    }
                else
                    {
                    Debug.Log("No ha encontrado al dragón");
                }
                
            }
            else {
            if (PlayerPrefs.GetString("HieloDerrotado") == "true") {
                StartCoroutine(Interact());
                PlayerPrefs.SetString("HieloTerminado", "true");
            }
            }
        }

        if (escenaActual == "Ruinas") {
            if (PlayerPrefs.GetString("RuinasTerminado") == "true") {
            GameObject dragonDerrotado = GameObject.FindGameObjectWithTag("NPC");
                if (dragonDerrotado != null)
                    {
                        Destroy(dragonDerrotado);
                    }
                else
                    {
                    Debug.Log("No ha encontrado al dragón");
                }
                
            
            }
            else {
            if (PlayerPrefs.GetString("RuinasDerrotado") == "true") {
                StartCoroutine(Interact());
                PlayerPrefs.SetString("RuinasTerminado", "true");
            }
            }
        }
    }

    public void UpdateDialogueObject(DialogueObject dialogueObject) {
        this.dialogueObject = dialogueObject;
    }

    public IEnumerator Interact() {
        DialogueActivator dialogueActivator = GetComponent<DialogueActivator>();
                if (dialogueActivator != null)
                {
                    dialogueActivator.enabled = false;
                }
        this.DialogueUI.ShowDialogue(dialogueObject);
        yield return new WaitUntil(() => !this.DialogueUI.IsOpen);
        GameObject dragonDerrotado = GameObject.FindGameObjectWithTag("NPC");
                if (dragonDerrotado != null)
                    {
                        conectar();
                        Destroy(dragonDerrotado);
                    }
                else
                    {
                    Debug.Log("No ha encontrado al dragón");
                }
        yield return null;
    }

    void conectar() {
        String conn = "URI=file:" + Application.dataPath + "/Plugins/Dragon Showdown db.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        String sqlQuery = null;
        
        if (escenaActual == "Hielo") {
        sqlQuery = "INSERT INTO Monstruo (Id, Nombre, Nivel, HP, HP_Actual, Ataque, Defensa, Velocidad, Inteligencia, Cordura, Atributo, Elemento, Id_AtaqueFisico, Id_AtaqueEstado, Id_AtaqueElemental) VALUES (2, \"Paquito\", 5, 120, 120, 35, 25, 60, 30, 60, \"Tierra\", \"Fuego\", 1, 2, 3)";
        }
        if (escenaActual == "Ruinas") {
            sqlQuery = "INSERT INTO Monstruo (Id, Nombre, Nivel, HP, HP_Actual, Ataque, Defensa, Velocidad, Inteligencia, Cordura, Atributo, Elemento, Id_AtaqueFisico, Id_AtaqueEstado, Id_AtaqueElemental) VALUES (3, \"Juanito\", 5, 120, 120, 35, 25, 60, 30, 60, \"Aire\", \"Electrico\", 1, 2, 3)";
        }
        dbcmd.CommandText = sqlQuery;
        int escrituras = dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
