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
    [SerializeField] Text[] dinheiroText;
    [SerializeField] Text dinheiroText2;
    public Text dinheiroText3;
    public Text dinheiroText4;
    [SerializeField] Text dinheiroTextFimDoDia;
    [SerializeField] List<EventosFimDeDia> ListaDeEventosFimDeDia = new List<EventosFimDeDia>();
    [SerializeField] List<EventosFimDeDia> ListaDeEventosBackup = new List<EventosFimDeDia>();
    [SerializeField] string[] textoFimDeJogoGangues;
    [Header("Personagens")]
    public GameObject personagemInstancia;
    [SerializeField] List<GameObject> personagensTodos = new List<GameObject>();
    [SerializeField] List<GameObject> personagensTodosBackup = new List<GameObject>();
    [SerializeField] List<GameObject> personagensDisponiveis = new List<GameObject>();
    [SerializeField] GameObject mercador;
    public List<GameObject> personagensDoDia = new List<GameObject>();
    public List<GameObject> personagensDiaSeguinte = new List<GameObject>();
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    public Button botaoTablet;
    public Tablet tabletScript;
    [SerializeField] GameObject menu;
    public TVEffect tVEffectScript;
    public GameObject botaoFimDoDia;
    //[SerializeField] Texture2D cursorSeta;
    //[SerializeField] Vector2 hotspot = Vector2.zero;
    public Image cursorImage;
    [Header("Pedidos")]
    public int dia;
    [SerializeField] float diaMax;
    public float quantidadeDePedidos;
    public float quantidadeDePedidosPorDia;
    [SerializeField] int[] diasMercador; //o dia que o mercador aparece tambem é o dia em que a quantidade de personagens no dia aumenta
    public GameObject painelFimDeDia;
    [SerializeField] Text textoFimDoDia;
    [SerializeField] Text textoEventoFimDoDia;
    [SerializeField] GameObject painelFimDeJogo;
    [SerializeField] Text fimDeJogo;
    [SerializeField] Text textoFimDeJogo;
    [SerializeField] bool GameOver;
    [SerializeField] GameObject painelVitoria;
    [SerializeField] GameObject painelRanking;
    [SerializeField] FirebaseREST firebaseRESTScript;
    [Header("Cores")]
    public Color corTeste;
    public float R;
    public float G;
    [Header("Inventario")]
    public Inventario inventario;
    public bool HaEncomenda;
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
    public AudioSource SFXPassos;
    public AudioSource SFXBotao;
    public AudioSource SFXVoz;
    public AudioSource SFXDinheiroGasto;
    public AudioSource SFXDinheiroPedido;
    [SerializeField] AudioClip musicaJogo;
    [SerializeField] Slider sliderVolume;
    public Slider sliderSFX;
    [SerializeField] TMP_Text volumeSFXTexto;
    [SerializeField] TMP_Text volumeMusicaTexto;
    [Header("Cheat")]
    public bool cheatDavid;
    [Header("Itens")]
    public GameObject[] itemEsgotado;
    [Header("Ciberolho")]
    public float quantidadeCiberOlho;
    public Button botaoCiberOlho;
    [SerializeField] TMP_Text textoQuantidadeCiberOlho;
    public bool ciberOlhoUsado;
    public bool ciberOlhoUsadoNessePedido;
    [SerializeField] GameObject painelOlho;
    public GameObject[] gangueOlho1;
    public TMP_Text gangueOlho1Dinheiro;
    public GameObject[] gangueOlho2;
    public TMP_Text gangueOlho2Dinheiro;
    [Header("Chips")]
    [SerializeField] ParametrosItens parametrosItensScript1;
    public float quantidadeChips;
    public Button botaoChip;
    [SerializeField] TMP_Text textoQuantidadeChip;
    [Header("Multichips")]
    public float quantidadeMultichips;
    public Button botaoMultichip;
    [SerializeField] TMP_Text textoQuantidadeMultichip;
    //public List<GameObject> chips = new List<GameObject>();

    //[Header("Carros")]
    //[SerializeField] GameObject[] carros;
    //[SerializeField] float tempoSpawnCarros;
    //int qualCarro = 1;
    void Start()
    {
        //Cursor.SetCursor(cursorSeta, hotspot, CursorMode.Auto);
        AtualizarGangues();
    }

    void Update()
    {
        Cursor.visible = false;
        cursorImage.transform.position = Input.mousePosition;

        foreach (var item in dinheiroText)
        {
            item.text = "$" + gangues[6].ToString("F0");
        }

        dinheiroText2.text = dinheiroText[0].text;
        dinheiroText3.text = dinheiroText[0].text;
        dinheiroText4.text = dinheiroText[0].text;
        dinheiroTextFimDoDia.text = "Dinheiro atual: " + dinheiroText[0].text;

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

        if (sliderVolume != null)
        {
            if (botaoTablet.gameObject.activeSelf)
            {

                audioSource.volume = sliderVolume.value / 6;
                volumeMusicaTexto.text = (sliderVolume.value * 100).ToString("F0");
                SFXPassos.volume = sliderSFX.value;
                SFXBotao.volume = sliderSFX.value / 4;
                SFXVoz.volume = sliderSFX.value;
                SFXDinheiroGasto.volume = sliderSFX.value;
                SFXDinheiroPedido.volume = sliderSFX.value;
                volumeSFXTexto.text = (sliderSFX.value * 100).ToString("F0");

            }

        }

        if (botaoCiberOlho != null)
        {
            if (quantidadeCiberOlho < 1)
            {
                botaoCiberOlho.GameObject().SetActive(false);
            }
            else
            {
                botaoCiberOlho.GameObject().SetActive(true);
                textoQuantidadeCiberOlho.text = quantidadeCiberOlho.ToString();
            }
        }

        if (botaoChip != null)
        {
            if (quantidadeChips < 1)
            {
                botaoChip.GameObject().SetActive(false);
            }
            else
            {
                botaoChip.GameObject().SetActive(true);
                textoQuantidadeChip.text = quantidadeChips.ToString();
            }
        }

        if (botaoMultichip != null)
        {
            if (quantidadeMultichips < 1)
            {
                botaoMultichip.GameObject().SetActive(false);
            }
            else
            {
                botaoMultichip.GameObject().SetActive(true);
                textoQuantidadeMultichip.text = quantidadeMultichips.ToString();
            }
        }

    }

    public void AtualizarGangues()
    {
        
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

    }

    public void CriarPersonagem2()
    {

        if (personagemInstancia != null)
        {

            Destroy(personagemInstancia);

        }

        int aleatoria = Random.Range(0, personagensDoDia.Count);

        personagemInstancia = Instantiate(personagensDoDia[aleatoria]);
        if (personagemInstancia.GetComponent<Personagem>().nome == "Mercador")
        {
            parametrosItensScript1.AleatoriezarChip();
            print("Aleatoriezei Chips");
        }
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
        SFXBotao.Play();
        painelFimDeDia.SetActive(true);
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

                mercenarioScript.textoFimDoDia.text += "- A missão de " + mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().nomeNPC + " não foi executada - $ " + (mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[6] * 2).ToString() + "\n";
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

        if (cheatDavid)
        {

            quantidadeDePedidosPorDia--;
            cheatDavid = false;

        }

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

        AtualizarGangues();

        foreach (var item in dinheiroText)
        {
            item.text = "$" + gangues[6].ToString("F0");
        }

        botaoFimDoDia.SetActive(false);
        ciberOlhoUsado = false;

    }

    public void ProximoDia()
    {

        SFXBotao.Play();
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
                foreach (var item in diasMercador)
                {
                    if (item == dia)
                    {
                        quantidadeDePedidosPorDia++;
                        personagensDiaSeguinte.Add(mercador);
                        break;
                    }
                }
                foreach (var item in itemEsgotado)
                {
                    item.SetActive(false);
                }
                PreencherListaDoDiaAtual();
                Invoke("CriarPersonagem2", 0.75f);
            }
            else
            {
                painelFimDeDia.SetActive(false);
                painelVitoria.SetActive(true);
            }

        }
        else
        {
            painelFimDeDia.SetActive(false);
            float DiaImpresso = dia - 1;
            fimDeJogo.text = "Fim de jogo no dia " + DiaImpresso.ToString();
            painelFimDeJogo.SetActive(true);
        }

    }

    public void Reiniciar() {

        SceneManager.LoadScene(0);

    }
    public void IniciarJogo()
    {

        SFXBotao.Play();
        menu.SetActive(false);
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

    // public void AbrirConfigurações()
    // {



    // }

    public void AbrirRanking()
    {

        firebaseRESTScript.diasSobrevividos = dia - 1;
        firebaseRESTScript.dinheiroRestante = (int)gangues[6];

        for (int i = 0; i < firebaseRESTScript.listaDeDadosOrdenada.Count; i++)
        {
            if (firebaseRESTScript.diasSobrevividos > firebaseRESTScript.listaDeDadosOrdenada[i].diasDado)
            {

                firebaseRESTScript.HabilitarSubirRanking();
                firebaseRESTScript.seuRecord.text = $"Dias sobrevividos:{firebaseRESTScript.diasSobrevividos}\nDinheiro restante:{firebaseRESTScript.dinheiroRestante}";
                break;

            }

            if (firebaseRESTScript.diasSobrevividos == firebaseRESTScript.listaDeDadosOrdenada[i].diasDado)
            {

                if (firebaseRESTScript.dinheiroRestante > firebaseRESTScript.listaDeDadosOrdenada[i].dinheiroDado)
                {
            
                     firebaseRESTScript.HabilitarSubirRanking();
                    firebaseRESTScript.seuRecord.text = $"Dias sobrevividos:{firebaseRESTScript.diasSobrevividos}\nDinheiro restante:{firebaseRESTScript.dinheiroRestante}";
                    break;   
                    
                }
            }
        }

        painelFimDeDia.SetActive(false);
        painelVitoria.SetActive(false);
        painelRanking.SetActive(true);

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
