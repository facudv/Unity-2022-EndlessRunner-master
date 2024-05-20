using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public ParticleSystem startEffect;

    private void Reset()
    {
        startEffect.Play();
    }

    public static void TurnOn(Debuff d)
    {
        d.gameObject.SetActive(true);
        d.Reset();
    }

    public static void TurnOff(Debuff d)
    {
        d.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        //devolveme al pool
        if (other.gameObject.tag == "restart")
        {
            DebuffSpawner.Instance.ReturnDebuff(this);
        }

        if(other.gameObject.GetComponent<Platform>())
        {
            Debug.Log("Debuff:choque contra el piso");
            //rb.isKinematic = true;
        }
    }
}
