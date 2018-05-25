using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateTexture : MonoBehaviour {

    public float speed;

    private Material mat;
    private float offset;
    private const string tex = "_MainTex";

	void Start ()
    {
        mat = GetComponent<Image>().material;
	}
	
	void Update ()
    {
        offset += Time.deltaTime * speed;
        mat.SetTextureOffset(tex, new Vector2(offset, 0));
	}
}