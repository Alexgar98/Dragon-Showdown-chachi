using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noche : MonoBehaviour
{
    public Shader shader;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);
        GetComponent<Renderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        var tiempoActual = System.DateTime.Now;
        var comienzo = new System.DateTime(tiempoActual.Year, tiempoActual.Month, tiempoActual.Day, 19, 0, 0);
        var fin = new System.DateTime(tiempoActual.Year, tiempoActual.Month, tiempoActual.Day, 7, 0, 0).AddDays(1);

        if (tiempoActual >= comienzo || tiempoActual <= fin) {
            material.SetFloat("_ActivateShader", 1f);
        }
        else {
            material.SetFloat("_ActivateShader", 0f);
        }
    }
}
