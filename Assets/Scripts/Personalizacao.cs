using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Personalizacao : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("GameController")]
    [SerializeField] GameController gameController;
    [Header("Menus")]
    [SerializeField] GameObject inicio;
    [SerializeField] GameObject luzes;
    [SerializeField] GameObject decoracoes;
    [SerializeField] GameObject itens;
    [Header("Personalização")]
    [SerializeField] Image LuzNeonImagem;
    [SerializeField] RawImage rodaCores;
    [SerializeField] RectTransform bolinhaCursor;
    [SerializeField] Image bolinhaImage;
    [SerializeField] Texture2D colorTexture;
    [Header("CiberOlho")]
    [SerializeField] GameObject painelCiberOlho;
    [SerializeField] GameObject desejaUsarCiberOlho;
    [SerializeField] GameObject ciberOlhoUsado;
    void Start()
    {
        colorTexture = rodaCores.texture as Texture2D;
    }

    public void FecharPainel()
    {

        luzes.SetActive(false);
        itens.SetActive(false);
        decoracoes.SetActive(false);
        inicio.SetActive(true);
        this.gameObject.SetActive(false);

    }

    public void AbrirLuzes()
    {

        inicio.SetActive(false);
        luzes.SetActive(true);

    }

    public void AbrirDecoracoes()
    {

        inicio.SetActive(false);
        decoracoes.SetActive(true);

    }

    public void AbrirItens()
    {

        inicio.SetActive(false);
        itens.SetActive(true);

    }

    public void AbrirInicio()
    {

        luzes.SetActive(false);
        itens.SetActive(false);
        decoracoes.SetActive(false);
        inicio.SetActive(true);


    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (luzes.activeSelf)
        {

            PegarCor(eventData);

        }

    }

    public void OnDrag(PointerEventData eventData)
    {

        if (luzes.activeSelf)
        {

            PegarCor(eventData);

        }

    }

    void PegarCor(PointerEventData eventData)
    {

        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rodaCores.rectTransform, eventData.position, eventData.pressEventCamera, out localCursor))
        {
            return;

        }

        Rect rect = rodaCores.rectTransform.rect;
        float x = Mathf.Clamp01((localCursor.x - rect.x) / rect.width);
        float y = Mathf.Clamp01((localCursor.y - rect.y) / rect.height);

        int texX = Mathf.RoundToInt(x * colorTexture.width);
        int texY = Mathf.RoundToInt(y * colorTexture.height);

        if (texX < 0 || texX >= colorTexture.width || texY < 0 || texY >= colorTexture.height)
        {

            return;

        }

        Color corPega = colorTexture.GetPixel(texX, texY);

        if (corPega.a < 0.1f)
        {

            return;

        }

        LuzNeonImagem.color = corPega;
        if (bolinhaCursor != null)
        {

            bolinhaCursor.localPosition = localCursor;
            bolinhaImage.color = corPega;

        }

    }

    public void ComprarItem(ParametrosItens pParametrosItem)
    {

        gameController.gangues[6] -= pParametrosItem.valor;

        switch (pParametrosItem.nome)
        {
            case "Ciber olho":
                gameController.quantidadeCiberOlho += 1;
                break;

            case "Chip":
                print("chip");
                break;

            case "Multichip":
                print("multichip");
                break;

        }

    }

    public void AbrirPainelOlho()
    {
        painelCiberOlho.SetActive(true);
        if (gameController.ciberOlhoUsado)
        {
            desejaUsarCiberOlho.SetActive(false);
            ciberOlhoUsado.SetActive(true);
        }
    }

    public void UsarCiberOlho()
    {
        desejaUsarCiberOlho.SetActive(false);
        ciberOlhoUsado.SetActive(true);
        //gameController.quantidadeCiberOlho -= 1;
        gameController.ciberOlhoUsado = true;
        gameController.ciberOlhoUsadoNessePedido = true;
    }

    public void FecharPainelOlho()
    {
        desejaUsarCiberOlho.SetActive(true);
        ciberOlhoUsado.SetActive(false);
        painelCiberOlho.SetActive(false);
    }
}
