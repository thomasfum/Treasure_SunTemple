using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public TextMeshProUGUI TxtScore = null;
    private int nbchest = 0;
    private int total=2;
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
        TxtScore.text = nbchest+"/" + total;
        print("+++++++ActiveChest="+ nbchest);
        if(nbchest< total)
            setActiveChet(nbchest);
    }
        // Start is called before the first frame update
        void Start()
    {
        chestArray = new GameObject[total];
        for (int i = 0; i < total; i++)
        {
            chestArray[i] = GameObject.Find("PiratesChest_" + (i + 1));
            Debug.Log("---> TreasurePositions: " + chestArray[i].name);
        }

        setActiveChet(0);

        TxtScore.text = "0/" + total;
    }


    void setActiveChet(int iActive)
    {
        for (int i = 0; i < total; i++)
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
