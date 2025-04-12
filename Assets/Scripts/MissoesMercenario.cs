using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MissoesMercenario : MonoBehaviour
{
    [Header("Caracteristicas")]
    public string nomeNPC;
    [SerializeField] float forca;
    [SerializeField] float inteligencia;
    [SerializeField] float stealh;
    public int index;
    [Header("Punição")]
    //public bool[] recursosMercenarios;
    public float[] mudadoresMercenarios;
    [Header("Painel")]
    [SerializeField] GameObject painelListaDeMercenarios;
    [SerializeField] ScrollRect scrollRectMercenario;
    [SerializeField] GameObject scrollbarMercenario;
    [SerializeField] Mercenarios mercenarioScript;

    void Start()
    {
        painelListaDeMercenarios = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.CompareTag("ListaDeMercenarios"));
        scrollRectMercenario = Resources.FindObjectsOfTypeAll<ScrollRect>().FirstOrDefault(obj => obj.CompareTag("Scrollview"));
        scrollbarMercenario = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.CompareTag("ScrollBarMercenario"));
        mercenarioScript = Resources.FindObjectsOfTypeAll<Mercenarios>().FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirListaDeMercenarios() {

        scrollRectMercenario.enabled = false;
        scrollbarMercenario.SetActive(false);
        painelListaDeMercenarios.SetActive(true);

        //enviar dados para comparacao

        //print(index);
        mercenarioScript.indexDaMissaoAtual = index;
        mercenarioScript.forcaNecessariaMissaoAtual = forca;
        mercenarioScript.inteligenciaNecessariaMissaoAtual = inteligencia;
        mercenarioScript.stealhNecessarioMissaoAtual = stealh;
        mercenarioScript.missaoAtual = this.GameObject();
        

    }
}
