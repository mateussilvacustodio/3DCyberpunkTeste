using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class TesteDeListas : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool podeMover;
    [SerializeField] bool podeRodar;
    Quaternion rotacaoAlvo = Quaternion.Euler(0, 180, 0);
    [SerializeField] bool irEmbora;

    [SerializeField] float teste1;
    [SerializeField] float teste2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(podeMover) {
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * 0.5f;
        }

        if(podeRodar) {

            transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoAlvo, 10f * Time.deltaTime);

            if(Quaternion.Angle(transform.rotation, rotacaoAlvo) < 0.1f) {

                transform.rotation = rotacaoAlvo;
                podeRodar = false;
                print("Acabou rotacao");
                
            }

        }

        if(Input.GetKey(KeyCode.A)) {

            irEmbora = true;
            animator.SetBool("Andar", true);
            animator.SetTrigger("IrEmbora");
            podeMover = true;
            rotacaoAlvo = Quaternion.Euler(0, 270, 0);
            podeRodar = true;

        }

    }

    public void Parar() {
        
        if(!irEmbora) {
            podeRodar = true;
            podeMover = false;
            animator.SetBool("Andar", false);
        }

    }
}
