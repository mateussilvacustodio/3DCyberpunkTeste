using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParametrosEncomendas : MonoBehaviour
{
    [Header("Encomenda de itens")]
    public Item parametrosItem;
    public float custo;

    [Header("Textos")]
    [SerializeField] TMP_Text Preco;

    void Update()
    {
        Preco.text = "$ " + custo.ToString();
    }

    // public void mudarTexto() {

    //     textoNoBotao.text = "$ " + custo.ToString();
    //     textoNoBotao.color = new Color32(255, 220, 0, 255);

    // }

    // public void voltarTexto() {

    //     textoNoBotao.text = "Encomendar";
    //     textoNoBotao.color = new Color32(50, 50, 50, 255);


    // }

}
