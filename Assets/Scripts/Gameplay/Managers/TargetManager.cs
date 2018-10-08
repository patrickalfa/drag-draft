using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGET_SHAPE
{
    CIRCLE,
    TRIANGLE
}

public class TargetManager : MonoBehaviour
{
    [Range(0.01f, 2f)]
    public float size;
    public Color color;
    public int sortingOrder;
    public TARGET_SHAPE shape;

    public Sprite[] sprites;

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

    private List<GameObject> _markers;
    private List<Vector2> _targetPositions;
    private List<TARGET_SHAPE> _shapesIDs;

    ///////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        _markers = new List<GameObject>();
        _targetPositions = new List<Vector2>();
        _shapesIDs = new List<TARGET_SHAPE>();
    }

    private void FixedUpdate()
    {
        DestroyMarkers();
    }

    ///////////////////////////////////////////////////////////////////////////////

    private void DestroyMarkers()
    {
        foreach (GameObject m in _markers)
            Destroy(m);

        _markers.Clear();
        _targetPositions.Clear();
        _shapesIDs.Clear();
    }

    private GameObject GetMarker(TARGET_SHAPE shapeID)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * size;
        gameObject.transform.parent = transform;

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        sr.sprite = sprites[(int)shapeID];
        sr.color = color;

        return gameObject;
    }

    public void DrawMarker(Vector2 position)
    {
        _targetPositions.Add(position);
        _shapesIDs.Add(shape);

        Render();
    }

    private void Render()
    {
        int last = _targetPositions.Count - 1;

        GameObject g = GetMarker(_shapesIDs[last]);
        g.transform.position = _targetPositions[last];
        _markers.Add(g);
    }
}
