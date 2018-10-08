using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public Sprite dot;
    [Range(0.01f, 1f)]
    public float size;
    [Range(0.1f, 2f)]
    public float delta;
    public Color color;
    public int sortingOrder;
    public bool outline;
    public float outlineWidth;
    public Color outlineColor;

    private List<Vector2> _positions = new List<Vector2>();
    private List<GameObject> _dots = new List<GameObject>();

    ///////////////////////////////////////////////////////////////////////////////

    private void FixedUpdate()
    {
        if (_positions.Count > 0)
        {
            DestroyAllDots();
            _positions.Clear();
        }

    }

    ///////////////////////////////////////////////////////////////////////////////

    private void DestroyAllDots()
    {
        foreach (GameObject dot in _dots)
        {
            Destroy(dot);
        }
        _dots.Clear();
    }

    private GameObject GetOneDot()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * size;
        gameObject.transform.parent = transform;

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        sr.sprite = dot;
        sr.color = color;

        if (outline)
            CreateDotOutline(gameObject);

        return gameObject;
    }

    private void CreateDotOutline(GameObject dot)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * (size + outlineWidth);
        gameObject.transform.parent = dot.transform;

        SpriteRenderer ol = gameObject.AddComponent<SpriteRenderer>();
        ol.sortingOrder = sortingOrder - 1;
        ol.sprite = this.dot;
        ol.color = outlineColor;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            _positions.Add(point);
            point += (direction * delta);
        }

        Render();
    }

    private void Render()
    {
        foreach (Vector2 position in _positions)
        {
            GameObject g = GetOneDot();
            g.transform.position = position;
            _dots.Add(g);
        }
    }
}