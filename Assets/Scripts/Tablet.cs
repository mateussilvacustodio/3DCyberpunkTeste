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
}
