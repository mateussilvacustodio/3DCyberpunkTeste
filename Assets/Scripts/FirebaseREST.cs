using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

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
public class CampoInt {
    public string integerValue;
}

public class FirebaseREST : MonoBehaviour
{

    string bancoDeDadosURL = "https://firestore.googleapis.com/v1/projects/thefixer-17e57/databases/(default)/documents/Jogador";

    string nomePessoa = "Bruna";
    int diasSobrevividos = 3;
    int dinheiroRestante = 200;

    [SerializeField] List<DocumentosFirestore> documentosBD;

    void Start()
    {
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

        string json = $@"
        {{
            ""fields"": {{
                ""Nome"": {{""stringValue"": ""{nomePessoa}""}},
                ""Dias"": {{""integerValue"": ""{diasSobrevividos}""}},
                ""Dinheiro"": {{""integerValue"": ""{dinheiroRestante}""}}
            }}
        }}"; //essa é a string que passa as infos pro banco de dados

        UnityWebRequest requisicao = new UnityWebRequest(bancoDeDadosURL, "POST"); //esse comando cria a requisao pro banco, o POST indica que vai inserir dados
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json); //cria um array de bytes convertendo a string 'json' em bytes para serem enviados
        requisicao.uploadHandler = new UploadHandlerRaw(bodyRaw); //define que o que vai ser enviado na requisicao é o array de bytes criado
        requisicao.downloadHandler = new DownloadHandlerBuffer(); //diz pra Unity que vai receber respostas da API
        requisicao.SetRequestHeader("Content-Type", "application/json"); //define o tipo de dados que estao sendo enviados, para confirmar para o Firebase que é um json

        yield return requisicao.SendWebRequest(); //o retorno aguarda o envio para o banco

        if (requisicao.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Dados do jogador salvos com sucesso");
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

            string jsonResposta = requisicao.downloadHandler.text;  //os dados recebidos são armazenados nessa variavel
            Debug.Log("Json recebido " + jsonResposta);

            var resultadoRequisicao = JsonUtility.FromJson<FirestoreResposta>(jsonResposta); //a string recebida é transformada em objeto da classe FirestoreResposta (criada la em cima)

            documentosBD = resultadoRequisicao.documents;

            foreach (var item in resultadoRequisicao.documents)
            {
                Debug.Log("Nome: " + item.fields.Nome.stringValue + ", Dias sobrevividos: " + item.fields.Dias.integerValue + ", Dinheiro restante: " + item.fields.Dinheiro.integerValue);
            }

        }
        else
        {

            Debug.LogError("Erro ao buscar os dados: " + requisicao.error);

        }

    }
}
