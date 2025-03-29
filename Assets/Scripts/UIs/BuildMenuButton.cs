using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class BuildMenuButton : MonoBehaviour
{
    [SerializeField] TMP_Text structureName;
    Button button;
    public Structure structure { get; private set; }

    public delegate void BuildMenuButtonClickCallback(Structure structure);
    public event BuildMenuButtonClickCallback onBuildMenuButtonClick;
    public void Set(Structure structure)
    {
        this.structure = structure;
        structureName.text = structure.Name;
        if(button==null) button = GetComponent<Button>();
        button.onClick.AddListener(() => { onBuildMenuButtonClick.Invoke(structure); });
    }
}
