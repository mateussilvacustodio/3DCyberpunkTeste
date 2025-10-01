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
    [SerializeField] string descricao;
    [SerializeField] Image ladoA;
    [SerializeField] Image ladoB;
    public Color corA;
    public Color corB;
    [SerializeField] Color[] corLista;
    [SerializeField] string[] gangues;
    public int aleatCor;
    public int aleatCor2;
    public GameObject chipPrefab;

    // Start is called before the first frame update
    void Start()
    {

        AleatoriezarChip();
    }

    // Update is called once per frame
    void Update()
    {

        if (nome == "Chip")
        {
            corA = corLista[aleatCor];
            corB = corA;
            ladoA.color = corA;
            ladoB.color = corB;
            nomeMostrar = "Chip " + gangues[aleatCor];
        }
        else if (nome == "Multichip")
        {
            corA = corLista[aleatCor];
            corB = corLista[aleatCor2];
            ladoA.color = corA;
            ladoB.color = corB;
            nomeMostrar = "Chip " + gangues[aleatCor] + "+" + gangues[aleatCor2];
        }

        campoNome.text = nomeMostrar;
        campoValor.text = "$" + valor.ToString();

    }

    public void MostrarExplicacao()
    {



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
