using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonagemMercador : MonoBehaviour
{
    [SerializeField] GameObject painelMercador;
    void Start()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject go in allObjects) {
            
            if (go.name == "PainelMercador") {

                painelMercador = go;
                break;
            }
        }
    }

    public void AbrirMercador()
    {

        painelMercador.SetActive(true);

    }

}
