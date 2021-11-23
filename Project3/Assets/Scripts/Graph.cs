using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform point_prefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    Transform[] points;

    private void Awake()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;

        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; ++i)
        {
            points[i] = Instantiate(point_prefab);
            points[i].localScale = scale;
            points[i].SetParent(transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;
        for (int i = 0, x = 0, z = 0; i < points.Length; ++i, ++x)
        {
            if (x == resolution)
            {
                x = 0;
                z++;
            }
            float u = (x + 0.5f) * step - 1f;
            float v = (z + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
}
