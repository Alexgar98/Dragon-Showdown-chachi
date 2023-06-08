using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScriptDialogoInicial : MonoBehaviour
{
    private int respuestasTierra = 0;
    private int respuestasMar = 0;
    private int respuestasAire = 0;

    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObjectInicial;
    [SerializeField] private DialogueObject dialogueObjectFinal;
    [SerializeField] private DialogueObject[] preguntas;

    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Interact());
    }

    public void UpdateDialogueObject(DialogueObject dialogueObject) {
        this.dialogueObjectInicial = dialogueObject;
    }

    public IEnumerator Interact() {
        yield return new WaitForSeconds(1f);

        this.DialogueUI.ShowDialogue(dialogueObjectInicial);
        yield return new WaitUntil(() => !this.DialogueUI.IsOpen);
        List<DialogueObject> dialogos = new List<DialogueObject>();
        

        DialogueObject[] shuffledPreguntas = ShuffleArray(preguntas);

        for (int i = 0; i < 3; i++)
            {
                this.DialogueUI.ShowDialogue(shuffledPreguntas[i]);
                yield return new WaitUntil(() => !this.DialogueUI.IsOpen);
            }

        if (this.respuestasTierra == this.respuestasAire && this.respuestasTierra == this.respuestasMar) {
            this.DialogueUI.ShowDialogue(shuffledPreguntas[3]);
            yield return new WaitUntil(() => !this.DialogueUI.IsOpen);
        }

        this.DialogueUI.ShowDialogue(dialogueObjectFinal);
        yield return new WaitUntil(() => !this.DialogueUI.IsOpen);
        SceneManager.LoadScene("Overworld");
    }

    private DialogueObject[] ShuffleArray(DialogueObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            DialogueObject temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }

        return array;
    }

    public void respuestaTierra() {
        this.respuestasTierra += 1;
        Debug.Log("Respuestas tierra: " + respuestasTierra);
    }
    public void respuestaMar() {
        this.respuestasMar += 1;
        Debug.Log("Respuestas mar: " + respuestasMar);
    }
    public void respuestaAire() {
        this.respuestasAire += 1;
        Debug.Log("Respuestas aire: " + respuestasAire);
    }
}
