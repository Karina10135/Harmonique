using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateTexture : MonoBehaviour {

    public float speed;
    public Vector2 offset;
    public bool waterfall;

    private Material mat;
    private const string tex = "_MainTex";

	void Start ()
    {
        mat = GetComponent<MeshRenderer>().material;

    }

    void Update ()
    {
        var x = offset.x;
        var y = offset.y += Time.deltaTime * speed;

        mat.SetTextureOffset(tex, new Vector2(x,y));

        if (waterfall)
        {
            TriggerSound();
        }
	}

    public void TriggerSound()
    {
        Fabric.EventManager.Instance.PostEvent("Misc/Waterfall", gameObject);
        print("Playing sound");
    }
}