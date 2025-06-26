using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;
using TMPro;

[System.Serializable]
public class EventosFimDeDia {

    public string textoDoEvento;
    public int quantasGanguesAfeta;
    public int[] gangueQueAfeta;
    public float[] quantoAfetaNaGangue;

}

public class GameController : MonoBehaviour
{
    [Header("Recursos")]
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public float[] gangues;
    [SerializeField] Image[] barrasGangues;
    [SerializeField] Image[] barrasGangues2;
    [SerializeField] Text[] barrasGanguesPCT;
    [SerializeField] Text[] barrasGanguesPCT2;
    [SerializeField] Text dinheiroText;
    [SerializeField] Text dinheiroText2;
    [SerializeField] Text dinheiroText3;
    [SerializeField] Text dinheiroText4;
    [SerializeField] Text dinheiroTextFimDoDia;
    [SerializeField] List<EventosFimDeDia> ListaDeEventosFimDeDia = new List<EventosFimDeDia>();
    [SerializeField] string[] textoFimDeJogoGangues;
    [Header("Personagens")]
    //[SerializeField] List<int> indicesDisponiveis = new List<int>();
    //public int personagemIndex;
    public GameObject personagemInstancia;
    [SerializeField] List<GameObject> personagensTodos = new List<GameObject>();
    [SerializeField] List<GameObject> personagensDisponiveis = new List<GameObject>();
    [SerializeField] List<GameObject> personagensDoDia = new List<GameObject>();
    public List<GameObject> personagensDiaSeguinte = new List<GameObject>();
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    public Button botaoTablet;
    [SerializeField] Tablet tabletScript;
    [SerializeField] GameObject botaoIniciar;
    [SerializeField] GameObject botaoTutorial;
    //[SerializeField] GameObject botaoConfig;
    public TVEffect tVEffectScript;
    [SerializeField] GameObject botaoFimDoDia;
    [Header("Pedidos")]
    public float dia;
    [SerializeField] float diaMax;
    public float quantidadeDePedidos;
    public float quantidadeDePedidosPorDia;
    [SerializeField] GameObject painelFimDeDia;
    [SerializeField] Text textoFimDoDia;
    [SerializeField] Text textoEventoFimDoDia;
    [SerializeField] GameObject painelFimDeJogo;
    [SerializeField] Text fimDeJogo;
    [SerializeField] Text textoFimDeJogo;
    [SerializeField] bool GameOver;
    [SerializeField] GameObject painelVitoria;
    [Header("Cores")]
    public Color corTeste;
    public float R;
    public float G;
    [Header("Inventario")]
    public Inventario inventario;
    public bool HaEncomenda;
    //public List<Item> itensEncomendados = new List<Item>();
    //public List<GameObject> personagPraQuemDevo = new List<GameObject>();
    public ParametrosEncomendas parametrosEncomendasGC;
    [Header("Mercenarios")]
    public Mercenarios mercenarioScript;
    [SerializeField] bool HaMissoesDeMercenario;
    [SerializeField] GameObject notificacao1;
    [SerializeField] Text notificacao1Text;
    [SerializeField] GameObject notificacao2;
    [SerializeField] Text notificacao2Text;
    public int numNotificacao;
    [SerializeField] GameObject[] indisponiveis;
    [Header("Musicas")]
    [SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip musicaMenu;
    [SerializeField] AudioClip musicaJogo;
    [SerializeField] Slider sliderVolume;
    [SerializeField] TMP_Text volumeMusicaTexto;
    [Header("Playtest")]
    bool contarTempo;
    public float tempoDeJogo;
    //[Header("Carros")]
    //[SerializeField] GameObject[] carros;
    //[SerializeField] float tempoSpawnCarros;
    //int qualCarro = 1;
    void Start()
    {
        contarTempo = true;
        tempoDeJogo = PlayerPrefs.GetFloat("tempoJogo");

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            R = (100 - gangues[i]) / 50;
            G = gangues[i] / 50;
            corTeste = new Color(R, G, 0f, 1f);
            barrasGangues[i].color = corTeste;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        }

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].color = barrasGangues[i].color;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT2[i].text = barrasGanguesPCT[i].text;
        }


        // for (int i = 0; i < personagens.Length; i++) {

        //     indicesDisponiveis.Add(i);

        // }

        //criarPersonagem1();
    }

    void Update()
    {
        dinheiroText.text = "$ " + gangues[6].ToString("F0");
        dinheiroText2.text = dinheiroText.text;
        dinheiroText3.text = dinheiroText.text;
        dinheiroText4.text = dinheiroText.text;
        dinheiroTextFimDoDia.text = "Dinheiro atual: " + dinheiroText.text;

        if (numNotificacao > 0)
        {

            notificacao1.SetActive(true);
            notificacao2.SetActive(true);
            notificacao1Text.text = numNotificacao.ToString();
            notificacao2Text.text = numNotificacao.ToString();

        }

        if (numNotificacao <= 0)
        {

            notificacao1.SetActive(false);
            notificacao2.SetActive(false);

        }

        if (contarTempo)
        {

            tempoDeJogo += Time.deltaTime;

        }

        if (sliderVolume != null)
        {
            
            audioSource.volume = sliderVolume.value;
            volumeMusicaTexto.text = (sliderVolume.value*100).ToString("F0");

        }

    }

    public void CriarPersonagem2()
    {

        if (personagemInstancia != null)
        {

            Destroy(personagemInstancia);

        }

        int aleatoria = Random.Range(0, personagensDoDia.Count);

        personagemInstancia = Instantiate(personagensDoDia[aleatoria]);
        //personagemIndex = aleatoria;
        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);

        personagensDoDia.RemoveAt(aleatoria);

    }

    public void PreencherListaDoDiaAtual()
    {

        for (int i = 0; i < personagensDiaSeguinte.Count; i++)
        {
            //print("Adicionado um personagem do dia seguinte");
            personagensDoDia.Add(personagensDiaSeguinte[i]); //adiciona na lista de personagens do dia todos os que devem aparecer no dia seguinte
        }

        personagensDiaSeguinte.Clear(); //limpa a lista de personagens do dia seguinte

        if (personagensDoDia.Count < quantidadeDePedidosPorDia)
        {//se a lista de personagens para o dia não for totalmente preenchida...

            float diferenca = quantidadeDePedidosPorDia - personagensDoDia.Count;
            for (int i = 0; i < diferenca; i++)
            {
                if (personagensDisponiveis.Count == 0)
                {

                    for (int j = 0; j < personagensTodos.Count; j++)
                    {
                        personagensDisponiveis.Add(personagensTodos[j]);
                    }

                }

                int aleatoria = Random.Range(0, personagensDisponiveis.Count);
                bool jaExiste = personagensDoDia.Any(p => p.name == personagensDisponiveis[aleatoria].name);

                while (jaExiste)
                {

                    aleatoria = Random.Range(0, personagensDisponiveis.Count);
                    jaExiste = personagensDoDia.Any(p => p.name == personagensDisponiveis[aleatoria].name);

                }

                personagensDoDia.Add(personagensDisponiveis[aleatoria]); //... são adicionados mais até completarem a quantidade de pedido max no dia
                personagensDisponiveis.RemoveAt(aleatoria);

            }

        }

    }

    public void FimDoDia()
    {

        numNotificacao = 0;

        foreach (GameObject item in indisponiveis)
        {
            item.SetActive(false);
        }

        inventario.PagarDivida();

        if (HaEncomenda)
        {

            inventario.Encomendar();

        }

        for (int i = 0; i < mercenarioScript.pedidosAceitos.Count; i++)
        {
            if (mercenarioScript.pedidosAceitos[i] != null)
            {

                for (int j = 0; j < gangues.Length; j++)
                {
                    gangues[j] -= mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[j] * 2;
                }

                mercenarioScript.textoFimDoDia.text += "A missão de " + mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().nomeNPC + " não foi executada - $ " + (mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[6] * 2).ToString() + "\n";
                mercenarioScript.notificacaoMerc.SetActive(true);
                Destroy(mercenarioScript.pedidosAceitos[i]);

            }
        }

        for (int i = 0; i < mercenarioScript.pedidosFalhos.Count; i++)
        {
            for (int j = 0; j < gangues.Length; j++)
            {
                gangues[j] -= mercenarioScript.pedidosFalhos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[j] * 2;
            }

            Destroy(mercenarioScript.pedidosFalhos[i]);
        }

        mercenarioScript.pedidosFalhos.Clear();

        botaoTablet.interactable = false;
        tabletScript.FecharTablet();
        dia++;
        float DiaImpresso = dia - 1;
        textoFimDoDia.text = "Fim do dia " + DiaImpresso.ToString();

        gangues[6] -= 100;
        int eventoAleatorio = Random.Range(0, ListaDeEventosFimDeDia.Count);
        //int eventoAleatorio = 0;
        textoEventoFimDoDia.text = ListaDeEventosFimDeDia[eventoAleatorio].textoDoEvento;


        for (int i = 0; i < gangues.Length; i++)
        {

            for (int j = 0; j < ListaDeEventosFimDeDia[eventoAleatorio].quantasGanguesAfeta; j++)
            {

                if (i == ListaDeEventosFimDeDia[eventoAleatorio].gangueQueAfeta[j])
                {

                    gangues[i] += ListaDeEventosFimDeDia[eventoAleatorio].quantoAfetaNaGangue[j];

                }
            }

        }

        ListaDeEventosFimDeDia.RemoveAt(eventoAleatorio);

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        dinheiroText.text = gangues[6].ToString("F0");
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            R = (100 - gangues[i]) / 50;
            G = gangues[i] / 50;
            corTeste = new Color(R, G, 0f, 1f);
            barrasGangues[i].color = corTeste;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        }

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].color = barrasGangues[i].color;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT2[i].text = barrasGanguesPCT[i].text;
        }

        painelFimDeDia.SetActive(true);
        botaoFimDoDia.SetActive(false);

    }

    public void ProximoDia()
    {

        inventario.textoEncomendaFimDoDia.text = "";
        inventario.textoEntreguesFimDoDia.text = "";
        inventario.textoDevidosFimDoDia.text = "";
        mercenarioScript.textoFimDoDia.text = "";
        inventario.notificacaoInvent.SetActive(false);
        mercenarioScript.notificacaoMerc.SetActive(false);

        for (int i = 0; i < gangues.Length; i++) //confere o valor de todas as gangues do jogo (incluindo o dinheiro)
        {
            if (gangues[i] <= 0)
            { //se alguma for menor que zero...

                GameOver = true; //... o jogo acaba...
                textoFimDeJogo.text += "Você perdeu porque ficou sem " + textoFimDeJogoGangues[i] + "\n";

                //...e se o jogad ficar sem reputação com mais de uma gangue, mais de uma mensagem será exibida
            }
        }

        if (!GameOver)
        {

            if (dia < diaMax + 1)
            {

                AtivarEfeitoTV();
                painelFimDeDia.SetActive(false);
                tabletScript.AbrirReputacaoFimDoDia();
                botaoTablet.interactable = true;
                PreencherListaDoDiaAtual();
                Invoke("CriarPersonagem2", 0.75f);

            }
            else
            {

                painelFimDeDia.SetActive(false);
                painelVitoria.SetActive(true);
                float minuto = 0;
                while (tempoDeJogo >= 60)
                {
                    minuto++;
                    tempoDeJogo -= 60;
                }
                print("Sua gameplay durou " + minuto.ToString("00") + ":" + tempoDeJogo.ToString("00") + " minutos");
                contarTempo = false;
                tempoDeJogo = 0;
                PlayerPrefs.SetFloat("tempoJogo", tempoDeJogo);
                PlayerPrefs.Save();

            }

        }
        else
        {
            painelFimDeDia.SetActive(false);
            float DiaImpresso = dia - 1;
            fimDeJogo.text = "Fim de jogo no dia " + DiaImpresso.ToString();
            painelFimDeJogo.SetActive(true);
            float minuto = 0;
            while (tempoDeJogo >= 60)
            {
                minuto++;
                tempoDeJogo -= 60;
            }
            print("Sua gameplay durou " + minuto.ToString("00") + ":" + tempoDeJogo.ToString("00") + " minutos");
            contarTempo = false;
            tempoDeJogo = 0;
            PlayerPrefs.SetFloat("tempoJogo", tempoDeJogo);
            PlayerPrefs.Save();
        }

    }

    public void Reiniciar()
    {

        SceneManager.LoadScene(0);

    }
    public void IniciarJogo()
    {

        botaoIniciar.SetActive(false);
        botaoTutorial.SetActive(false);
        //botaoConfig.SetActive(false);
        botaoTablet.gameObject.SetActive(true);
        PreencherListaDoDiaAtual();
        Invoke("AtivarEfeitoTV", 0.25f);
        Invoke("CriarPersonagem2", 0.75f);
        //StartCoroutine(CriarCarros());
        audioSource.clip = musicaJogo;
        audioSource.Play();
        //trocar musica

    }
    public void IniciarTutorial()
    {

        SceneManager.LoadScene(1);
        //trocar musica

    }

    void AtivarEfeitoTV()
    {

        tVEffectScript.tvEffectAnim.SetTrigger("Abrir");

    }

    public void AbrirConfigurações()
    {
        


    }

    // IEnumerator CriarCarros()
    // {

    //     yield return new WaitForSeconds(tempoSpawnCarros + Random.Range(0, 1));
    //     GameObject carroInstancia = Instantiate(carros[qualCarro]);

    //     if (qualCarro == 1)
    //     {
    //         qualCarro = 0;
    //     }
    //     else if (qualCarro == 0)
    //     {

    //         qualCarro = 1;

    //     }

    //     Destroy(carroInstancia, 2.5f);
    //     StartCoroutine(CriarCarros());

    // }

}
