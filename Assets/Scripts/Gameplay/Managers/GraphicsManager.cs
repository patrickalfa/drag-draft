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

    private static TargetManager m_target;
    public static TargetManager target
    {
        get
        {
            if (m_target == null)
                m_target = FindObjectOfType<TargetManager>();
            return m_target;
        }
    }

    private static LineManager m_line;
    public static LineManager line
    {
        get
        {
            if (m_line == null)
                m_line = FindObjectOfType<LineManager>();
            return m_line;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    public Sprite[] costSprites;

    ///////////////////////////////////////////////////////////////////////////////
}