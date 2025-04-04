using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform cameraTransform;
    [SerializeField] float velocidadeCamera;
    [SerializeField] TerrainLayer geloTerrainLayer;
    [SerializeField] float velocidadeGelo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position += new Vector3(0,0,velocidadeCamera) * Time.deltaTime;

        geloTerrainLayer.tileOffset -= new Vector2(velocidadeGelo,0) * Time.deltaTime;
    }
}
