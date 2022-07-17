using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonalizedButton : MonoBehaviour
{
    public bool Animated;
    public UnityEvent OnClick;
    
    void OnMouseEnter()
    {
        if(Animated)
            transform.localScale += new Vector3(0.3f, 0.3f, 0);
    }

    void OnMouseExit()
    {
        if(Animated)
            transform.localScale -= new Vector3(0.3f, 0.3f, 0);   
    }

    void OnMouseDown()
    {
        OnClick.Invoke();
    }


}
