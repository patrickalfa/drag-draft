using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Transform _transform;

    private static UIManager m_instance;
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UIManager>();
            return m_instance;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        _transform = transform;
    }

    public void ChangeText(string name, string text)
    {
        _transform.Find(name).GetComponent<Text>().text = text;
    }
}