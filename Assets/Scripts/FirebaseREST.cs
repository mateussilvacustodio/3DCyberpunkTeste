using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class FirestoreResposta //a string recebida do banco é transformada em um objeto dessa classe
{
    public List<DocumentosFirestore> documents;
}

[System.Serializable]
public class DocumentosFirestore
{
    public string name;
    public CamposFirestore fields;
}

[System.Serializable]
public class CamposFirestore
{
    public CampoString Nome;
    public CampoInt Dias;
    public CampoInt Dinheiro;
}

[System.Serializable]
public class CampoString {
    public string stringValue;
}

[System.Serializable]
public class CampoInt
{
    public string integerValue;
}

[System.Serializable]
public class Dado
{
    public string nomeDado;
    public float diasDado;
    public float dinheiroDado;
}

public class FirebaseREST : MonoBehaviour
{
    string bancoDeDadosURL = "https://firestore.googleapis.com/v1/projects/thefixer-17e57/databases/(default)/documents/Jogador";

    public string nomePessoa;
    public int diasSobrevividos;
    public int dinheiroRestante;

    [Header("Listas")]
    [SerializeField] List<DocumentosFirestore> documentosBD;
    [SerializeField] List<Dado> listaDeDados = new List<Dado>();
    public List<Dado> listaDeDadosOrdenada = new List<Dado>();

    [Header("TextosDoRanking")]
    [SerializeField] GameObject adicionarRanking;
    [SerializeField] Text listaNomes;
    [SerializeField] Text listaDias;
    [SerializeField] Text listaDinheiro;
    public Text seuRecord;
    [SerializeField] TMP_InputField nomePreenchido;
    [SerializeField] GameObject atualizandoRanking;
    [SerializeField] GameObject rankingAtualizado;

    void Start()
    {

        nomePreenchido.onValueChanged.AddListener(FormatarNome);
        //SalvarJogador();
        LerJogadores();
    }

    public void SalvarJogador()
    {
        StartCoroutine(EnviarDadosBancoDeDados());
    }

    public void LerJogadores()
    {
        StartCoroutine(BuscarDadosBancoDeDados());
    }

    IEnumerator EnviarDadosBancoDeDados()
    {

        string urlEscrita = $"{bancoDeDadosURL}/{nomePessoa}"; //muda a url para criar um documento com o nome da pessoa na colecao Jogador do banco de dados

        string json = $@"
        {{
            ""fields"": {{
                ""Nome"": {{""stringValue"": ""{nomePessoa}""}},
                ""Dias"": {{""integerValue"": {diasSobrevividos}}},
                ""Dinheiro"": {{""integerValue"": {dinheiroRestante}}}
            }}
        }}"; //essa é a string que passa as infos pro banco de dados

        UnityWebRequest requisicao = new UnityWebRequest(urlEscrita, "PATCH"); //esse comando cria a requisao pro banco, o PATCH indica que vai inserir dados ou substituilos se ja existirem
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json); //cria um array de bytes convertendo a string 'json' em bytes para serem enviados
        requisicao.uploadHandler = new UploadHandlerRaw(bodyRaw); //define que o que vai ser enviado na requisicao é o array de bytes criado
        requisicao.downloadHandler = new DownloadHandlerBuffer(); //diz pra Unity que vai receber respostas da API
        requisicao.SetRequestHeader("Content-Type", "application/json"); //define o tipo de dados que estao sendo enviados, para confirmar para o Firebase que é um json

        yield return requisicao.SendWebRequest(); //o retorno aguarda o envio para o banco

        if (requisicao.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Dados do jogador salvos com sucesso");
            yield return StartCoroutine(BuscarDadosBancoDeDados());
        }
        else
        {
            Debug.LogError("Erro ao salvar dados do jogador " + requisicao.error);
            Debug.LogError("Resposta da API " + requisicao.downloadHandler.text);
        }

    }

    IEnumerator BuscarDadosBancoDeDados()
    {

        UnityWebRequest requisicao = UnityWebRequest.Get(bancoDeDadosURL); //essa requisicao acessa o banco. O Get pega todos os dados que estão no banco
        requisicao.SetRequestHeader("Content-Type", "application/json");

        yield return requisicao.SendWebRequest();

        if (requisicao.result == UnityWebRequest.Result.Success)
        {

            Debug.Log("Dados recebidos com sucesso");
            if (atualizandoRanking.activeSelf)
            {

                atualizandoRanking.SetActive(false);
                rankingAtualizado.SetActive(true);

            }

            string jsonResposta = requisicao.downloadHandler.text;  //os dados recebidos são armazenados nessa variavel
            //Debug.Log("Json recebido " + jsonResposta);

            var resultadoRequisicao = JsonUtility.FromJson<FirestoreResposta>(jsonResposta); //a string recebida é transformada em objeto da classe FirestoreResposta (criada la em cima)

            documentosBD = resultadoRequisicao.documents;

            foreach (var dado in documentosBD)
            {

                Dado novoDado = new Dado();

                novoDado.nomeDado = dado.fields.Nome.stringValue;
                novoDado.diasDado = float.Parse(dado.fields.Dias.integerValue); //float.Parse() converte o q tá dentro do () pra float. Funciona tbm com Int
                novoDado.dinheiroDado = float.Parse(dado.fields.Dinheiro.integerValue);

                listaDeDados.Add(novoDado);
            }

            OrdenarDados();
            ImprimirDados();
            listaDeDados.Clear();

        }
        else
        {

            Debug.LogError("Erro ao buscar os dados: " + requisicao.error);

        }

    }

    void OrdenarDados()
    {

        listaDeDadosOrdenada = listaDeDados.OrderByDescending(d => d.diasDado).ThenByDescending(d => d.dinheiroDado).Take(7).ToList();
        //adiciona na nova lista a lsta anterior ordenada com um limite de 7

    }

    void ImprimirDados()
    {

        listaNomes.text = "";
        listaDias.text = "";
        listaDinheiro.text = "";

        for (int i = 0; i < listaDeDadosOrdenada.Count; i++)
        {
            listaNomes.text += listaDeDadosOrdenada[i].nomeDado + "\n";
            listaDias.text += listaDeDadosOrdenada[i].diasDado + "\n";
            listaDinheiro.text += "$ " + listaDeDadosOrdenada[i].dinheiroDado + "\n";
        }

    }

    public void HabilitarSubirRanking()
    {

        adicionarRanking.SetActive(true);

    }

    public void AtivarSalvarDados()
    {

        nomePessoa = nomePreenchido.text;
        adicionarRanking.SetActive(false);
        atualizandoRanking.SetActive(true);
        SalvarJogador();

    }

    public void FormatarNome(string valor)
    {

        if (string.IsNullOrEmpty(valor))
        {
            return;

        }

        if (valor.Length > 10)
        {

            valor = valor.Substring(0, 10);

        }

        valor = char.ToUpper(valor[0]) + valor.Substring(1).ToLower();

        nomePreenchido.text = valor;

    }

    void TesteImpressao()
    {

        print("Imprimi");

    }
}
