using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Add : MonoBehaviour
{
    [HideInInspector] public bool activeAdd;
    void Update()
    {
        if (activeAdd)
        {
            if (Advertisement.IsReady() && !Advertisement.isShowing)
            {
                var options = new ShowOptions();
                options.resultCallback = HandleShowResult;
                Advertisement.Show("video", options);
            }
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            activeAdd = false;
            Debug.Log("continuo jugando");
            ResetParams();
        }
        else if (result == ShowResult.Skipped)
        {
            activeAdd = false;
            Debug.Log("continuo jugando");
            ResetParams();
        }
        else if (result == ShowResult.Failed)
        {
            activeAdd = false;
            Debug.Log("error al cargar el video");
        }
    }

    void StartCreateTiles()
    {
        FindObjectOfType<PlatformManager>().menu = false;
    }

    void ResetParams()
    {
        var menu = FindObjectOfType<Menu>();
        foreach (var item in menu.defeatMenu)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1f;
        StartCreateTiles();
        var model = FindObjectOfType<Model>();
        model.view.meshModel.enabled = true;
        model.view.turbo.SetActive(true);
        foreach (var item in model.view.buttonShootList)
        {
            item.enabled = true;
        }
        foreach (var item in model.view.buttonImagesList)
        {
            item.enabled = true;
        }
        model.stop = false;
        model.view.buttonMenuInGame.SetActive(true);
        model.GetComponent<Rigidbody>().isKinematic = false;
        model.GetComponent<SphereCollider>().enabled = true;
        model.reespawn = true;
        model.view.EffectLostAdditionalLifeCoroutine();
        model.view.offBlackScreen = true;
        //this.gameObject.SetActive(false);
    }
}
