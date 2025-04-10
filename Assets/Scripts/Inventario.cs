using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item {

    public string nome;
    public int quant;
    public Sprite sprite;

}

public class Inventario : MonoBehaviour
{
    [Header("Itens")]
    //[SerializeField] List<Item> itensPossiveis = new List<Item>(); //lista de todos os itens do jogo
    [SerializeField] List<Item> itensPossuidos = new List<Item>(); //lista e itens
    [SerializeField] GameObject[] espacos;
    public TMP_Text textoEncomendaFimDoDia;
    public TMP_Text textoDevidosFimDoDia;

    [Header("GameController")]
    [SerializeField] GameController gameController;
    [SerializeField] bool tutorial;
    [SerializeField] Tutorial tutorialScript;

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

                    //print("Voce esta devendo item");
                    textoDevidosFimDoDia.text += "- Voce esta devendo: " + itensPossuidos[i].nome + "\n";
                    if(!tutorial) {

                        gameController.personagPraQuemDevo.Add(Resources.Load<GameObject>(gameController.personagemInstancia.name.Replace("(Clone)", "")));

                    } else {

                        gameController.personagPraQuemDevo.Add(Resources.Load<GameObject>(tutorialScript.personagemTutorialInstancia.name.Replace("(Clone)", "")));

                    }
                    
                    //a linha adiciona na list 'personagPraQuemDevo' o prefab da instancia do personagem. o 'Replace' tira a parte '(Clone)' do nome da inst pra buscar na pasta de prefabs
                    itensPossuidos[i].quant = 0;
                }
                //itemEncontrado = true;

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
        gameController.itensEncomendados.Add(PparametrosEncomendas.parametrosItem);

    }

    public void Encomendar() {

        for (int i = 0; i < gameController.itensEncomendados.Count; i++)
        {
            GanharPerderItens(gameController.itensEncomendados[i]); 
            textoEncomendaFimDoDia.text += "- Voce recebeu: " + gameController.itensEncomendados[i].nome + "\n";   
        }
        
        gameController.itensEncomendados.Clear(); //limpa todos os itens da lista

        AtualizarItens();

    }

    public void PagarDivida() {

        if(gameController.personagPraQuemDevo.Count > 0) {//confere se a list de personagens pra quem devo não está vazia

            for (int i = 0; i < gameController.personagPraQuemDevo.Count; i++) { //vai olhar todos os personagens da list 'personagPraQuemDevo'    

                for (int j = 0; j < itensPossuidos.Count; j++) {//pra cada personag, vai comparar com todos os itens da list 'itensPossuidos'
                
                    if(gameController.personagPraQuemDevo[i].GetComponent<Personagem>().itemDoNPC.nome == itensPossuidos[j].nome) {

                        //olhar se a quantidade do item supre o pedido do personagem
                        if(itensPossuidos[j].quant >= Mathf.Abs(gameController.personagPraQuemDevo[i].GetComponent<Personagem>().itemDoNPC.quant)) {

                            print("Tenho itens pra pagar a divida");
                            itensPossuidos[j].quant += gameController.personagPraQuemDevo[i].GetComponent<Personagem>().itemDoNPC.quant;
                            //o valor do item é diminuído no estoque. Como o valor do item no NPC é sempre negativo, coloquei uma soma

                        } else {

                            print("Punição pra você");
                            for (int k = 0; k < gameController.gangues.Length; k++){
        
                                if(gameController.personagPraQuemDevo[i].GetComponent<Personagem>().mudadoresSim[k] > 0) {

                                    print(k);
                                    print(gameController.personagPraQuemDevo[i].GetComponent<Personagem>().mudadoresSim[k] * 2);
                                    gameController.gangues[k] -= gameController.personagPraQuemDevo[i].GetComponent<Personagem>().mudadoresSim[k] * 2;

                                }
                            }

                        }

                    }
                }
            }

            gameController.personagPraQuemDevo.Clear();
            AtualizarItens();

        }

    }

}
