using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SplatterAlignment
{
    Floor,
    Wall
}

public class BloodController : MonoBehaviour
{

    public int numTextures;
    public static BloodController Instance;

    //Include all blood splatter textures in resources/blood
    //in format blood_0X
    Texture[] bloodTextures;

    private int useNext;
    Material baseMaterial;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        bloodTextures = new Texture[numTextures];

        for (int i = 0; i < numTextures; i++)
        {
            bloodTextures[i] = Resources.Load<Texture>("Blood/blood_" + i) as Texture;
        }
        baseMaterial = (Material)Resources.Load("Blood/Base");

        useNext = 0;

    }

    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSplatter(Vector3 pos, SplatterAlignment align)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f); //TODO this may change based on our texture sizes
        quad.transform.position = pos;
        switch (align)
        {
            //Note: This is East wall
            case SplatterAlignment.Wall:
                quad.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case SplatterAlignment.Floor:
            default:
                quad.transform.rotation = Quaternion.Euler(90, 0, 0);
                quad.transform.position = new Vector3(pos.x, 0.02f, pos.z);
                break;
        }
        Renderer quadRender = quad.GetComponent<Renderer>();
        quadRender.material = baseMaterial;
        quadRender.material.SetTexture("_MainTex", bloodTextures[useNext]);
        quadRender.material.mainTexture = bloodTextures[useNext];
        useNext++;
    }
}
