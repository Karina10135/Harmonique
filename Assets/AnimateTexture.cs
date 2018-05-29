using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateTexture : MonoBehaviour {

    public float speed;
    public Vector2 offset;

    private Material mat;
    private const string tex = "_MainTex";

	void Start ()
    {
        mat = GetComponent<Image>().material;
	}
	
	void Update ()
    {
        var x = offset.x += Time.deltaTime * speed;
        var y = offset.y;
            //offset.y += Time.deltaTime * speed;

        mat.SetTextureOffset(tex, new Vector2(x,y));
	}
}