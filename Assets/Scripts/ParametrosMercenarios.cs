using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametrosMercenarios : MonoBehaviour
{
    [Header("Paramentros")]
    [SerializeField] string nomeMercenario;
    [SerializeField] float forcaMercenario;
    [SerializeField] float inteligenciaMercenario;
    [SerializeField] float stealhMercenario;

    [SerializeField] Mercenarios mercenarioScript;

    [Header("Paineis")]
    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;

    public void EscolherMercenario() {

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);

        mercenarioScript.destruirMissaoAtual(forcaMercenario, inteligenciaMercenario, stealhMercenario, nomeMercenario);

    }

}
