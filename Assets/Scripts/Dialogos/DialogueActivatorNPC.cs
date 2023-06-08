using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DialogueActivatorNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObjectFinal;
    [SerializeField] private DialogueObject dialogueObjectNoFinal;
    private bool tresDragones = false;

    public void UpdateDialogueObject(DialogueObject dialogueObjectFinal, DialogueObject dialogueObjectNoFinal) {
        this.dialogueObjectFinal = dialogueObjectFinal;
        this.dialogueObjectNoFinal = dialogueObjectNoFinal;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && other.TryGetComponent(out Jugador player)) {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && other.TryGetComponent(out Jugador player)) {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this) {
                player.Interactable = null;
            }
        }
    }

    public void Interact(Jugador player) {
        DialogueObject selectedDialogueObject;
        conectar();
        if (tresDragones) {
            selectedDialogueObject = dialogueObjectFinal;
        }
        else {
            selectedDialogueObject = dialogueObjectNoFinal;
        }
        if (selectedDialogueObject != null) {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>()) {
            if (responseEvents.DialogueObject == selectedDialogueObject) {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.ShowDialogue(selectedDialogueObject);
        }
    }

    void conectar() {
        String conn = "URI=file:" + Application.dataPath + "/Plugins/Dragon Showdown db.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        String sqlQuery = "SELECT COUNT(Nombre) FROM Monstruo";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            int numeroDragones = reader.GetInt32(0);
            Debug.Log("Ha salido este número de dragones: " + numeroDragones);
            if (numeroDragones >= 3) {
                tresDragones = true;
            }
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
