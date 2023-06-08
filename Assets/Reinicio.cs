using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que carga la pantalla del título
/// </summary>

public class Reinicio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Destroy(playerObject);
        }
    }

/// <summary>
/// Al pulsar cualquier tecla, se carga la pantalla del título
/// </summary>
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            SceneManager.LoadScene("Titulo");
        }
    }
}
