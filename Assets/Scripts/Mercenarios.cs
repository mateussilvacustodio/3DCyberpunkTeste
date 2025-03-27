using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Mercenarios : MonoBehaviour
{

    public List<GameObject> pedidosAceitos = new List<GameObject>();
    public List<GameObject> pedidosFalhos = new List<GameObject>();
    [Header("MissaoAtual")]
    public GameObject missaoAtual;
    public int indexDaMissaoAtual;
    public float forcaNecessariaMissaoAtual;
    public float inteligenciaNecessariaMissaoAtual;
    public float stealhNecessarioMissaoAtual;
    public string nomeMercenarioAtual;
    public float forcaMercenarioAtual;
    public float inteligenciaMercenarioAtual;
    public float stealhMercenarioAtual;
    [Header("Paineis")]
    [SerializeField] GameObject ListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;

    public TMP_Text textoFimDoDia;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destruirMissaoAtual(float pForcaMercenarioAtual, float pInteligenciaMercenarioAtual, float pStealthMercenarioAtual, string pNomeMercenarioAtual) {

        forcaMercenarioAtual = pForcaMercenarioAtual;
        inteligenciaMercenarioAtual = pInteligenciaMercenarioAtual;
        stealhMercenarioAtual = pStealthMercenarioAtual;
        nomeMercenarioAtual = pNomeMercenarioAtual;
        
        if(forcaMercenarioAtual >= forcaNecessariaMissaoAtual && inteligenciaMercenarioAtual >= inteligenciaNecessariaMissaoAtual && stealhMercenarioAtual >= stealhNecessarioMissaoAtual)
        {
            textoFimDoDia.text += "- O mercenario " + nomeMercenarioAtual + " cumpriu a missão \n"; 

        } else {

            textoFimDoDia.text += "- O mercenario " + nomeMercenarioAtual + " não cumpriu a missão \n";
            GameObject clone = Instantiate(missaoAtual);
            clone.SetActive(false);
            pedidosFalhos.Add(clone);

        }
        
        Destroy(pedidosAceitos[indexDaMissaoAtual]);

    }

    public void FecharListaDeMercenarios() {

        ListaDeMercenarios.SetActive(false);
        scrollRectMercenario.enabled = true;
        scrollbarMercenario.SetActive(true);

    }

    
}
