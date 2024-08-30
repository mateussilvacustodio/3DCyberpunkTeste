using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] float dinheiroValor;
    [SerializeField] Text dinheiroText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dinheiroText.text = dinheiroValor.ToString("F0");
    }
}
