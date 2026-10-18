
using UnityEngine;

public class ModulationEngine : MonoBehaviour
{
    [Header("Input")]
    public MidiMonitor midiInput;

    [Header("Modulation")]
    public float offset = 0f;
    public float multiplier = 1f;
    public bool invert = false;

    [Header("Output Limits")]
    public float minimum = 0f;
    public float maximum = 1f;

    [Header("Smoothing")]
    [Min(0f)]
    public float smoothing = 2f;

    [Header("Live Output")]
    [Range(0f, 1f)]
    public float rawInput;

    public float outputValue;

    void Update()
    {
        if (midiInput == null)
            return;

        rawInput = Mathf.Clamp01(midiInput.midiValue);

        float input = invert
            ? 1f - rawInput
            : rawInput;

        float target = Mathf.Clamp(
            offset + input * multiplier,
            minimum,
            maximum
        );

        outputValue = Mathf.MoveTowards(
            outputValue,
            target,
            smoothing * Time.deltaTime
        );
    }
}
