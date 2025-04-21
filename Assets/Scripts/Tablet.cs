using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    
    [SerializeField] GameObject painelTablet;
    [SerializeField] Text diaAtual;
    [SerializeField] GameController gameController;
    [SerializeField] Animator tabletAnim;

    [Header("Aplicativos")]
    [SerializeField] GameObject menuInicial;
    [SerializeField] GameObject menuReputacao;
    [SerializeField] GameObject menuInventario;
    [SerializeField] GameObject menuMercenarios;
    [SerializeField] GameObject listaMercenarios;
    [Header("Aplicativos Fim Do Dia")]
    [SerializeField] GameObject reputacaoFimDoDia;
    [SerializeField] GameObject inventarioFimDoDia;
    [SerializeField] GameObject mercenariosFimDoDia;
    [Header("SetasTutorial")]
    [SerializeField] GameObject[] setas;
    int indexSeta;
    void Update() {

        diaAtual.text = "Dia " + gameController.dia;

    }
    
    public void AbrirTablet() {

        painelTablet.SetActive(true);

    }

    public void FecharTablet() {

        menuInicial.SetActive(true);
        menuReputacao.SetActive(false);
        menuInventario.SetActive(false);
        menuMercenarios.SetActive(false);
        listaMercenarios.SetActive(false);
        if(setas.Length > 0) {

            setas[indexSeta].SetActive(true);

        }
        painelTablet.SetActive(false);

    }

    public void CrescerTablet() {

        tabletAnim.SetBool("Crescer", true);

    }

    public void DiminuirTablet() {

        tabletAnim.SetBool("Crescer", false);

    }

    public void AbrirReputacao() {

        menuInicial.SetActive(false);
        menuReputacao.SetActive(true);
        if(setas.Length > 0) {

            foreach (GameObject seta in setas)
            {
                seta.SetActive(false);
            }
            indexSeta = 0;
        }

    }

    public void AbrirInventario() {

        menuInicial.SetActive(false);
        menuInventario.SetActive(true);
        if(setas.Length > 0) {
            foreach (GameObject seta in setas)
            {
                seta.SetActive(false);
            }
            indexSeta = 1;
        }

    }

    public void AbrirMercenarios() {

        menuInicial.SetActive(false);
        menuMercenarios.SetActive(true);
        if(setas.Length > 0) {
            foreach (GameObject seta in setas)
            {
                seta.SetActive(false);
            }
            indexSeta = 2;
        }
        gameController.numNotificacao = 0;

    }

    public void VoltarTelaInicio() {

        menuInicial.SetActive(true);
        menuReputacao.SetActive(false);
        menuInventario.SetActive(false);
        menuMercenarios.SetActive(false);
        if(setas.Length > 0) {
            setas[indexSeta].SetActive(true);
        }

    }

    public void AbrirReputacaoFimDoDia() {

        reputacaoFimDoDia.SetActive(true);
        inventarioFimDoDia.SetActive(false);
        mercenariosFimDoDia.SetActive(false);

    }

    public void AbrirInventarioFimDoDia() {

        reputacaoFimDoDia.SetActive(false);
        inventarioFimDoDia.SetActive(true);
        mercenariosFimDoDia.SetActive(false);

    }

    public void AbrirMercenariosFimDoDia() {

        reputacaoFimDoDia.SetActive(false);
        inventarioFimDoDia.SetActive(false);
        mercenariosFimDoDia.SetActive(true);

    }
}
