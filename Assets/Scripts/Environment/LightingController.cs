using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{

    public GameObject lightParent;
    public Light[] storeLights;
    public Animator[] lightAnimators;
    public bool monsterLightingActive;

    // Start is called before the first frame update
    public bool isFlashing;


    public Light playerHighlight;
    public Light sceneLight;


    void Start()
    {
        storeLights = lightParent.GetComponentsInChildren<Light>(true);
        playerHighlight.enabled = false;
        lightAnimators = new Animator[storeLights.Length];
        for(int i = 0; i < storeLights.Length; i++)
        {
            lightAnimators[i] = storeLights[i].gameObject.GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerController.Instance.IsMonster && !monsterLightingActive)
        {
            monsterLightingActive = true;
            EnterMonsterModeLighting();
            Invoke(nameof(ExitMonsterModeLighting), PlayerController.Instance.timeSpentAsMonsterSec);
        }

    }


    void EnterMonsterModeLighting()
    {
        sceneLight.intensity = 0.1f;
        playerHighlight.intensity = 20.0f;
        playerHighlight.enabled = true;

        for(int  i = 0; i < lightAnimators.Length; i++)
        {
            if(lightAnimators[i] != null)
            {
                lightAnimators[i].SetBool("Flashing", true);
            }
        }
    }

    void ExitMonsterModeLighting()
    {
        sceneLight.intensity = 0.75f;
        playerHighlight.enabled = false;

        for (int i = 0; i < lightAnimators.Length; i++)
        {
            if (lightAnimators[i] != null)
            {
                lightAnimators[i].SetBool("Flashing", false);
            }
        }
        monsterLightingActive = false; 
    }
}
