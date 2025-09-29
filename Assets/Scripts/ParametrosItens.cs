using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParametrosItens : MonoBehaviour
{
    [SerializeField] TMP_Text campoNome;
    public string nome;
    [SerializeField] string nomeMostrar;
    [SerializeField] TMP_Text campoValor;
    public float valor;
    [SerializeField] string descricao;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        campoNome.text = nomeMostrar;
        campoValor.text = "$" + valor.ToString();
    }

    public void MostrarExplicacao()
    {
        


    }
}
