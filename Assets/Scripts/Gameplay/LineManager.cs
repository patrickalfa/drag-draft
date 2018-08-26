using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    // Inspector fields
    public Sprite Dot;
    [Range(0.01f, 1f)]
    public float Size;
    [Range(0.1f, 2f)]
    public float Delta;

    public Color color;
    public int sortingOrder;
    public bool outline;
    public float outlineWidth;
    public Color outlineColor;

    //Static Property with backing field
    private static LineManager instance;
    public static LineManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LineManager>();
            return instance;
        }
    }

    //Utility fields
    List<Vector2> positions = new List<Vector2>();
    List<GameObject> dots = new List<GameObject>();

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (positions.Count > 0)
        {
            DestroyAllDots();
            positions.Clear();
        }

    }

    private void DestroyAllDots()
    {
        foreach (GameObject dot in dots)
        {
            Destroy(dot);
        }
        dots.Clear();
    }

    private GameObject GetOneDot()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * Size;
        gameObject.transform.parent = transform;

        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        sr.sprite = Dot;
        sr.color = color;

        if (outline)
            CreateDotOutline(gameObject);

        return gameObject;
    }

    private void CreateDotOutline(GameObject dot)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * (Size + outlineWidth);
        gameObject.transform.parent = dot.transform;

        SpriteRenderer ol = gameObject.AddComponent<SpriteRenderer>();
        ol.sortingOrder = sortingOrder - 1;
        ol.sprite = Dot;
        ol.color = outlineColor;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            positions.Add(point);
            point += (direction * Delta);
        }

        Render();
    }

    private void Render()
    {
        foreach (Vector2 position in positions)
        {
            GameObject g = GetOneDot();
            g.transform.position = position;
            dots.Add(g);
        }
    }
}