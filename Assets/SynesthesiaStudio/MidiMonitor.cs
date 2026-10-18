
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Minis;

public class MidiMonitor : MonoBehaviour
{
    public int controllerNumber = 20;

    [Range(0f, 1f)]
    public float midiValue;

    private readonly List<MidiDevice> devices = new List<MidiDevice>();

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        ConnectDevices();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        DisconnectDevices();
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (!(device is MidiDevice)) return;

        DisconnectDevices();
        ConnectDevices();
    }

    void ConnectDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (!(device is MidiDevice midi)) continue;

            midi.onWillControlChange += OnControlChange;
            devices.Add(midi);

            Debug.Log("MIDI detected: " + midi.description.product);
        }
    }

    void DisconnectDevices()
    {
        foreach (var device in devices)
            device.onWillControlChange -= OnControlChange;

        devices.Clear();
    }

    void OnControlChange(MidiValueControl cc, float value)
    {
        if (cc.controlNumber != controllerNumber) return;

        midiValue = value;
    }

    void OnGUI()
    {
        GUI.Label(
            new Rect(20, 20, 400, 40),
            $"MIDI CC {controllerNumber}: {midiValue:F3}"
        );
    }
}
