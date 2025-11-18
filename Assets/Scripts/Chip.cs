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
    public string descricao;

    [SerializeField] GameController gameController;
    [SerializeField] GameObject reputacaoAtualizada;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        ladoA.color = corAChip;
        ladoB.color = corBChip;
        nomeCampo.text = nome;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsarChip()
    {
        reputacaoAtualizada.GetComponent<TMP_Text>().text = "Reputação atualizada";
        GameObject reputacaoAtualizadaInstancia = Instantiate(reputacaoAtualizada, gameController.tabletScript.transform);
        Destroy(reputacaoAtualizadaInstancia, 0.75f);
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

    public void MostrarExplicacaoChip()
    {

        gameController.objDescricaoItem.SetActive(true);
        gameController.campoDescricaoItem.text = descricao;

    }
    
    public void SumirExplicacaoChip()
    {
        
        gameController.objDescricaoItem.SetActive(false);

    }
}
