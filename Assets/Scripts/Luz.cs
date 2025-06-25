using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Luz : MonoBehaviour
{
    [SerializeField] Light spotLight;

    [SerializeField] float velocidadeLuz;
    [SerializeField] float intensidadeMinima;
    [SerializeField] float intensidadeMaxima;
    [SerializeField] float controleAumentoLuz;
    [SerializeField] float quantidadePiscadas;
    [SerializeField] float maxPiscadas;
    [SerializeField] float tempoEntrePiscadas;
    [SerializeField] bool pararLuz;
    void Start()
    {
        StartCoroutine(VoltarLuz());
    }

    void Update()
    {

        if (spotLight.intensity >= intensidadeMaxima)
        {

            if (pararLuz)
            {

                controleAumentoLuz = 0;

            }
            else
            {

                controleAumentoLuz = -1;

            }


        }
        else if (spotLight.intensity <= intensidadeMinima)
        {

            controleAumentoLuz = 1;
            quantidadePiscadas++;
            if (quantidadePiscadas >= maxPiscadas)
            {
                pararLuz = true;
                quantidadePiscadas = 0;

            } 

        }

        spotLight.intensity += velocidadeLuz * Time.deltaTime * controleAumentoLuz;

    }

    IEnumerator VoltarLuz()
    {

        yield return new WaitForSeconds(tempoEntrePiscadas);
        pararLuz = false;
        StartCoroutine(VoltarLuz());

    }
}
