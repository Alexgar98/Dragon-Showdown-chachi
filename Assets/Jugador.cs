using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;

/// <summary>
/// Clase que controla el movimiento y colisiones del jugador
/// <param name="playerInstance">Objeto de tipo Jugador que se usa para guardarlo para el cambio entre escenas</param>
/// <param name="dialogueUI">Objeto que hace funcionar el cuadro de diálogo</param>
/// <param name="animacion">Objeto que hace funcionar la animación</param>
/// <param name="velocidad">Valor de velocidad de desplazamiento</param>
/// </summary>
public class Jugador : MonoBehaviour
{
    private static Jugador playerInstance;

    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable {get; set;}

    private Animator animacion;
    public float velocidad;

/// <summary>
/// Función que guarda este objeto en un DontDestroyOnLoad y comprueba si hay otro objeto del mismo tipo. Si lo hay, lo destruye
/// </summary>
    void Awake() {
        //DontDestroyOnLoad(this);

        if (playerInstance == null) { 
            playerInstance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
            this.transform.position = new Vector2(-17f, 0.65f);
        }
    }

/// <summary>
/// Instancia el objeto Animator para poder ejecutar la animación
/// </summary>
    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();
    }

/// <summary>
/// Primero impide hacer nada si hay una caja de diálogo abierta. Luego, hace moverse al personaje, con su animación, al pulsar WASD. Por último, al pulsar X y poderse interactuar con algo, abre la caja de diálogo
/// </summary>
    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen) return;
        else {
            if (Input.GetKey(KeyCode.W)) {
                velocidad = 1;
                animacion.SetFloat("Horizontal", 0);
                animacion.SetFloat("Vertical", 1);
                animacion.SetFloat("Velocidad", 1);
                transform.Translate(new Vector2(0,velocidad*Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S)) {
                velocidad = 1;
                animacion.SetFloat("Horizontal", 0);
                animacion.SetFloat("Vertical", -1);
                animacion.SetFloat("Velocidad", 1);
                transform.Translate(new Vector2(0,-velocidad*Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.A)) {
                velocidad = 1;
                animacion.SetFloat("Horizontal", -1);
                animacion.SetFloat("Vertical", 0);
                animacion.SetFloat("Velocidad", 1);
                transform.Translate(new Vector2(-velocidad*Time.deltaTime,0));
            }
            if (Input.GetKey(KeyCode.D)) {
                velocidad = 1;
                animacion.SetFloat("Horizontal", 1);
                animacion.SetFloat("Vertical", 0);
                animacion.SetFloat("Velocidad", 1);
                transform.Translate(new Vector2(velocidad*Time.deltaTime,0));
            }
            if (!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.D)) {
                animacion.SetFloat("Velocidad", 0);
                velocidad = 0;
            }
            
        }
        

        if (Input.GetKeyDown(KeyCode.X) && dialogueUI.IsOpen == false) {
            Interactable?.Interact(this); //Esto equivale a un if(!null)
        }
    }
/// <summary>
/// Función que comprueba la colisión a la que entra el personaje y realiza los cambios de escenas llamando a CambiarPantalla.cambiarAPantalla
/// </summary>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag=="Ruinas") {
            Debug.Log("Cambiando de overworld a ruinas");
            CambiarPantalla.cambiarAPantalla("Ruinas", "Overworld", gameObject, dialogueUI);
        }

        if (other.gameObject.tag=="Hielo") {
            Debug.Log("Cambiando de overworld a hielo");
            CambiarPantalla.cambiarAPantalla("Hielo", "Overworld", gameObject, dialogueUI);
        }

        if (other.gameObject.tag=="SalidaHielo") {
            Debug.Log("Cambiando de hielo a overworld");
            CambiarPantalla.cambiarAPantalla("Overworld", "Hielo", gameObject, dialogueUI);
        }

        if (other.gameObject.tag=="SalidaRuinas") {
            Debug.Log("Cambiando de ruinas a overworld");
            CambiarPantalla.cambiarAPantalla("Overworld", "Ruinas", gameObject, dialogueUI);
        }

        if (other.gameObject.tag=="NPC") {
            Debug.Log("Ha chocao con el NPC");
            
        }

        if (other.gameObject.tag=="Agua") {
            Debug.Log("Al agua");
            this.transform.position = new Vector2(-50, -22);
        }
        
    }
}
