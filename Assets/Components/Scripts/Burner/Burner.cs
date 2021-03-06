﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Burner : MonoBehaviour
{
    public GameObject noNote;
    public Text leafCountText;
    public int maxLeafCount;
    public GameObject FireParticle;
    public ParticleSystem smoke;
    int currentCount;
    bool completed;

    private void Start()
    {
        currentCount = maxLeafCount;
        leafCountText.text = currentCount.ToString();
        Fabric.EventManager.Instance.PostEvent("Misc/Fire", FireParticle);
    }

    public void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Leaf"))
        {

            smoke.Play();
            Fabric.EventManager.Instance.PostEvent("Misc/Leafburn", other.gameObject);
            other.GetComponent<Leaf>().BurnLeaf();
            if(currentCount <= 0) { return; }
            currentCount--;
            leafCountText.text = currentCount.ToString();
            if (currentCount == 0)
            {
                noNote.SetActive(true);
            }
        }
    }

    public void BurnTrigger()
    {
        smoke.Play();
    }

}
