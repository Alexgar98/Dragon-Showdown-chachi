using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    public bool IsOpen {get; private set;}

    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        closeDialogueBox();
        
    }

    public void ShowDialogue (DialogueObject dialogueObject) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents) {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++) {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.X));
        }

        if (dialogueObject.HasResponses) {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else {
            closeDialogueBox();
        }

        
    }

    private IEnumerator RunTypingEffect(string dialogue) {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning) {
            yield return null;

            if (Input.GetKeyDown(KeyCode.X)) {
                typewriterEffect.Stop();
            }
        }
    }

    public void closeDialogueBox() {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
