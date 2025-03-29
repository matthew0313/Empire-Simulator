using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Log : MonoBehaviour
{
    public UnityEvent onLogDelete = new();

    [SerializeField] Text title, content;
    public void Set(string title, string content, Color titleColor)
    {
        this.title.text = title;
        this.content.text = content;
        this.title.color = titleColor;
    }
    public void Delete()
    {
        transform.parent = null;
        onLogDelete.Invoke();
        onLogDelete.RemoveAllListeners();
    }
}
