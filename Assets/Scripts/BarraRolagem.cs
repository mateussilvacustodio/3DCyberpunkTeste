using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraRolagem : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] float valor;
    // Start is called before the first frame update
    void Start()
    {
        //scrollbar.value = Mathf.Clamp(valor, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mudarValorDoScroll() {

        scrollbar.value = Mathf.Clamp(valor, 0, 1);

    } 
}
