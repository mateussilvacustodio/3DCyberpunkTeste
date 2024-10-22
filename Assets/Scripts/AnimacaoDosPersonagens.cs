using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoDosPersonagens : MonoBehaviour
{
    [SerializeField] Animator bonecoAnimator;
    [SerializeField] bool podeMoverTeste;
    [SerializeField] float velocidadeMoverTeste;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(podeMoverTeste) {

            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * velocidadeMoverTeste;

        }
    }

    public void parar() {

        podeMoverTeste = false;

    }

    public void andar() {

        podeMoverTeste = true;

    }
}
