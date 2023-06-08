using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject) {
        if (!enabled) {
            return;
        }
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!enabled) {
            return;
        }
        if (other.CompareTag("Player") && other.TryGetComponent(out Jugador player)) {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (!enabled) {
            return;
        }
        if (other.CompareTag("Player") && other.TryGetComponent(out Jugador player)) {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this) {
                player.Interactable = null;
            }
        }
    }

    public void Interact(Jugador player) {
        if (!enabled) {
            return;
        }
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>()) {
            if (responseEvents.DialogueObject == dialogueObject) {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
