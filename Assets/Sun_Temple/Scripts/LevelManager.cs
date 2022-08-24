using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Management;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public TextMeshProUGUI TxtScore = null;
    private int nbchest = 0;
    private int NbTotalChests=0;
    GameObject[] chestArray;

    /*
       allPositions = new Transform[LevelManager.GetMaxLevel()][];
        for (int i = 0; i < LevelManager.GetMaxLevel(); i++)
        { 
            GameObject TreasurePositions = GameObject.Find("TreasurePositions_L"+(i+1));
            allPositions[i] = TreasurePositions.GetComponentsInChildren<Transform>(true);
        }
        /*
         * Debug.Log("---> TreasurePositions Nb: " + (allPositions.Length - 1));
        foreach (Transform child in allPositions)
        {
            if (child != allPositions[0])
                Debug.Log("---> child: " + child.name);
        }
        */


    void Awake()
    {
        instance = this;
    }

    public void AddOne()
    {
        
        nbchest++;
        TxtScore.text = nbchest+"/" + NbTotalChests;
        print("+++++++ActiveChest="+ nbchest);
        if(nbchest< NbTotalChests)
            setActiveChet(nbchest);
    }
        // Start is called before the first frame update
    void Start()
    {

        GameObject chests = GameObject.Find("Chests");
        NbTotalChests = chests.transform.childCount;
        //Debug.Log("---> chests Total: " + total);

        chestArray = new GameObject[NbTotalChests];
        for (int i = 0; i < NbTotalChests; i++)
        {
            chestArray[i] = GameObject.Find("PiratesChest_" + (i + 1));
            //Debug.Log("---> chests: " + chestArray[i].name);
        }

        setActiveChet(0);

        TxtScore.text = "0/" + NbTotalChests;

        if (XRGeneralSettings.Instance != null)
        {
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                //manage unvisible screen part in VR
                TxtScore.GetComponent<RectTransform>().localPosition += new Vector3(Screen.width / 30, 0, 0);
            }
        }

    }


    void setActiveChet(int iActive)
    {
        for (int i = 0; i < NbTotalChests; i++)
        {
            chest a = chestArray[i].GetComponent<chest>();
            if ((a.isEmpty ()== false)&& (a.isOpen() == false))
                chestArray[i].SetActive(false);
        }
        chestArray[iActive].SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
