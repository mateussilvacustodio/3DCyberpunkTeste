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
    // Start is called before the first frame update
    void Update() {

        diaAtual.text = "Dia " + gameController.dia;

    }
    
    public void AbrirTablet() {

        painelTablet.SetActive(true);

    }

    public void FecharTablet() {

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

    }

    public void AbrirInventario() {

        menuInicial.SetActive(false);
        menuInventario.SetActive(true);

    }

    public void AbrirMercenarios() {

        menuInicial.SetActive(false);
        menuMercenarios.SetActive(true);

    }

    public void VoltarTelaInicio() {

        menuInicial.SetActive(true);
        menuReputacao.SetActive(false);
        menuInventario.SetActive(false);
        menuMercenarios.SetActive(false);

    }
}
