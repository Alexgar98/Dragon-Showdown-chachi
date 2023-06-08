using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase para establecer los valores que van a tomar los elementos del layout de batalla
/// <param name="nameText">Texto que representa el nombre del Dragón</param>
/// <param name="levelText">Texto que representa el nivel del Dragón</param>
/// <param name="hpSlider">Slider que representa la cantidad de vida del Dragón respecto al máximo</param>
/// </summary>
public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

/// <summary>
/// Setter del layout de batalla
/// <param name="dragon">Objeto Dragón del que se toman los datos</param>
/// </summary>
    public void SetHUD(Dragon dragon) {
        nameText.text = dragon.nombre;
        levelText.text = "Lv " + dragon.nv;
        hpSlider.maxValue = dragon.vitMax;
        hpSlider.value = dragon.vitActual;
    }

/// <summary>
/// Setter para actualizar el slider de la vida
/// <param name="vit">La cantidad de vida a la que queremos setear el slider</param>
/// </summary>
    public void setVit(int vit) {
        hpSlider.value = vit;
    }

    public void disable() {
        nameText.enabled = false;
        levelText.enabled = false;
        hpSlider.gameObject.SetActive(false);
    }
}
