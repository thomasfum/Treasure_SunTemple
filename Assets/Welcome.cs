using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using Google.XR.Cardboard;
using TMPro;

public class Welcome : MonoBehaviour
{
    


    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void StartNoXR(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }


    public IEnumerator StartXR(string scene)
    {
        if (XRGeneralSettings.Instance == null)//probably in unity
        {
            Debug.Log("XRGeneralSettings.Instance=null");
            StartNoXR("DemoScene");
        }
        else
        {
            /*
            Debug.Log("Initializing XR...");
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

            if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            {
                Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
            }
            else
            */
            {
                Debug.Log("Starting XR...");
                XRGeneralSettings.Instance.Manager.StartSubsystems();
                //SceneManager.LoadScene(scene, LoadSceneMode.Single);
                float progress = 0f;

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

                asyncLoad.allowSceneActivation = false; //scene will not load while this value is false

                while (!asyncLoad.isDone)
                {
                    if (asyncLoad.progress >= 0.9f)
                    {
                        asyncLoad.allowSceneActivation = true;
                        // yield return null;
                        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

                        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
                        {
                            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
                            yield return null;
                        }
                        else
                            XRGeneralSettings.Instance.Manager.StartSubsystems();
                    }
                    yield return null;
                }

                 
                    
                
            }
        }
    }
    public void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
   
    public void ButtonVR()
    {
        Debug.Log("Button VR");
        StartCoroutine(StartXR("DemoScene"));
    }
    public void ButtonNoVR()
    {
        Debug.Log("Button No VR");
        StartNoXR("DemoScene");
    }

    public void ButtonQuit()
    {
       Debug.Log("Button Quit");
       Application.Quit();
    }
}