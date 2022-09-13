using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class car_light : MonoBehaviour
{
public enum Side
    {
        Front,
        Back
    }

    [System.Serializable]
    public struct Light
    {
        public GameObject lightObj;
        public Material lightMat;
        public Side side;
    }

   // public Toggle lightToggle;

    public bool isFrontLightOn;
    public bool isBackLightOn;

    public Color frontLightOnColor;
    public Color frontLightOffColor;
    public Color backLightOnColor;
    public Color backLightOffColor;


[ColorUsage(true, true)]
    public GameObject [] bakcligh;
    //public Material   backlightMat;
    public  Color testCol;

    
   // public bool isback=false;
    public List<Light> lights;
    private Material material;
    //public GameObject backObj;
    //[SerializeField]private Material breaklight;
    //public Material test;
    

    [Header("backlightOBJ")]
    [SerializeField]private GameObject backlightOBJ;
    [SerializeField]public Material   backlightMatidle;
    [SerializeField]public Material   backlightLIGHT;


    [Header("breakeOBJ")]
    [SerializeField]private GameObject breakOBJ;
    [SerializeField]private Material breakMATidle;
    [SerializeField]private Material breakMATbright;


    [Header("blanck")]
    [SerializeField]private GameObject blanckobj_R;
    [SerializeField]private Material blanck_idle_R;
    [SerializeField]private Material blanck_light_R;
    [SerializeField]private GameObject blanckobj_L;
    [SerializeField]private Material blank_idle_L;
    [SerializeField]private Material blanck_light_L;
    private bool blankR=false;
    private bool blankL=false;
    private bool waringblank=false;

    void Awake()
    {
        
        //breaklight.DisableKeyword("_EMISSION");
        //backlightMat.DisableKeyword("_EMISSION");
        //backlightMat.SetColor("_EmissionColor",new Vector4(191,191,191,0));
        //breaklight.SetColor("_EmissionColor",new Vector4(191,191,191,0));
        
        //backlightMat.SetColor("_EmissionColor",new Vector4(191,191,191,0));
    }
    void Start()
    {
        
        //testCol = (Color)backlightMat.GetVector("activeColor");
        material = GetComponent<Material>();
        //isFrontLightOn = lightToggle.isOn;
        isBackLightOn = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            OperateFrontLights();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!blankR)
            {
                blankR=true;
            }
            else
            {
                blankR=false;
            }

            blankL=false;
            waringblank=false;
            StartCoroutine(blank_R());
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!blankL)
            {
                blankL=true;
            }
            else
            {
                blankL=false;
            }

            blankR=false;
            waringblank=false;
            StartCoroutine(blank_L());
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            blankL=false;
            blankR=false;
            if(!waringblank)
            {
                waringblank=true;
            }
            else
            {
                waringblank=false;
            }
            StartCoroutine(waringblankCO());
        }
        // if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
        // {
        //     foreach(var bakclighs in bakcligh)
        //     {
        //         //bakclighs.gameObject.SetActive(true);
        //     }
        // }
        // else if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.S))
        // {
        //     foreach(var bakclighs in bakcligh)
        //     {
        //         //bakclighs.gameObject.SetActive(false);
        //     }
        // }

        if(isBackLightOn)
        {
            //breaklight.SetVector("_EmissionColor",new Vector4(191,191,191,4));
            breakOBJ.GetComponent<Renderer>().material =breakMATbright;
            //breaklight.EnableKeyword("_EMISSION");
        }
        else
        {
            breakOBJ.GetComponent<Renderer>().material =breakMATidle;
            //breaklight.DisableKeyword("_EMISSION");
            //breaklight.SetColor("_EmissionColor",new Vector4(191,191,191,0)* 0);
        }

        // if(Input.GetKeyDown(KeyCode.S) || isback)
        // {   
        //     isback=true;
        //     breaklight.SetVector("_EmissionColor",new Vector4(191,191,191,4));
        // }
        // else 
        // {
        //     isback=false;
        //     breaklight.SetColor("_EmissionColor",new Vector4(191,191,191,0)* 0);
        // }

        // if(Input.GetKeyDown(KeyCode.Space) || isback)
        // {
        //     isback=true;
        //     breaklight.SetVector("_EmissionColor",new Vector4(191,191,191,4));
        // }
        // else
        // {
        //     isback=false;
        //     breaklight.SetColor("_EmissionColor",new Vector4(191,191,191,0)* 0);
        // }
        
        // if(Input.GetKeyDown(KeyCode.DownArrow) || isback)
        // {
        //     isback=true;
        //     breaklight.SetVector("_EmissionColor",new Vector4(191,191,191,4));
        // }
        // else
        // {
        //     isback=false;
        //     breaklight.SetColor("_EmissionColor",new Vector4(191,191,191,0)* 0);
        // }
    }

    public void OperateFrontLights()
    {
        isFrontLightOn = !isFrontLightOn;

        if (isFrontLightOn)
        {
            //Turn On Lights
            foreach(var light in lights)
            {
                if(light.side == Side.Front && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                    light.lightMat.color = frontLightOnColor;
                }
            }
            //backlightMat.SetVector("_EmissionColor",new Vector4(191,191,191,3));
            Color a = new Color(191,191,191);
            //backlightOBJ.GetComponent<MeshRenderer> ().materials [1].color = new Color(191,191,191);
            backlightOBJ.GetComponent<Renderer>().material = backlightLIGHT;
            //backlightOBJ.GetComponent<Renderer>().material =backlightLIGHT;
            //backlightMat.EnableKeyword("_EMISSION");
            //backlightMat.SetColor("_EmissionColor",a,3);
            //lightToggle.gameObject.GetComponent<Image>().color = Color.yellow;
        }

        else
        {
            //Turn Off Lights
            foreach (var light in lights)
            {
                if (light.side == Side.Front && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                    light.lightMat.color = frontLightOffColor;
                }
            }
            //backlightMat.DisableKeyword("_EMISSION");
            backlightOBJ.GetComponent<Renderer>().material = backlightMatidle;
            //backlightOBJ.GetComponent<MeshRenderer> ().materials [0].color = new Color(191,191,191);
            //backlightMat.SetColor("_EmissionColor",new Vector4(191,191,191,0)* 0);
            
            //lightToggle.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    IEnumerator blank_R()
    {
        while(blankR)
        {
            blanckobj_R.GetComponent<Renderer>().material = blanck_light_R;
            yield return new WaitForSeconds(0.5f);
            blanckobj_R.GetComponent<Renderer>().material = blanck_idle_R;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    IEnumerator blank_L()
    {
        while(blankL)
        {
            blanckobj_L.GetComponent<Renderer>().material = blanck_light_L;
            yield return new WaitForSeconds(0.5f);
            blanckobj_L.GetComponent<Renderer>().material = blank_idle_L;
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    IEnumerator waringblankCO()
    {
        while(waringblank)
        {
            blanckobj_R.GetComponent<Renderer>().material = blanck_light_R;
            blanckobj_L.GetComponent<Renderer>().material = blanck_light_L;
            yield return new WaitForSeconds(0.5f);
            blanckobj_R.GetComponent<Renderer>().material = blanck_idle_R;
            blanckobj_L.GetComponent<Renderer>().material = blank_idle_L;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // public void OperateBackLights()
    // {
    //     if (isBackLightOn)
    //     {
    //         //Turn On Lights
    //         // foreach (var light in lights)
    //         // {
    //         //     if (light.side == Side.Back && light.lightObj.activeInHierarchy == false)
    //         //     {
    //         //         light.lightObj.SetActive(true);
    //         //         light.lightMat.color = backLightOnColor;
    //         //     }
    //         // }
    //         backlightMat.SetVector("_EmissionColor",new Vector4(191,191,191,4));
    //     }
    //     else
    //     {
    //         //Turn Off Lights
    //         foreach (var light in lights)
    //         {
    //             if (light.side == Side.Back && light.lightObj.activeInHierarchy == true)
    //             {
    //                 light.lightObj.SetActive(false);
    //                 light.lightMat.color = backLightOffColor;
    //             }
    //         }
    //     }
    // }
}
