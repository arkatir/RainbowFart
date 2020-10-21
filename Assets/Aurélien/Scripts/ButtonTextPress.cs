using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component


public class ButtonTextPress : MonoBehaviour
{ // Extends the pointer handlers

    //Button Text update
    public int buttonOffsetX = 3, buttonOffsetY = 12;
    Vector3 pos;

    //Test for press and release
    public void OnButtonPressed(Text text)
    {
        pos = text.transform.position;
        text.transform.position = new Vector3(pos.x + (float)buttonOffsetX, pos.y - (float)buttonOffsetY, pos.z);
    }

    public void OnButtonReleased(Text text)
    {
        pos = text.transform.position;
        text.transform.position = new Vector3(pos.x - (float)buttonOffsetX, pos.y + (float)buttonOffsetY, pos.z);
    }
}