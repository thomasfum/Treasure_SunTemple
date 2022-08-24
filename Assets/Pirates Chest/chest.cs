using SunTemple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{

    public GameObject cap;
    public GameObject lockA1;
    public GameObject lockA2;
    public GameObject customPivot;
    public GameObject treasure;
    private Collider ChestCollider;
    public string playerTag = "Player";
    public bool debug = false;
    private bool empty = false;

    private LevelManager level;


    public float fadeSpeed = 0.1f;

    private float t = 0;
    private bool open = false;
    private IEnumerator coroutine;

    private Material[] MaterialsTreasure;
    //private Material[] MaterialsLockA1;
    //private Material[] MaterialsLockA2;
    private float spawnTime;

    private float timeLock = 0;
    private float timeOpen = 2;
    private float timeTreasure = 3;

    private float fire_start_time = 0;

    private CursorManager cursor;
    private Radar radar;
    private GameObject Player;
    private Camera Cam;
    private bool scriptIsEnabled = true;

    public float MaxDistance = 3.0f;
    float MaxDistance_update = 5.0f;
    int radarUpdate = 0;

    public bool isOpen()
    {
        return open;
    }
    public bool isEmpty()
    {
        return empty;
    }

    // Start is called before the first frame update
    void Start()
    {

        MaterialsTreasure = treasure.GetComponent<MeshRenderer>().materials;
       // MaterialsLockA1 = lockA1.GetComponent<MeshRenderer>().materials;
       // MaterialsLockA2 = lockA2.GetComponent<MeshRenderer>().materials;


        ChestCollider = GetComponent<BoxCollider>();

        if (!ChestCollider)
        {
            Debug.LogWarning(this.GetType().Name + ".cs on " + gameObject.name + "door has no collider", gameObject);
            scriptIsEnabled = false;
            return;
        }

        Player = GameObject.FindGameObjectWithTag(playerTag);

        if (!Player)
        {
            Debug.LogWarning(this.GetType().Name + ".cs on " + this.name + ", No object tagged with " + playerTag + " found in Scene", gameObject);
            scriptIsEnabled = false;
            return;
        }
        Cam = Camera.main;
        if (!Cam)
        {
            Debug.LogWarning(this.GetType().Name + ", No objects tagged with MainCamera in Scene", gameObject);
            scriptIsEnabled = false;
        }
        cursor = CursorManager.instance;
        if (cursor != null)
        {
            cursor.SetCursorToDefault();
        }


        level = LevelManager.instance;
        radar = Radar.instance;


        MaxDistance_update = MaxDistance * 1.2f;

    }


    // Update is called once per frame
    void Update()
    {
        if (scriptIsEnabled)
        {

            if (open == false)
            {
                radarUpdate++;
                if (radarUpdate > 10)
                {
                    radarUpdate = 0;
                    Vector2 Vcam = new Vector2(Cam.transform.forward.x, Cam.transform.forward.z);
                    Vector2 Vtreasure = new Vector2(transform.localPosition.x - Cam.transform.position.x, transform.localPosition.z - Cam.transform.position.z);
                    float Angle = Vector2.SignedAngle(Vcam, Vtreasure);
                    float Dist = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.z), new Vector2(Cam.transform.position.x, Cam.transform.position.z));

                    radar.SetValues(Angle, Dist, this.name);
                }
            }

            /*
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                open = true;
                spawnTime = Time.time;
            }
            */

            if (cursor != null)
            {
                if (open != true)
                    CursorHint();
            }

            if (open == true)
            {
                lockA1.SetActive( false );
                lockA2.SetActive( false );
                /*
                if (t < timeLock * 2)
                {
                    SetAlphaA1((Time.time - spawnTime) * fadeSpeed * 3);
                    SetAlphaA2((Time.time - spawnTime) * fadeSpeed * 3);
                }
                */
                // print("WaitAndPrint " + Time.time + " " + t);

                if ((t > timeLock) && (t < timeOpen))
                    cap.transform.RotateAround(customPivot.transform.position, customPivot.transform.right * -1, 20 * Time.deltaTime * 2);


                if ((t > timeOpen) && (t < timeTreasure))
                {
                   
                    SetAlpha1((Time.time - spawnTime - timeOpen) * fadeSpeed * 10);
                }


                if ((t > timeTreasure)&&(empty==false))
                {
                    Player.GetComponent<CharController_Motor>().AllowMove(true);
                    LevelManager.instance.AddOne();
                    empty = true;
                }
                if (open == true)
                    t += Time.deltaTime;
            }
        }
    }

    void SetAlpha1(float alpha)
    {

        //print("Treasure " + alpha);

        // Change the alpha value of each materials' color
        for (int i = 0; i < MaterialsTreasure.Length; ++i)
        {
            Color color = MaterialsTreasure[i].color;
            color.a = Mathf.Clamp(1 - alpha, 0, 1);
            MaterialsTreasure[i].color = color;
        }
    }
    /*
    void SetAlphaA1(float alpha)
    {
        // Change the alpha value of each materials' color
        for (int i = 0; i < MaterialsLockA1.Length; ++i)
        {
            Color color = MaterialsLockA1[i].color;
            color.a = Mathf.Clamp(1 - alpha, 0, 1);
            MaterialsLockA1[i].color = color;
        }
    }
    void SetAlphaA2(float alpha)
    {
        // Change the alpha value of each materials' color
        for (int i = 0; i < MaterialsLockA2.Length; ++i)
        {
            Color color = MaterialsLockA2[i].color;
            color.a = Mathf.Clamp(1 - alpha, 0, 1);
            MaterialsLockA2[i].color = color;
        }
    }
    */
    void CursorHint()
    {
        float d = Mathf.Abs(Vector3.Distance(transform.position, Player.transform.position));
        if (debug == true)
            Debug.Log("Chest D=" + d);
        if (d <= MaxDistance_update)
        {

            //Ray ray = Cam.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
            Ray ray = new Ray(Cam.transform.position, Cam.transform.forward);

            RaycastHit hit;

            //if (DoorCollider.Raycast (ray, out hit, MaxDistance_update)) 
            //if (Physics.Raycast(Player.transform.position, Cam.transform.forward, out hit, MaxDistance_update*2))
            if (ChestCollider.Raycast(ray, out hit, MaxDistance_update))
            {
                if (debug == true)
                {
                    Debug.Log(hit.transform.gameObject.name + "-" + this.gameObject.name);
                    Debug.Log("------------>Open");
                }
                cursor.SetCursorToChest();
                Player.GetComponent<CharController_Motor>().AllowMove(false);
                if (fire_start_time == 0)
                    fire_start_time = Time.time;


                /*
            }
            else
            {
                Debug.Log("------------>Default"+ hit.transform.gameObject.name);
                cursor.SetCursorToDefault();
                Player.GetComponent<CharController_Motor>().AllowMove(true);
                fire_start_time = 0;
            }
                */
            }
            else
            {
                cursor.SetCursorToDefault();
                Player.GetComponent<CharController_Motor>().AllowMove(true);
                fire_start_time = 0;
            }
            if (fire_start_time != 0)
            {

                cursor.SetProress((Time.time - fire_start_time) * 100);
                if (Time.time - fire_start_time > 1f)
                {
                    open = true;
                    spawnTime = Time.time;
                    cursor.SetCursorToDefault();
                    fire_start_time = 0;
                }
            }
        }

    }
}
