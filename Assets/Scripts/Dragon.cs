using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase Dragón, que se usa para almacenar todos los datos y estadísticas de un dragón
/// </summary>

public class Dragon : MonoBehaviour
{
    public string nombre; //Nombre de la especie
    public int vitMax; //Puntos de vida máximos
    public int atk; //Ataque
    public int def; //Defensa
    public int spe; //Velocidad. Decide quién ataca antes (no implementado)
    public int intl; //Inteligencia. Es una herramienta sorpresa que nos ayudará más tarde
    public int cor; //Cordura. Es otra herramienta sorpresa que también nos ayudará más tarde
    public string atr; //Atributo. Define a qué son eficaces o no sus ataques
    public string elem; //Elemento. Define a qué alteración de estado es inmune (no implementado)
    public int nv; //Nivel del Dragón
    public List<Ataque> ataques;
    public int vitActual; //Vida que le quede al Dragón
    public string estado; //Alteración de estado (no implementado)


/// <summary>
/// Función que hace que el Dragón reciba daño
/// <param name="damage">El daño que recibe el Dragón</param>
/// </summary>
    public bool TakeDamage(int damage) {
        vitActual -= damage;
        UnityEngine.Debug.Log("Daño hecho: " + damage);
        UnityEngine.Debug.Log("Vida actual: " + vitActual);
        return (vitActual <= 0);

    }

/// <summary>
/// Función que hace que el Dragón se cure
/// <param name="cantidad">La cantidad de HP que regenera el Dragón</param>
/// </summary>
    public void Heal(int cantidad) {
        vitActual += cantidad;
        if (vitActual > vitMax) {
            vitActual = vitMax;
        }
    }
}
