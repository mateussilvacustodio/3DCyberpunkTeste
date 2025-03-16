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
    [SerializeField] List<Item> itensPossiveis = new List<Item>(); //lista de todos os itens do jogo
    [SerializeField] List<Item> itensPossuidos = new List<Item>(); //lista de itens que o jogador possui
    [SerializeField] GameObject[] espacos;

    void Start()
    {
        //pai.transform.Find("Imagem").GetComponent<Image>().sprite = teste;
        for (int i = 0; i < itensPossuidos.Count; i++)
        {
            espacos[i].transform.Find("Imagem").GetComponent<Image>().sprite = itensPossuidos[i].sprite;
            espacos[i].transform.Find("Imagem").GetComponent<Image>().color = new Color(255,255,255,255);
            espacos[i].transform.Find("Nome").GetComponent<TMP_Text>().text = itensPossuidos[i].nome;
            espacos[i].transform.Find("Quant").GetComponent<TMP_Text>().text = "X" + itensPossuidos[i].quant.ToString();
        }
        
        //GanharPerderItens(itensPossiveis[3]);
               
    }
    void Update()
    {
        
    }

    public void GanharPerderItens(Item itemRecebido) {

        bool itemEncontrado = false;
        bool deletarItem = false;
        int indiceDeletar = 0;
        
        if(itemRecebido.quant > 0) { //confere se o valor do item no personagem é positivo

            for (int i = 0; i < itensPossuidos.Count; i++)
            {
                
                if(itemRecebido.nome == itensPossuidos[i].nome){ //o codigo olha toda a lista de item pra ver se voce ja o possui

                    itensPossuidos[i].quant += itemRecebido.quant; //se voce tiver, ele somente atualiza o valor do item na lista
                    itemEncontrado = true;

                }
            }

            if(!itemEncontrado) { //se a variavel for falsa, significa q o item n foi achado...

                itensPossuidos.Add(itemRecebido); //... entao ele é adicionado

            }


        } else if(itemRecebido.quant < 0) { //confere se o valor do item é negativo

            for (int i = 0; i < itensPossuidos.Count; i++)
            {
                
                if(itemRecebido.nome == itensPossuidos[i].nome){ //o codigo olha toda a lista de item pra ver se voce ja o possui

                    //conferir se ele NÃO tem o item
                    itensPossuidos[i].quant += itemRecebido.quant; //se tiver ele adiciona o valor negativo

                    if(itensPossuidos[i].quant <= 0) { //se isso zerar o valor...
   
                        deletarItem = true;
                        indiceDeletar = i;

                    }

                }
            }

            if(deletarItem) {

                itensPossuidos.RemoveAt(indiceDeletar); //... ele deleta o item

            }

        }

        if(itensPossuidos.Count <= 0) {

            espacos[0].transform.Find("Imagem").GetComponent<Image>().sprite = null;
            espacos[0].transform.Find("Imagem").GetComponent<Image>().color = new Color(255,255,255,0);
            espacos[0].transform.Find("Nome").GetComponent<TMP_Text>().text = "";
            espacos[0].transform.Find("Quant").GetComponent<TMP_Text>().text = "";

        } else {

            for (int i = 0; i < itensPossuidos.Count; i++)
            {
                espacos[i].transform.Find("Imagem").GetComponent<Image>().sprite = itensPossuidos[i].sprite;
                espacos[i].transform.Find("Imagem").GetComponent<Image>().color = new Color(255,255,255,255);
                espacos[i].transform.Find("Nome").GetComponent<TMP_Text>().text = itensPossuidos[i].nome;
                espacos[i].transform.Find("Quant").GetComponent<TMP_Text>().text = "X" + itensPossuidos[i].quant.ToString();
            }

            for (int i = itensPossuidos.Count; i < espacos.Length; i++)
            {
                espacos[i].transform.Find("Imagem").GetComponent<Image>().sprite = null;
                espacos[i].transform.Find("Imagem").GetComponent<Image>().color = new Color(255,255,255,0);
                espacos[i].transform.Find("Nome").GetComponent<TMP_Text>().text = "";
                espacos[i].transform.Find("Quant").GetComponent<TMP_Text>().text = "";
            }

        }

        //itensPossuidos[itensPossuidos.Count - 1].quant += 1; //acessa o ultimo item da lista e aumenta sua quantidade

    }
}
