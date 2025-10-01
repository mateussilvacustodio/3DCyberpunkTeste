using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    [SerializeField] TMP_Text nomeCampo;
    public string nome;
    [SerializeField] Image ladoA;
    [SerializeField] Image ladoB;
    public Color corAChip;
    public Color corBChip;
    public int gangueAfetada;
    public int gangueAfetada2;
    public float valorAfetada;
    public float valorAfetada2;

    [SerializeField] GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        ladoA.color = corAChip;
        ladoB.color = corBChip;
        nomeCampo.text = nome;
    }

    public void UsarChip()
    {
        gameController.gangues[gangueAfetada] += valorAfetada;
        gameController.gangues[gangueAfetada2] += valorAfetada2;
        gameController.AtualizarGangues();
        if (corAChip == corBChip)
        {
            gameController.quantidadeChips -= 1;
        }
        else
        {
            gameController.quantidadeMultichips -= 1;
        }
        
        Destroy(this.GameObject());
    }
}
