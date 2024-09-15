using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Recursos")]
    public float Gangue1;
    public float Gangue2;
    public float Gangue3;
    public float dinheiroValor;
    [SerializeField] RectTransform ponteiroGangue1;
    [SerializeField] RectTransform ponteiroGangue2;
    [SerializeField] RectTransform ponteiroGangue3;
    [SerializeField] Text dinheiroText;
    [SerializeField] Quaternion rotacaoAlvoPonteiro1;
    [SerializeField] Quaternion rotacaoAlvoPonteiro2;
    [SerializeField] Quaternion rotacaoAlvoPonteiro3;
    [SerializeField] float velocidadeRotacao;
    [SerializeField] bool podeRodar1;
    [SerializeField] bool podeRodar2;
    [SerializeField] bool podeRodar3;
    [Header("Personagens")]
    public GameObject[] personagens;
    public int personagemIndex;
    public GameObject personagemInstancia;
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    // Start is called before the first frame update
    void Start()
    {
        
        personagemInstancia = Instantiate(personagens[personagemIndex]);
        
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);
    }

    // Update is called once per frame
    void Update()
    {
        dinheiroText.text = dinheiroValor.ToString("F0");

        if(podeRodar1) {

            ponteiroGangue1.rotation = Quaternion.Lerp(ponteiroGangue1.rotation, rotacaoAlvoPonteiro1, Time.deltaTime * velocidadeRotacao);
            print("rodar");

            if(Quaternion.Angle(ponteiroGangue1.rotation, rotacaoAlvoPonteiro1) < 0.1f) {

            ponteiroGangue1.rotation = rotacaoAlvoPonteiro1;
            podeRodar1 = false;

            }

        }

        if(podeRodar2) {

            ponteiroGangue2.rotation = Quaternion.Lerp(ponteiroGangue2.rotation, rotacaoAlvoPonteiro2, Time.deltaTime * velocidadeRotacao);
            print("rodar");

            if(Quaternion.Angle(ponteiroGangue2.rotation, rotacaoAlvoPonteiro2) < 0.1f) {

            ponteiroGangue2.rotation = rotacaoAlvoPonteiro2;
            podeRodar2 = false;

            }

        }

        if(podeRodar3) {

            ponteiroGangue3.rotation = Quaternion.Lerp(ponteiroGangue3.rotation, rotacaoAlvoPonteiro3, Time.deltaTime * velocidadeRotacao);
            print("rodar");

            if(Quaternion.Angle(ponteiroGangue3.rotation, rotacaoAlvoPonteiro3) < 0.1f) {

            ponteiroGangue3.rotation = rotacaoAlvoPonteiro3;
            podeRodar3 = false;

            }

        }

    }

    public void criarPersonagem() {

        int aleatoria = Random.Range(0,personagens.Length);

        while(aleatoria == personagemIndex) {

            aleatoria = Random.Range(0,personagens.Length);

        }

        personagemInstancia = Instantiate(personagens[aleatoria]);
        personagemIndex = aleatoria;
        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);

    }

    public void comecarRodar1(float rotacao) {

        rotacaoAlvoPonteiro1 = ponteiroGangue1.rotation * Quaternion.Euler(0,0,rotacao);
        podeRodar1 = true;

    }

    public void comecarRodar2(float rotacao) {

        rotacaoAlvoPonteiro2 = ponteiroGangue2.rotation * Quaternion.Euler(0,0,rotacao);
        podeRodar2 = true;

    }

    public void comecarRodar3(float rotacao) {

        rotacaoAlvoPonteiro3 = ponteiroGangue3.rotation * Quaternion.Euler(0,0,rotacao);
        podeRodar3 = true;

    }


}
