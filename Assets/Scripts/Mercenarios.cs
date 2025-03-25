using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mercenarios : MonoBehaviour
{

    public List<GameObject> pedidosAceitos = new List<GameObject>();
    public int indexDaMissaoAtual;

    public float forcaNecessariaMissaoAtual;
    public float inteligenciaNecessariaMissaoAtual;
    public float stealhNecessarioMissaoAtual;
    public float forcaMercenarioAtual;
    public float inteligenciaMercenarioAtual;
    public float stealhMercenarioAtual;

    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destruirMissaoAtual(float pForcaMercenarioAtual, float pInteligenciaMercenarioAtual, float pStealthMercenarioAtual) {

        forcaMercenarioAtual = pForcaMercenarioAtual;
        inteligenciaMercenarioAtual = pInteligenciaMercenarioAtual;
        stealhMercenarioAtual = pStealthMercenarioAtual;
        
        if(forcaMercenarioAtual >= forcaNecessariaMissaoAtual && inteligenciaMercenarioAtual >= inteligenciaNecessariaMissaoAtual && stealhMercenarioAtual >= stealhNecessarioMissaoAtual)
        {
            print("Missao cumprida");

        } else {

            print("Missao falha");

        }
        
        Destroy(pedidosAceitos[indexDaMissaoAtual]);

    }

    public void FecharListaDeMercenarios() {

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);

    }

    
}
