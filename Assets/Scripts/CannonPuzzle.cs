using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPuzzle : MonoBehaviour
{
    private GameObject jugador;
    private static GameObject staticJugador;

    private void Awake() {
        if (staticJugador == null) {
            staticJugador = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(staticJugador);
        }
        jugador = staticJugador;
    }
    public void canon1L() {
        jugador.transform.position = new Vector2(82.3f, -50.1f);       
    }
    public void canon1R() {
        jugador.transform.position = new Vector2(72.4f, -38.3f);
    }
    public void canon2L() {
        jugador.transform.position = new Vector2(62.5f, -20.2f);
    }
    public void canon2R() {
        jugador.transform.position = new Vector2(72.4f, -26.5f);
    }
    public void canon3L() {
        jugador.transform.position = new Vector2(62.5f, -50.1f);
    }
    public void canon3R() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon4L() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon4R() {
        jugador.transform.position = new Vector2(52.6f, -26.5f);
    }
    public void canon5L() {
        jugador.transform.position = new Vector2(62.5f, -32.4f);
    }
    public void canon5R() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon6L() {
        jugador.transform.position = new Vector2(82.3f, -44.2f);
    }
    public void canon6R() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon7L() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon7R() {
        jugador.transform.position = new Vector2(52.6f, -26.5f);
    }
    public void canon8L() {
        jugador.transform.position = new Vector2(62.5f, -26.5f);
    }
    public void canon8R() {
        jugador.transform.position = new Vector2(72.4f, -44.2f);
    }
    public void canon9L() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon9R() {
        jugador.transform.position = new Vector2(82.3f, -38.3f);
    }
    public void canon10L() {
        jugador.transform.position = new Vector2(72.4f, -50.1f);
    }
    public void canon10R() {
        jugador.transform.position = new Vector2(82.3f, -38.3f);
    }
    public void canon11L() {
        jugador.transform.position = new Vector2(72.4f, -26.5f);
    }
    public void canon11R() {
        jugador.transform.position = new Vector2(62.5f, -38.3f);
    }
    public void canon12L() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon12R() {
        jugador.transform.position = new Vector2(82.3f, -50.1f);
    }
    public void canon13L() {
        jugador.transform.position = new Vector2(52.6f, -26.5f);
    }
    public void canon13R() {
        jugador.transform.position = new Vector2(62.5f, -44.2f);
    }
    public void canon14L() {
        jugador.transform.position = new Vector2(72.4f, -38.3f);
    }
    public void canon14R() {
        jugador.transform.position = new Vector2(72.4f, -50.1f);
    }
    public void canon15L() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon15R() {
        jugador.transform.position = new Vector2(72.4f, -32.4f);
    }
    public void canon16L() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon16R() {
        jugador.transform.position = new Vector2(82.3f, -32.4f);
    }
    public void canon17L() {
        jugador.transform.position = new Vector2(72.4f, -32.4f);
    }
    public void canon17R() {
        jugador.transform.position = new Vector2(82.3f, -26.5f);
    }
    public void canon18L() {
        jugador.transform.position = new Vector2(72.4f, -44.2f);
    }
    public void canon18R() {
        jugador.transform.position = new Vector2(62.5f, -26.5f);
    }
    public void canon19L() {
        jugador.transform.position = new Vector2(82.3f, -38.3f);
    }
    public void canon19R() {
        jugador.transform.position = new Vector2(52.6f, -44.2f);
    }
    public void canon20L() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon20R() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon21L() {
        jugador.transform.position = new Vector2(82.3f, -32.4f);
    }
    public void canon21R() {
        jugador.transform.position = new Vector2(62.5f, -50.1f);
    }
    public void canon22L() {
        jugador.transform.position = new Vector2(52.6f, -50.1f);
    }
    public void canon22R() {
        jugador.transform.position = new Vector2(62.5f, -20.2f);
    }
    public void canon23L() {
        jugador.transform.position = new Vector2(52.6f, -44.2f);
    }
    public void canon23R() {
        jugador.transform.position = new Vector2(52.6f, -32.4f);
    }
    public void canon24L() {
        jugador.transform.position = new Vector2(62.5f, -20.2f);
    }
    public void canon24R() {
        jugador.transform.position = new Vector2(62.5f, -20.2f);
    }
    public void canon25L() {
        jugador.transform.position = new Vector2(62.5f, -44.2f);
    }
    public void canon25R() {
        jugador.transform.position = new Vector2(52.6f, -38.3f);
    }
    public void canon26L() {
        jugador.transform.position = new Vector2(52.6f, -26.5f);
    }
    public void canon26R() {
        jugador.transform.position = new Vector2(52.6f, -32.4f);
    }
    
}
