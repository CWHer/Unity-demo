using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField, Range(0.1f, 2f)]
    float sample_duration = 1f;

    public enum DisplayMode { FPS, MS }

    [SerializeField]
    DisplayMode display_mode = DisplayMode.FPS;

    int frames;
    float duration, best_duration = float.MaxValue, worst_duration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float frame_duration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frame_duration;

        best_duration = Mathf.Min(best_duration, frame_duration);
        worst_duration = Mathf.Max(worst_duration, frame_duration);

        if (duration >= sample_duration)
        {
            if (display_mode == DisplayMode.FPS)
            {
                display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}",
                    1f / best_duration,
                    frames / duration,
                    1f / worst_duration);
            }
            else
            {
                display.SetText("MS\n{0:1}\n{1:1}\n{2:1}",
                    1000f * best_duration,
                    1000f * duration / frames,
                    1000f * worst_duration);
            }
            frames = 0;
            duration = 0;
            best_duration = float.MaxValue;
            worst_duration = 0;
        }
    }
}
