using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class View : MonoBehaviour
{
    public BlackScreen postPro;
    public float intensity;
    
    public List<ParticleSystem> electric;
    public ParticleSystem dieExplosion;
    public GameObject turbo;

    public MeshRenderer meshModel;
    public List<Material> materiales;

    public List<GameObject> listAdditionalLifes;

    public GameObject offNearObsacleCannon;

    public List<ParticleSystem> feedBuckJumpPartycles;
    public ParticleSystem feedbuckBullet;

    public GameObject sphere;

    public Image disarm;
    public GameObject disarmParticles;

    public Slider sliderStar;
    public GameObject fillAreaStar;

    public GameObject starShield;

    public GameObject slowReactionParticles;

    public List<Image> buttonImagesList;

    public List<Image> buttonShootList;

    public void JumpPartycles()
    {
        foreach (var item in feedBuckJumpPartycles)
        {
            item.Play();
        }
    }

    public void FeedBuckShoot()
    {
        feedbuckBullet.Play();
    }

    public void ShieldUp()
    {
        sphere.gameObject.SetActive(true);
    }

    public void ShieldDown()
    {
        sphere.gameObject.SetActive(false);
    }

    public void StarUp()
    {
        fillAreaStar.SetActive(true);
        starShield.SetActive(true);
    }

    public void StarDown()
    {
        fillAreaStar.SetActive(false);
        starShield.SetActive(false);
    }

    public void DisarmOn()
    {
        disarm.enabled = true;
        disarmParticles.SetActive(true);
        foreach (var item in buttonShootList)
        {
            item.color = Color.red;
        }
    }

    public void DisarmOff()
    {
        disarmParticles.SetActive(false);
        disarm.enabled = false;
        foreach (var item in buttonShootList)
        {
            item.color = Color.white;
        }
    }

    public void SlowReactionOn()
    {
        StartCoroutine(ChangeColorButton(buttonImagesList));
        slowReactionParticles.SetActive(true);
    }

    public void SlowReactionOff()
    {
        slowReactionParticles.SetActive(false);
        StopAllCoroutines();
        ResetColourButton(buttonImagesList);
    }

    public IEnumerator ChangeColorButton(List<Image> typeList)
    {
        for (int i = 0; i < 10; i++)
        {
            foreach (var item in typeList)
            {
                item.color = Color.red;
            }
            yield return new WaitForSeconds(0.3f);
            foreach (var item in typeList)
            {
                item.color = Color.white;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ResetColourButton(List<Image> typeList)
    {
        foreach (var item in typeList)
        {
            item.color = Color.white;
        }
    }

    public GameObject buttonMenuInGame;


    bool _blackScreen;
    [HideInInspector] public bool offBlackScreen;

    public void Die()
    {
        meshModel.enabled = false;
        turbo.SetActive(false);
        dieExplosion.Play();
        _blackScreen = true;
        foreach (var item in buttonShootList)
        {
            item.enabled = false;
        }
        foreach (var item in buttonImagesList)
        {
            item.enabled = false;
        }
        buttonMenuInGame.SetActive(false);
    }

    public void Start()
    {
        postPro.blackScreenIntensity = 0;
        offBlackScreen = true;
    }

    public void Update()
    {
        if (_blackScreen)
        {
            if (postPro.blackScreenIntensity >= 0)
            {
                postPro.blackScreenIntensity -= 0.02f;
            }
            else _blackScreen = false;
        }
        if (offBlackScreen)
        {
            if (postPro.blackScreenIntensity <= 1)
            {
                postPro.blackScreenIntensity += 0.01f;
            }
            else offBlackScreen = false;
        }
    }

    #region CHEAT

    //Invulnerability
    public void Invulnerability(bool Invulnerability)
    {
        if (Invulnerability) meshModel.material = materiales[1];

        if (!Invulnerability) meshModel.material = materiales[0];
    }

    //AddLife
    public void EffectLostAdditionalLifeCoroutine()
    {
        StartCoroutine(AddLifeLostEffect());
    }

    public IEnumerator AddLifeLostEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            turbo.SetActive(false);
            meshModel.enabled = false;
            yield return new WaitForSeconds(0.2f);
            turbo.SetActive(true);
            meshModel.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void FeedBuckAdditionalLifes(int additionalLife)
    {
        for (int i = 0; i < additionalLife ; i++)
        {
            listAdditionalLifes[i].SetActive(true);
        }
    }


    int actualLife;

    public void EffectLostAdditionalLifeCanvas()
    {
        actualLife = 0;
        foreach (var item in listAdditionalLifes)
        {
            if (item.activeSelf == true) actualLife++;
        }
        listAdditionalLifes[actualLife - 1].SetActive(false);
    }

    //offNearObstacle

    public IEnumerator PartyclesCannon()
    {
        for (int i = 0; i < electric.Count; i++)
        {
            electric[i].Play();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void StartCoroutineElectric()
    {
        StartCoroutine(PartyclesCannon());
    }

    #endregion

}
