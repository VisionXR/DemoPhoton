using System;
using UnityEngine;

public class Inputmanager : MonoBehaviour
{
    public static Inputmanager instance;
    public event Action<int> ButtonClicked;

    private void Awake()
    {
        instance = this;
    }
    public void OnUpButtonClicked()
    {
        ButtonClicked(1);
    }

    public void OnDownButtonClicked()
    {
        ButtonClicked(2);
    }

    public void OnLeftButtonClicked()
    {
        ButtonClicked(3);
    }
    public void OnRightButtonClicked()
    {
        ButtonClicked(4);
    }
    public void OnFireButtonClicked()
    {
        ButtonClicked(5);
    }
}
