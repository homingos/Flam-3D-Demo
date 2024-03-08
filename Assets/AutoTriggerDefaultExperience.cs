using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTriggerDefaultExperience : MonoBehaviour
{
    // Start is called before the first frame update

    int defaultIndex = 0;
    void Start()
    {
        Invoke("TriggerDefaultExeprience", 0.5f);
    }

    void TriggerDefaultExeprience()
    {
        ExperienceManager e = FindObjectOfType<ExperienceManager>();
        e.PlayExperience(defaultIndex);
    }

}
