using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNote : MonoBehaviour
{
    public Light lightObject;
    public bool on;

    public void LightTrigger(bool l)
    {

        lightObject.gameObject.SetActive(l);
    }

}
