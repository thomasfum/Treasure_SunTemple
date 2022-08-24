using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class Radar : MonoBehaviour
{
    public static Radar instance;

    public GameObject center=null;
    public GameObject circle;
    public GameObject radar;

    private SpriteRenderer SpriteRendererCenter;

    private SpriteRenderer SpriteRendererRadar;

    void Awake()
    {
        instance = this;
        SpriteRendererRadar = radar.GetComponent<SpriteRenderer>();
        SpriteRendererCenter= center.GetComponent<SpriteRenderer>();
    }

    public void SetValues(float Angle, float Dist, string name)
    {
        

        float d= Mathf.Min(Dist / 10f,1f);


        //Debug.Log("===>" + name + ":" + Angle + "; " + Dist + ", "+d);

        Vector3 movement = Vector3.zero;
        movement.y = 1 * Mathf.Cos(Angle *  Mathf.PI / 180) * d;
        movement.x = - 1 * Mathf.Sin(Angle *  Mathf.PI / 180) * d;

        //SpriteRendererRadar.transform.localPosition = new Vector3(center.transform.localPosition.x+Dist / 10f, center.transform.localPosition.y, center.transform.localPosition.z);
        SpriteRendererRadar.transform.localPosition = movement;

    }

    // Start is called before the first frame update
    void Start()
    {
        //manage unvisible screen part in VR
        if (XRGeneralSettings.Instance != null)
        {
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                circle.GetComponent<RectTransform>().localPosition += new Vector3(Screen.width / 30, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
