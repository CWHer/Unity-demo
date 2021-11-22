using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform point_prefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    Transform[] points;

    private void Awake()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;

        points = new Transform[resolution];
        for (int i = 0; i < points.Length; ++i)
        {
            points[i] = Instantiate(point_prefab);
            position.x = (i + 0.5f) * step - 1f;
            //position.y = position.x * position.x * position.x;
            points[i].localPosition = position;
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
        float time = Time.time;
        for (int i = 0; i < points.Length; ++i)
        {
            var point = points[i];
            Vector3 position = point.position;
            //position.y = position.x * position.x * position.x;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;
        }
    }
}
