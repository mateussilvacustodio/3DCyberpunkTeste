using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    [Header("Textos")]
    public string pedido;
    public string opcao1;
    public string opcao2;
    //
    [Header("Recursos")]
    [SerializeField] GameObject ponteiro;
    [SerializeField] GameObject dinheiro;
    //[SerializeField] GameController gameController;
    public float mudadorDinheiro;
    public float mudadorImplantes;
    [Header("Movimento")]
    [SerializeField] float velocidadeMover;
    [SerializeField] bool podeMover;
    [SerializeField] float velocidadeRodar;
    [SerializeField] bool podeRodar;
    [SerializeField] Quaternion rotacaoAlvo;
    [SerializeField] bool irEmbora;
    // Start is called before the first frame update
    void Start()
    {
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(podeMover) {
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * velocidadeMover;
        }

        if(podeRodar) {

            transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoAlvo, velocidadeRodar * Time.deltaTime);

            if(Quaternion.Angle(transform.rotation, rotacaoAlvo) < 0.1f) {

                transform.rotation = rotacaoAlvo;
                podeRodar = false;
                if(irEmbora) {

                    podeMover = true;
                    
                }

            }

        }


        if(Input.GetKeyDown(KeyCode.A)) {

            podeMover = false;
            rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
            podeRodar = true;

        }

        if(Input.GetKeyDown(KeyCode.B)) {

            rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
            podeRodar = true;
            irEmbora = true;

        }

        

        

        //transform.rotation *= Quaternion.Euler(0,2 * velocidadeRodar,0);
        
    }
}
