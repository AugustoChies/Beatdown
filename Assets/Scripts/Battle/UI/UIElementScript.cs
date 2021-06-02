using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementScript : MonoBehaviour
{
    protected Canvas _mycanvas;

    protected void Awake()
    {
        _mycanvas = GetComponent<Canvas>();
    }

    public virtual void Show()
    {
        _mycanvas.enabled = true;
    }

    public virtual void Hide()
    {
        _mycanvas.enabled = false;
    }
}
