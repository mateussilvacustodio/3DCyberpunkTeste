using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorUIController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Sprite cursorSeta;
    [SerializeField] Sprite cursorMao;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPointerOverButton())
        {
            gameController.cursorImage.sprite = cursorMao;
        }
        else
        {
            gameController.cursorImage.sprite = cursorSeta;
        }
    }
    
    bool IsPointerOverButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool achouTexto = false;
        int index = 0;

        do
        {
            if (results[index].gameObject.GetComponent<Text>() != null || results[index].gameObject.GetComponent<TMP_Text>() != null)
            {
                achouTexto = true;
            }
            else if (results[index].gameObject.GetComponent<Button>() != null && results[index].gameObject.GetComponent<Button>().interactable)
            {
                achouTexto = false;
                return true;
            }
            else
            {
                achouTexto = false;
            }
            index++;
        } while (achouTexto);

        return false;
    }
    

}
