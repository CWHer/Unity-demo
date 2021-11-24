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

    public enum TransitionMode { Cycle, Random };

    [SerializeField]
    TransitionMode transition_mode = TransitionMode.Cycle;

    [SerializeField, Min(0f)]
    float function_duration = 1f, transition_duration = 1f;

    float duration;
    bool transitioning;
    FunctionLibrary.FunctionName transition_function;

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

    void pickNextFunction()
    {
        function = transition_mode == TransitionMode.Cycle ?
            FunctionLibrary.getNextFunction(function) :
            FunctionLibrary.getRandomFunction(function);
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        if (transitioning)
        {
            if (duration >= transition_duration)
            {
                duration -= transition_duration;
                transitioning = false;
            }
        }
        else if (duration >= function_duration)
        {
            duration -= function_duration;
            transitioning = true;
            transition_function = function;
            pickNextFunction();
        }

        if (transitioning)
            updateFunctionTransition();
        else
            updateFunction();
    }

    void updateFunctionTransition()
    {
        FunctionLibrary.Function
            from = FunctionLibrary.GetFunction(transition_function),
            to = FunctionLibrary.GetFunction(function);
        float progress = duration / transition_duration;
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
            points[i].localPosition = FunctionLibrary.Morph(
                u, v, time, from, to, progress);
        }

    }

    void updateFunction()
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
