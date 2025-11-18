using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParametrosItens : MonoBehaviour
{
    [SerializeField] TMP_Text campoNome;
    public string nome;
    public string nomeMostrar;
    [SerializeField] TMP_Text campoValor;
    public float valor;
    [SerializeField] TMP_Text campoDescricao;
    public string descricao;
    [SerializeField] Image ladoA;
    [SerializeField] Image ladoB;
    public Color corA;
    public Color corB;
    [SerializeField] Color[] corLista;
    [SerializeField] string[] ganguesSigla;
    [SerializeField] string[] gangues;
    public int aleatCor;
    public int aleatCor2;
    public GameObject chipPrefab;
    public int indexEsgotadoGameController;

    void Start()
    {

        AleatoriezarChip();
    }

    void Update()
    {

        if (nome == "Chip")
        {
            corA = corLista[aleatCor];
            corB = corA;
            ladoA.color = corA;
            ladoB.color = corB;
            nomeMostrar = "Chip " + ganguesSigla[aleatCor];
            descricao = "Um chip contendo informações sobre a " + gangues[aleatCor] + ". Pode ser usado para chantageá-los, aumentando sua reputação.";
        }
        else if (nome == "Multichip")
        {
            corA = corLista[aleatCor];
            corB = corLista[aleatCor2];
            ladoA.color = corA;
            ladoB.color = corB;
            nomeMostrar = "Chip " + ganguesSigla[aleatCor] + "+" + ganguesSigla[aleatCor2];
            descricao = "Um chip contendo informações sobre a " + gangues[aleatCor] + " e a " + gangues[aleatCor2] + ". Pode ser usado para chantageá-los, aumentando um pouco sua reputação com cada.";
        }

        campoNome.text = nomeMostrar;
        campoValor.text = "$" + valor.ToString();

    }

    public void MostrarExplicacao()
    {

        campoDescricao.text = descricao;

    }

    public void SumirExplicacao()
    {

        campoDescricao.text = "";

    }

    public void AleatoriezarChip()
    {

        aleatCor = Random.Range(0, 6);
        do
        {
            aleatCor2 = Random.Range(0, 6);
        } while (aleatCor2 == aleatCor);

    }
}
