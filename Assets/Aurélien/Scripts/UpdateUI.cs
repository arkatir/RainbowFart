using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers

public class UpdateUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ // Extends the pointer handlers

    //Button Text update
    public int buttonOffsetX = 3, buttonOffsetY = 12;
    Vector3 pos;
    public Color buttonHoverColor = Color.white;
    public Color buttonColor = new Color(1.0f, 1.0f, 1.0f, 0.7f);
    public int fontSize = 36;

    // Test for enter and exit
    public void OnPointerEnter(PointerEventData eventData)
    {
        //GetComponent<Text>().color = buttonHoverColor; // Changes the colour of the text
        //GetComponent<Text>().fontSize = fontSize + (int)Math.Ceiling((double)(fontSize / 10)); //Changes size of text
        //GetComponent<Text>().fontStyle = FontStyle.Bold; //Changes style of text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //GetComponent<Text>().color = buttonColor;
        //GetComponent<Text>().fontSize = fontSize;
        //GetComponent<Text>().fontStyle = FontStyle.Normal;
    }

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