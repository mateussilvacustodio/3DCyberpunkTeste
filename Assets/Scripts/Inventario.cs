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
    [SerializeField] List<Item> itensPossiveis = new List<Item>(); //lista de todos os itens do jogo
    [SerializeField] List<Item> itensPossuidos = new List<Item>(); //lista de itens que o jogador possui
    [SerializeField] GameObject[] espacos;

    [Header("GameController")]
    [SerializeField] GameController gameController;

    void Start()
    {
        AtualizarItens();
               
    }

    public void GanharPerderItens(Item itemRecebido) {

        //bool itemEncontrado = false;

        for (int i = 0; i < itensPossuidos.Count; i++)
        {
                
            if(itemRecebido.nome == itensPossuidos[i].nome){ //o codigo procura o item na lista pelo nome

                itensPossuidos[i].quant += itemRecebido.quant; //e atualiza o valor dele
                //itemEncontrado = true;

            }
        }

        AtualizarItens();  

        //if(!itemEncontrado) {

            //  print("PenalidadeNoJogador");
            //gameController.personagemInstancia.GetComponent<Personagem>().teste = true;


        //}
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

    public void AvisoEncomenda() {

        gameController.HaEncomenda = true;

    }

    public void Encomendar() {

        print("Houve uma encomenda");
        //itensPossuidos[parametrosEncomendas.itemPraEncomendar].quant += 1;
        //gameController.gangues[6] -= parametrosEncomendas.custo;
        //AtualizarItens();

    }
}
