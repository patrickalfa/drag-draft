using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Sprite highlight;
    [Range(0.01f, 2f)]
    public float size;
    public Color color;
    public int sortingOrder;

    private static TargetManager m_instance;
    public static TargetManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<TargetManager>();
            return m_instance;
        }
    }

    private Transform _transform;
    private GameObject _marker;
    private Vector2 _targetPosition;

    ///////////////////////////////////////////////////////////////////////////////

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        DestroyMarker();
    }

    ///////////////////////////////////////////////////////////////////////////////

    private void DestroyMarker()
    {
        if (_marker)
            Destroy(_marker);

        _marker = null;
    }

    private GameObject GetMarker()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * size;
        gameObject.transform.parent = transform;

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        sr.sprite = highlight;
        sr.color = color;

        return gameObject;
    }

    public void DrawMarker(Vector2 position)
    {
        DestroyMarker();

        _targetPosition = position;

        Render();
    }

    private void Render()
    {
        _marker = GetMarker();
        _marker.transform.position = _targetPosition;
    }
}
