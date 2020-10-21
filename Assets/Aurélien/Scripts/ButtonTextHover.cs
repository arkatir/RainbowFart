using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
using System.Globalization;

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ // Extends the pointer handlers

    //Button Text update
    public Color buttonHoverColor = Color.white;
    public Color buttonColor = new Color(1.0f, 1.0f, 1.0f, 0.7f);
    public int fontSize = 36;

    // Test for enter and exit
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Text>() != null)
        {
            GetComponent<Text>().color = buttonHoverColor; // Changes the colour of the text
            GetComponent<Text>().fontSize = fontSize + (int)Math.Ceiling((double)(fontSize / 10)); //Changes size of text
            //GetComponent<Text>().fontStyle = FontStyle.Bold; //Changes style of text
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<Text>() != null)
        {
            GetComponent<Text>().color = buttonColor;
            GetComponent<Text>().fontSize = fontSize;
            //GetComponent<Text>().fontStyle = FontStyle.Normal;
        }
    }
}