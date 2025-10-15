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
    [SerializeField] GameObject dinheiroGasto;

    [Header("Paineis")]
    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;

    [Header("SetasTutorial")]
    [SerializeField] GameObject seta6;

    void Update()
    {
        textoPrecoMercenario.text = "$" + precoMercenario.ToString();
    }

    public void EscolherMercenario()
    {

        gameController.SFXDinheiroGasto.Play();
        mercenarioScript.destruirMissaoAtual(forcaMercenario, inteligenciaMercenario, stealhMercenario, nomeMercenario);
        gameController.gangues[6] -= precoMercenario;

        indisponivel.SetActive(true);

        if (seta6 != null)
        {

            seta6.SetActive(false);

        }

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);
        string dinheiroGastoTexto = "-" + precoMercenario.ToString();
        dinheiroGasto.GetComponent<TMP_Text>().text = dinheiroGastoTexto;
        GameObject dinheiroGastoInstancia = Instantiate(dinheiroGasto, gameController.dinheiroText4.transform);
        Destroy(dinheiroGastoInstancia, 0.75f);

    }

}
