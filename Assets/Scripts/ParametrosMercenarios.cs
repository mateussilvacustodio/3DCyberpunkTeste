using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametrosMercenarios : MonoBehaviour
{
    
    [SerializeField] float forcaMercenario;
    [SerializeField] float inteligenciaMercenario;
    [SerializeField] float stealhMercenario;

    [SerializeField] Mercenarios mercenarioScript;

    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscolherMercenario() {

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);

        //comparar mercenario com missao
        mercenarioScript.destruirMissaoAtual(forcaMercenario, inteligenciaMercenario, stealhMercenario);

    }

}
