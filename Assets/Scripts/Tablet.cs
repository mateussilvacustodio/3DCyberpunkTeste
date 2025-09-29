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
    [SerializeField] GameObject contornoTablet;

    [Header("Aplicativos")]
    [SerializeField] GameObject menuInicial;
    [SerializeField] GameObject menuReputacao;
    [SerializeField] GameObject menuInventario;
    [SerializeField] GameObject menuMercenarios;
    [SerializeField] GameObject listaMercenarios;
    [SerializeField] GameObject menuConfiguracoes;
    [SerializeField] GameObject menuCheats;
    [Header("Aplicativos Fim Do Dia")]
    [SerializeField] GameObject reputacaoFimDoDia;
    [SerializeField] GameObject inventarioFimDoDia;
    [SerializeField] GameObject mercenariosFimDoDia;
    [Header("SetasTutorial")]
    [SerializeField] GameObject[] setas;
    //int indexSeta;
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
        menuConfiguracoes.SetActive(false);
        menuCheats.GetComponent<Cheats>().codigoDigitado.text = "";
        menuCheats.SetActive(false); //no tutorial está ligado o menu de config, para n precisar passar os cheats, ja que o jogador nem tem acesso a eles no tutorial
        if (setas.Length > 0)
        {

            if (setas[3].activeSelf)
            {

                setas[2].SetActive(true);

            }

            if (setas[5].activeSelf)
            {

                setas[4].SetActive(true);

            }

        }
        painelTablet.SetActive(false);

    }

    public void CrescerTablet()
    {

        //tabletAnim.SetBool("Crescer", true);
        contornoTablet.SetActive(true);

    }

    public void DiminuirTablet()
    {

        //tabletAnim.SetBool("Crescer", false);
        contornoTablet.SetActive(false);

    }

    public void AbrirReputacao() {

        menuInicial.SetActive(false);
        menuReputacao.SetActive(true);

    }

    public void AbrirInventario() {

        menuInicial.SetActive(false);
        menuInventario.SetActive(true);

    }

    public void AbrirMercenarios() {

        menuInicial.SetActive(false);
        menuMercenarios.SetActive(true);
        gameController.numNotificacao = 0;

    }

    public void AbrirConfiguracoes()
    {
        
        menuInicial.SetActive(false);
        menuConfiguracoes.SetActive(true);

    }

    public void AbrirCheats()
    {

        menuInicial.SetActive(false);
        menuCheats.SetActive(true);

    }

    public void VoltarTelaInicio()
    {

        menuInicial.SetActive(true);
        menuReputacao.SetActive(false);
        menuInventario.SetActive(false);
        menuMercenarios.SetActive(false);
        menuConfiguracoes.SetActive(false);
        menuCheats.GetComponent<Cheats>().codigoDigitado.text = "";
        menuCheats.SetActive(false);

    }

    public void AbrirReputacaoFimDoDia() {

        reputacaoFimDoDia.SetActive(true);
        inventarioFimDoDia.SetActive(false);
        mercenariosFimDoDia.SetActive(false);

    }

    public void AbrirInventarioFimDoDia()
    {

        reputacaoFimDoDia.SetActive(false);
        inventarioFimDoDia.SetActive(true);
        mercenariosFimDoDia.SetActive(false);
        gameController.inventario.notificacaoInvent.SetActive(false);

    }

    public void AbrirMercenariosFimDoDia()
    {

        reputacaoFimDoDia.SetActive(false);
        inventarioFimDoDia.SetActive(false);
        mercenariosFimDoDia.SetActive(true);
        gameController.mercenarioScript.notificacaoMerc.SetActive(false);

    }

    public void SumirSetas() {

        if(setas.Length > 0) {

            foreach (GameObject seta in setas)
            {
                seta.SetActive(false);
            }
            //indexSeta = 0;
        }

    }

    public void SumirSeta3() {

        setas[2].SetActive(false);
        
    }

    public void AparecerSeta3() {

        if(setas[3].activeSelf) {

            setas[2].SetActive(true);

        }

    }

    public void SumirSeta5() {

        setas[4].SetActive(false);
        
    }

    public void AparecerSeta5() {

        if(setas[5].activeSelf) {

            setas[4].SetActive(true);

        }

    }

    
}
