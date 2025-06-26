using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVEffect : MonoBehaviour
{
    public Animator tvEffectAnim;

    [SerializeField] GameObject botaoFimDoDia;

    [SerializeField] Tutorial tutorialScript;

    public void DelayMostrarBotaoFimDoDia()
    {

        Invoke("MostrarBotaoFimDoDia", 0.25f);

    }

    void MostrarBotaoFimDoDia()
    {

        botaoFimDoDia.SetActive(true);

    }

    public void IniciarTutorial()
    {

        if (tutorialScript != null)
        {

            //print("Iniciar tutorial");
            tutorialScript.Tutoriall();

        }

    }

}
