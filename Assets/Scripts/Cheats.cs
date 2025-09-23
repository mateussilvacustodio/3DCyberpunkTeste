using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] List<string> codigosSecretos = new List<string>();
    public TMP_InputField codigoDigitado;
    bool cheatConfirmado;
    [SerializeField] GameObject cheatConfirmadoText;
    [SerializeField] GameObject codigoIncorretoText;
    [SerializeField] GameObject cheatsAba;
    [SerializeField] GameObject david;

    [Header("GameController")]
    [SerializeField] GameController gameController;

    public void ConfirmarCheat() {
        
        for (int i = 0; i < codigosSecretos.Count; i++)
        {

            if (codigoDigitado.text == codigosSecretos[i])
            {

                codigosSecretos.RemoveAt(i);
                cheatConfirmado = true;
            }

        }
        
        if (cheatConfirmado) {

            switch (codigoDigitado.text)
            {
                case "Ali":
                    Debug.Log("Ali");
                    break;
                case "Bruna":
                    gameController.gangues[6] += 7500;
                    break;
                case "Felipe":
                    if (!gameController.botaoFimDoDia.activeSelf)
                    {
                        gameController.personagensDoDia.Add(david);
                        gameController.quantidadeDePedidosPorDia++;
                    }
                    else
                    {
                        print("O botao esta ativado");
                        gameController.personagensDiaSeguinte.Add(david);
                        gameController.quantidadeDePedidosPorDia++;
                    }
                    
                    break;
                case "Leonardo":
                    Debug.Log("Leonardo");
                    break;
                case "Mateus":
                    Debug.Log("Mateus");
                    break;
            }
            GameObject textoCheatConfirmado = Instantiate(cheatConfirmadoText, cheatsAba.transform);
            Destroy(textoCheatConfirmado, 0.75f);

        } else {

            if (codigoDigitado.text != "") {

                GameObject textoCodigoIncorreto = Instantiate(codigoIncorretoText, cheatsAba.transform);
                Destroy(textoCodigoIncorreto, 0.75f);
            }

        }

        codigoDigitado.text = "";
        cheatConfirmado = false;

    }
}
