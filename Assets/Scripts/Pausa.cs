using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Mono.Data.Sqlite;
using System.Data;
using System;

/// <summary>
/// Clase que maneja la pantalla de pausa. Tiene el canvas, tres objetos Dragón y los tres GameObjects, posiciones, nombres y barras de vida correspondientes
/// </summary>
public class Pausa : MonoBehaviour
{
    bool enPausa = false;
    public Canvas canvas;
    public Animator animacion;
    public Button botonSalir;
    public GameObject pj;

    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    private Dragon dragon1;
    private Dragon dragon2;
    private Dragon dragon3;

    public GameObject dragon1GO;
    public GameObject dragon2GO;
    public GameObject dragon3GO;

    public Transform hueco1;
    public Transform hueco2;
    public Transform hueco3;

    public TextMeshProUGUI nombre1;
    public TextMeshProUGUI nombre2;
    public TextMeshProUGUI nombre3;

    public TextMeshProUGUI vida1;
    public TextMeshProUGUI vida2;
    public TextMeshProUGUI vida3;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    private List<Dragon> dragones = new List<Dragon>();

/// <summary>
/// Llama a SetHUD con los tres objetos Dragón y desactiva la pantalla
/// </summary>
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        //conectar();
        //SetHUD(ref dragon1, ref dragon2, ref dragon3);        
    }

/// <summary>
/// Si se pulsa Escape, comprueba si el juego está en pausa para activar o desactivar la pantalla llamando a PausarJuego() o ReanudarJuego()
/// </summary>
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (dialogueUI.IsOpen) return;
            if (!enPausa) {
                //conectar();
                SetHUD(ref dragon1, ref dragon2, ref dragon3);     
                PausarJuego();
            }
            else {
                ReanudarJuego();
            }
        }
    }

/// <summary>
/// Función que pausa el juego y activa la pantalla de pausa
/// </summary>
    void PausarJuego() {
        Time.timeScale = 0;
        canvas.enabled = true;
        Debug.Log("Juego en pausa");
        enPausa = true;
        this.animacion.enabled = false;
        botonSalir.onClick.AddListener(Salir);
    }

/// <summary>
/// Función que reanuda el juego y desactiva la pantalla de pausa
/// </summary>
    void ReanudarJuego() {
        Time.timeScale = 1;
        canvas.enabled = false;
        Debug.Log("El juego no está en pausa");
        enPausa = false;
        this.animacion.enabled = true;
    }

    void Salir() {
        Destroy(pj);
        ReanudarJuego();
        SceneManager.LoadScene("Titulo");
    }

/// <summary>
/// Función que setea todos los datos que van a mostrarse en la pantalla de pausa
/// <param name="dragon1">El primer objeto Dragón que aparece</param>
/// <param name="dragon2">El primer objeto Dragón que aparece</param>
/// <param name="dragon3">El primer objeto Dragón que aparece</param>
/// </summary>
    void SetHUD (ref Dragon dragon1, ref Dragon dragon2, ref Dragon dragon3) {
        /*GameObject dragon1Inst = Instantiate (dragon1GO, hueco1);
        dragon1 = dragon1Inst.GetComponent<Dragon>();
        GameObject dragon2Inst = Instantiate (dragon2GO, hueco2);
        dragon2 = dragon2Inst.GetComponent<Dragon>();
        GameObject dragon3Inst = Instantiate (dragon3GO, hueco3);
        dragon3 = dragon3Inst.GetComponent<Dragon>();*/

        conectar();

        for (int i = 0; i < dragones.Count; i++)
        {
        Dragon dragon = dragones[i];
        switch (i)
        {
            case 0:
                nombre1.text = dragon.nombre + " nv " + dragon.nv;
                slider1.maxValue = dragon.vitMax;
                slider1.value = dragon.vitActual;
                vida1.text = dragon.vitActual + "/" + dragon.vitMax;
                break;
            case 1:
                nombre2.text = dragon.nombre + " nv " + dragon.nv;
                slider2.maxValue = dragon.vitMax;
                slider2.value = dragon.vitActual;
                vida2.text = dragon.vitActual + "/" + dragon.vitMax;
                nombre2.enabled = true;
                slider2.gameObject.SetActive(true);
                vida2.enabled = true;
                break;
            case 2:
                nombre3.text = dragon.nombre + " nv " + dragon.nv;
                slider3.maxValue = dragon.vitMax;
                slider3.value = dragon.vitActual;
                vida3.text = dragon.vitActual + "/" + dragon.vitMax;
                nombre3.enabled = true;
                slider3.gameObject.SetActive(true);
                vida3.enabled = true;
                break;
        }
        if (dragones.Count < 3) {
            nombre3.enabled = false;
            slider3.gameObject.SetActive(false);
            vida3.enabled = false;
            if (dragones.Count < 2) {
                nombre2.enabled = false;
                slider2.gameObject.SetActive(false);
                vida2.enabled = false;
            }
        }
    }
    }

/// <summary>
/// Conexión con la base de datos para leer los datos de los Dragones del jugador.
/// </summary>
    void conectar() {
        dragones.Clear();
        String conn = "URI=file:" + Application.dataPath + "/Plugins/Dragon Showdown db.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        String sqlQuery = "SELECT Nombre, HP, HP_Actual, Nivel FROM Monstruo";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            String nombre = reader.GetString(0);
            int hp = reader.GetInt32(1);
            int hpActual = reader.GetInt32(2);
            int nivel = reader.GetInt32(3);

            Dragon dragon = new Dragon();
            dragon.nombre = nombre;
            dragon.vitMax = hp;
            dragon.vitActual = hpActual;
            dragon.nv = nivel;
            dragones.Add(dragon);

        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
