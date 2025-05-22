using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParametrosMercenarios : MonoBehaviour
{
    [Header("Paramentros")]
    [SerializeField] string nomeMercenario;
    [SerializeField] float forcaMercenario;
    [SerializeField] float inteligenciaMercenario;
    [SerializeField] float stealhMercenario;
    [SerializeField] float precoMercenario;
    [SerializeField] TMP_Text textoPrecoMercenario;
    [SerializeField] GameObject indisponivel;

    [Header("Game Controler")]
    [SerializeField] GameController gameController;
    [SerializeField] Mercenarios mercenarioScript;

    [Header("Paineis")]
    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;

    [Header("SetasTutorial")]
    [SerializeField] GameObject seta6;

    void Update()
    {
        textoPrecoMercenario.text = "$ " + precoMercenario.ToString();
    }

    public void EscolherMercenario()
    {

        mercenarioScript.destruirMissaoAtual(forcaMercenario, inteligenciaMercenario, stealhMercenario, nomeMercenario);
        gameController.gangues[6] -= precoMercenario;

        //textoPrecoMercenario.text = "Escolher";
        //textoPrecoMercenario.color = new Color32(50, 50, 50, 255);
        indisponivel.SetActive(true);

        if (seta6 != null)
        {

            seta6.SetActive(false);

        }

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);

    }

    // public void mudarTexto() {

    //     textoPrecoMercenario.text = "$ " + precoMercenario.ToString();
    //     textoPrecoMercenario.color = new Color32(255, 0, 0, 255);

    // }

    // public void voltarTexto() {

    //     textoPrecoMercenario.text = "Escolher";
    //     textoPrecoMercenario.color = new Color32(50, 50, 50, 255);

    // }

}
