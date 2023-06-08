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
/// Enum para establecer los estados que puede tomar el combate
/// </summary>
public enum BattleState {
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}

/// <summary>
/// Clase que controla el sistema de batalla
/// <param name="jugador">GameObject que representa al Dragón controlado por el jugador</param>
/// <param name="contrario">GameObject que representa al Dragón controlado por la IA</param>
/// <param name="playerBattleStation">Posición en la que se coloca el Dragón del jugador</param>
/// <param name="foeBattleStation">Posición en la que se coloca el Dragón de la IA</param>
/// <param name="dragonJugador">Objeto Dragón donde se encuentran los datos del Dragón del jugador</param>
/// <param name="dragonContrario">Objeto Dragón donde se encuentran los datos del Dragón de la IA</param>
/// <param name="dialogo">Texto que muestra las acciones que ocurren en el combate</param>
/// <param name="jugadorHUD">Objeto BattleHUD que controla la parte del layout correspondiente al Dragón del jugador</param>
/// <param name="contrarioHUD">Objeto BattleHUD que controla la parte del layout correspondiente al Dragón de la IA</param>
/// <param name="state">Estado que toma el combate</param>
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public GameObject jugador;
    public GameObject contrario;

    public Transform playerBattleStation;
    public Transform foeBattleStation;

    Dragon dragonJugador;
    Dragon dragonContrario;
    Dragon reserva1; //Dragones en la reserva, para cambio
    Dragon reserva2;

    public TextMeshProUGUI dialogo;

    public BattleHUD jugadorHUD;
    public BattleHUD contrarioHUD;
    public BattleHUD reserva1HUD; //Dragones en la reserva, para cambio
    public BattleHUD reserva2HUD;

    public TextMeshProUGUI ataqueFisico;
    public TextMeshProUGUI ataqueEstado;
    public TextMeshProUGUI ataqueElemental;

    public BattleState state;

    private List<Dragon> dragones = new List<Dragon>();
    private List<String> ataques = new List<String>();

/// <summary>
/// Función que setea el estado del combate a Start y llama a SetupBattle()
/// </summary>
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

/// <summary>
/// Corrutina que instancia a los dos Dragones con sus datos y los coloca en sus sitios. Tras una pausa, le da el turno al jugador llamando a PlayerTurn()
/// </summary>
    IEnumerator SetupBattle() {
        conectar();
        dragonJugador = dragones[0];
        Debug.Log(dragonJugador.nombre);
        if (dragones.Count > 1) {
            reserva1 = dragones[1];
            Debug.Log(reserva1.nombre);
            reserva1HUD.SetHUD(reserva1);
            if (dragones.Count > 2) {
                reserva2 = dragones[2];
                Debug.Log(reserva2.nombre);
                reserva2HUD.SetHUD(reserva2);
            }
        }
        ataqueFisico.text = ataques[0];
        ataqueEstado.text = ataques[1];
        ataqueElemental.text = ataques[2];
        GameObject jugadorGO = Instantiate(jugador, playerBattleStation);
        //dragonJugador = jugadorGO.GetComponent<Dragon>();

        GameObject contrarioGO = Instantiate(contrario, foeBattleStation);
        dragonContrario = contrarioGO.GetComponent<Dragon>();

        dialogo.text = "It's a foe " + dragonContrario.nombre + "!";

        jugadorHUD.SetHUD(dragonJugador);
        contrarioHUD.SetHUD(dragonContrario);

        if (dragones.Count < 3) {
            reserva2HUD.disable();
            if (dragones.Count < 2) {
                reserva1HUD.disable();
            }
        }

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

/// <summary>
/// Corrutina que ejecuta un ataque físico. Resta salud al Dragón contrario, comprueba si ha muerto o no y cambia de estado en consecuencia. Si ha muerto, llama a EndBattle(); si no, llama a TurnoContrario()
/// </summary>
    IEnumerator AtaqueFisico() {
        bool muerto = false;
        if (dragonJugador.atr == "Tierra") {
            if (dragonContrario.atr == "Mar") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Aire") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonJugador.atr == "Mar") {
            if (dragonContrario.atr == "Aire") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Tierra") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonJugador.atr == "Aire") {
            if (dragonContrario.atr == "Tierra") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Mar") {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It's a hit!";
            }
        }

        contrarioHUD.setVit(dragonContrario.vitActual);
        

        if (muerto) {
            state = BattleState.WON;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(TurnoContrario());
        }
        //Cambiar estado según lo que pase
    }

/// <summary>
/// Corrutina que ejecuta un ataque de estado. Está por ahora hardcodeado a curar al Dragón del jugador y pasar el turno al contrario
/// </summary>
    IEnumerator AtaqueEstado() {
        dragonJugador.Heal(20);
        jugadorHUD.setVit(dragonJugador.vitActual);
        dialogo.text = "That was a needed rest!";
        UnityEngine.Debug.Log(dragonJugador.vitActual + "|" + dragonJugador.vitMax);
        //Ataque de estado (no causa daño directo); se saca de base de datos

        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        StartCoroutine(TurnoContrario());

    }

/// <summary>
/// Corrutina que ejecuta un ataque elemental. Resta salud a ambos Dragones, comprueba si han muerto o no y cambia de estado en consecuencia. Si ha muerto alguno, llama a EndBattle(); si no, llama a TurnoContrario(). Si los dos han muerto, se da el combate por perdido
/// </summary>
    IEnumerator AtaqueElemental() {
        bool jugadorMuerto = false;
        bool contrarioMuerto = false;
        if (dragonJugador.atr == "Tierra") {
            if (dragonContrario.atr == "Mar") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Aire") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*4);
                dialogo.text = "It was super effective!";
            }
            else {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonJugador.atr == "Mar") {
            if (dragonContrario.atr == "Aire") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Tierra") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*4);
                dialogo.text = "It was super effective!";
            }
            else {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonJugador.atr == "Aire") {
            if (dragonContrario.atr == "Tierra") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonContrario.atr == "Mar") {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*4);
                dialogo.text = "It was super effective!";
            }
            else {
                contrarioMuerto = dragonContrario.TakeDamage(dragonJugador.atk*2);
                dialogo.text = "It's a hit!";
            }
        }

        contrarioHUD.setVit(dragonContrario.vitActual);

        yield return new WaitForSeconds(2f);

        jugadorMuerto = dragonJugador.TakeDamage(dragonJugador.atk*2);

        jugadorHUD.setVit(dragonJugador.vitActual);
        dialogo.text = dragonJugador.nombre + " suffered recoil!";
        UnityEngine.Debug.Log(dragonJugador.vitActual + "|" + dragonJugador.vitMax);

        if (jugadorMuerto) {
            state = BattleState.LOST;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndBattle());
        }
        else if (contrarioMuerto) {
            state = BattleState.WON;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(TurnoContrario());
        }
    }

/// <summary>
/// Corrutina que ejecuta el ataque del contrario. Resta salud al Dragón del jugador, comprueba si ha muerto o no y cambia de estado en consecuencia. Si ha muerto, llama a EndBattle(); si no, llama a PlayerTurn()
/// </summary>
    IEnumerator TurnoContrario() {
        bool muerto = false;
        dialogo.text = dragonContrario.nombre + " attacks!";
        yield return new WaitForSeconds(2f);
        if (dragonContrario.atr == "Tierra") {
            if (dragonJugador.atr == "Mar") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonJugador.atr == "Aire") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonContrario.atr == "Mar") {
            if (dragonJugador.atr == "Aire") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonJugador.atr == "Tierra") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk);
                dialogo.text = "It's a hit!";
            }
        }
        if (dragonContrario.atr == "Aire") {
            if (dragonJugador.atr == "Tierra") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk/2);
                dialogo.text = "It was not very effective!";
            }
            else if (dragonJugador.atr == "Mar") {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk*2);
                dialogo.text = "It was super effective!";
            }
            else {
                muerto = dragonJugador.TakeDamage(dragonContrario.atk);
                dialogo.text = "It's a hit!";
            }
        }
        UnityEngine.Debug.Log("Muerto "+muerto);
        UnityEngine.Debug.Log("ataque contrario: "+dragonContrario.atk);
        jugadorHUD.setVit(dragonJugador.vitActual);
        
        UnityEngine.Debug.Log(dragonJugador.vitActual + "|" + dragonJugador.vitMax);

        yield return new WaitForSeconds(2f);
        
        if (muerto) {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator Cambiar1() {
        Dragon auxiliar = dragonJugador;
        dragonJugador = reserva1;
        reserva1 = auxiliar;
        jugadorHUD.SetHUD(dragonJugador);
        reserva1HUD.SetHUD(reserva1);
        dialogo.text = "Your Dragon was changed!";
        UnityEngine.Debug.Log(dragonJugador.vitActual + "|" + dragonJugador.vitMax);
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        StartCoroutine(TurnoContrario());
        
    }

    IEnumerator Cambiar2() {
        UnityEngine.Debug.Log("Si ves este mensaje es que el botón 2 hace algo");
        state = BattleState.ENEMYTURN;
        Dragon auxiliar = dragonJugador;
        dragonJugador = reserva2;
        reserva2 = auxiliar;
        jugadorHUD.SetHUD(dragonJugador);
        reserva2HUD.SetHUD(reserva2);
        dialogo.text = "Your Dragon was changed!";
        yield return new WaitForSeconds(2f);
        StartCoroutine(TurnoContrario());
    }

/// <summary>
/// Función que termina el combate. Si el jugador ha ganado, pasa por ahora a la pantalla final; si no, a la de Game Over
/// </summary>
    IEnumerator EndBattle() {
        if (state == BattleState.WON) {
            dialogo.text = "The foe " + dragonContrario.nombre + " was defeated!";
            yield return new WaitForSeconds(2f);
            string escenaAnterior = PlayerPrefs.GetString("EscenaAnterior");
            if (escenaAnterior == "Overworld") {
                SceneManager.LoadScene("Creditos");
                
            }
            else {
                SceneManager.LoadScene(escenaAnterior);
            }
        }
        else if (state == BattleState.LOST) 
        {
            dialogo.text = "You lost the battle!";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameOver");
        }
    }

/// <summary>
/// Función que cambia el texto cuando es el turno del jugador
/// </summary>
    void PlayerTurn() {
        dialogo.text = "What should " + dragonJugador.nombre + " do?";
    }

/// <summary>
/// Función que setea el efecto del botón de "Ataque físico", evitando que haga nada si no es el turno del jugador
/// </summary>
    public void BotonFisico() {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine(AtaqueFisico());


    }
/// <summary>
/// Función que setea el efecto del botón de "Ataque de estado", evitando que haga nada si no es el turno del jugador
/// </summary>
    public void BotonEstado() {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine(AtaqueEstado());


    }

/// <summary>
/// Función que setea el efecto del botón de "Ataque elemental", evitando que haga nada si no es el turno del jugador
/// </summary>
    public void BotonElemental() {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine(AtaqueElemental());


    }

    public void BotonCambio1() {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine(Cambiar1());
    }

    public void BotonCambio2() {
        if (state != BattleState.PLAYERTURN) {
            return;
        }
        StartCoroutine(Cambiar2());
    }

/// <summary>
/// Función para conectar los cambios que ocurran en el combate con la base de datos. No funciona.
/// </summary>
    void conectar() {
        String conn = "URI=file:" + Application.dataPath + "/Plugins/Dragon Showdown db.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        String sqlQuery = "SELECT Nombre, HP, HP_Actual, Nivel, Ataque, Atributo FROM Monstruo";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            String nombre = reader.GetString(0);
            int hp = reader.GetInt32(1);
            int hpActual = reader.GetInt32(2);
            int nivel = reader.GetInt32(3);
            int ataque = reader.GetInt32(4);
            String atributo = reader.GetString(5);

            Dragon dragon = new Dragon();
            dragon.nombre = nombre;
            dragon.vitMax = hp;
            dragon.vitActual = hpActual;
            dragon.nv = nivel;
            dragon.atk = ataque;
            dragon.atr = atributo;
            dragones.Add(dragon);
        }

        reader.Close();
        reader = null;

        sqlQuery = "SELECT Nombre FROM Ataque";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            String ataqueLeido = reader.GetString(0);
            ataques.Add(ataqueLeido);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }

    
}
