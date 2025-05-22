using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item {

    public string nome;
    public int quant;
    public Sprite sprite;
    public float[] modificadores;

}

public class Inventario : MonoBehaviour
{
    [Header("Itens")]
    [SerializeField] List<Item> itensPossuidos = new List<Item>(); //lista de itens
    [SerializeField] List<Item> itensRecebidos = new List<Item>();
    [SerializeField] List<Item> itensEntregues = new List<Item>();
    [SerializeField] List<Item> itensDevidos = new List<Item>();
    [SerializeField] GameObject[] espacos;
    public TMP_Text textoEncomendaFimDoDia;
    public TMP_Text textoEntreguesFimDoDia;
    public TMP_Text textoDevidosFimDoDia;
    public GameObject notificacaoInvent;

    [Header("GameController")]
    [SerializeField] GameController gameController;
    [SerializeField] bool tutorial;
    [SerializeField] Tutorial tutorialScript;

    [SerializeField] GameObject encomendaRealizadaText;
    [SerializeField] GameObject inventarioAba;

    void Start()
    {
        AtualizarItens();     
    }

    public void GanharPerderItens(Item itemRecebido) {

        for (int i = 0; i < itensPossuidos.Count; i++)
        {
                
            if(itemRecebido.nome == itensPossuidos[i].nome){ //o codigo procura o item na lista pelo nome

                itensPossuidos[i].quant += itemRecebido.quant; //e atualiza o valor dele
                if(itensPossuidos[i].quant < 0) { //se após atualizar a quantiade ficar negativa, significa que voce ficou devendo itens para o personag

                    itensDevidos.Add(itemRecebido);
                    if(!tutorial) {

                        //gameController.personagPraQuemDevo.Add(Resources.Load<GameObject>(gameController.personagemInstancia.name.Replace("(Clone)", "")));

                    } else {

                        //gameController.personagPraQuemDevo.Add(Resources.Load<GameObject>(tutorialScript.personagemTutorialInstancia.name.Replace("(Clone)", "")));

                    }
                    
                    //a linha adiciona na list 'personagPraQuemDevo' o prefab da instancia do personagem. o 'Replace' tira a parte '(Clone)' do nome da inst pra buscar na pasta de prefabs
                    itensPossuidos[i].quant = 0;
                }

            }
        }

        AtualizarItens();  

    }

    public void AtualizarItens() {

        for (int i = 0; i < itensPossuidos.Count; i++) //o codigo repete esse loop uma quant de vezes igual a quant de itens que existem na lista de itens possuidos
        {
            espacos[i].transform.Find("Imagem").GetComponent<Image>().sprite = itensPossuidos[i].sprite;
            if(itensPossuidos[i].quant <= 0) {

                espacos[i].transform.Find("Imagem").GetComponent<Image>().color = new Color(1,1,1,0.25f);

            } else {

                espacos[i].transform.Find("Imagem").GetComponent<Image>().color = new Color(1,1,1,1);

            }
            espacos[i].transform.Find("Nome").GetComponent<TMP_Text>().text = itensPossuidos[i].nome;
            espacos[i].transform.Find("Quant").GetComponent<TMP_Text>().text = "X" + itensPossuidos[i].quant.ToString();
        }

    }

    public void AvisoEncomenda(ParametrosEncomendas PparametrosEncomendas) {

        gameController.HaEncomenda = true;
        gameController.gangues[6] -= PparametrosEncomendas.custo;
        //gameController.itensEncomendados.Add(PparametrosEncomendas.parametrosItem);
        itensRecebidos.Add(PparametrosEncomendas.parametrosItem);
        GameObject textoEncomendaRealizada = Instantiate(encomendaRealizadaText, inventarioAba.transform);
        Destroy(textoEncomendaRealizada, 1f);

    }

    public void Encomendar() {

        for (int i = 0; i < itensRecebidos.Count; i++)
        {
            GanharPerderItens(itensRecebidos[i]);
            textoEncomendaFimDoDia.text += "- " + itensRecebidos[i].nome + "\n";
            notificacaoInvent.SetActive(true);
        }
        
        itensRecebidos.Clear(); //limpa todos os itens da lista

        AtualizarItens();

    }

    public void PagarDivida() {

        if(itensDevidos.Count > 0) {

            for (int i = itensDevidos.Count - 1; i >= 0; i--)
            {
                for (int j = itensRecebidos.Count - 1; j >= 0; j--)
                {
                    if(itensDevidos[i].nome == itensRecebidos[j].nome) {

                        itensEntregues.Add(itensDevidos[i]);
                        itensDevidos.RemoveAt(i);
                        itensRecebidos.RemoveAt(j);
                        break;

                    }
                }
            }

        }

        if(itensDevidos.Count > 0) {

            for (int i = 0; i < itensDevidos.Count; i++)
            {
                for (int j = 0; j < gameController.gangues.Length; j++)
                {
                    gameController.gangues[j] += itensDevidos[i].modificadores[j];
                }
                textoDevidosFimDoDia.text += "- " + itensDevidos[i].nome + " - $ " + itensDevidos[i].modificadores[6] + "\n";
                notificacaoInvent.SetActive(true);
            }
            
        }

        itensDevidos.Clear();

        if(itensEntregues.Count > 0) {

            for (int i = 0; i < itensEntregues.Count; i++)
            {
                textoEntreguesFimDoDia.text += "- " + itensEntregues[i].nome + "\n";
                notificacaoInvent.SetActive(true);
            }

        }

        itensEntregues.Clear();

    }

}
