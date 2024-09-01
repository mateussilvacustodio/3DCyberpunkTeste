using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] float dinheiroValor;
    [SerializeField] Text dinheiroText;
    [Header("Personagens")]
    [SerializeField] GameObject[] personagens;
    [SerializeField] int personagemIndex;
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    // Start is called before the first frame update
    void Start()
    {
        botaoSim.onClick.AddListener(personagens[personagemIndex].GetComponent<Personagem>().VaiTeEmbora);
        botaoNao.onClick.AddListener(personagens[personagemIndex].GetComponent<Personagem>().VaiTeEmbora);
    }

    // Update is called once per frame
    void Update()
    {
        dinheiroText.text = dinheiroValor.ToString("F0");
    }
}
