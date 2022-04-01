using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonOnOverPopUp : MonoBehaviour
{
    Transform ownTransform;

    private void Start()
    {
        ownTransform = this.transform;
      
        
    }

   public void OnOverButton(bool isEnter)
    {
        if (isEnter)
        {
            ownTransform.localScale=new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            ownTransform.localScale = Vector3.one;


        }
    }
}
