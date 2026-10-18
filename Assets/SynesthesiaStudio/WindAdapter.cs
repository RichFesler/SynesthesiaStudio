
using UnityEngine;

public class WindAdapter : MonoBehaviour
{
    [Header("Input")]
    public ModulationEngine modulation;

    [Header("Output")]
    public WindZone windZone;

    void Update()
    {
        if (modulation == null || windZone == null)
            return;

        windZone.windMain = modulation.outputValue;
    }
}
