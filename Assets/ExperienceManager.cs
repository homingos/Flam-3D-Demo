using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{

    public static string activeExpName = "Undfined";
    public GameObject[] experiences;

    public void PlayExperience(int index)
    {
       if(experiences==null || experiences.Length == 0)
        {
                Debug.LogError("experinces are not assigned to array");
            return;
        }

        foreach(GameObject e in experiences)
        e.SetActive(false);

        experiences[index].SetActive(true);

        activeExpName = experiences[index].name;
    }
  
}
