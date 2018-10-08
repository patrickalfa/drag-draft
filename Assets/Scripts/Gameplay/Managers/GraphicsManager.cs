using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    private static GraphicsManager m_instance;
    public static GraphicsManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<GraphicsManager>();
            return m_instance;
        }
    }

    public Sprite[] costSprites;

    ///////////////////////////////////////////////////////////////////////////////
}