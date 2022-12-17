using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Datas : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown difficulity;
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField colums;
    
    
    private void Update()
    {
        Debug.Log(difficulity.options[difficulity.value].text);
    }
}
