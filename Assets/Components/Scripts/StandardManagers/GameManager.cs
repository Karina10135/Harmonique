using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool dialog;
    public bool recording;
    public bool moleSequence;

    public GameObject fadeCanvas;
    public static GameManager GM;

    public float fadeSpeed;
    float val;

    private void Awake()
    {
        GM = this;
        if(GM != this) { Destroy(this); }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
    }

    public void SceneChange(string name)
    {

        SceneManager.LoadScene(name);
        
    }

    public void UpdatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FadingOutOfScene(string scene)
    {
        StartCoroutine(FadeTimer(true, scene));

    }

    public void FadingInToScene()
    {
        StartCoroutine(FadeTimer(false, "Main"));

    }

    IEnumerator FadeTimer(bool fade, string name)
    {
        var fadeCan = fadeCanvas.GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(1f);
        val = fadeCan.alpha;

        if (fade)
        {
            while (val < 1f)
            {
                val += Time.deltaTime * fadeSpeed;
                fadeCan.alpha = val;

                if(val >= 1f)
                {
                    val = 1f;

                    if(name != null)
                    {
                        SceneManager.LoadScene(name);
                    }

                    yield break;
                }
                yield return null;
            }
        }
        else 
        {


            while (val > 0f)
            {

                val -= Time.deltaTime * fadeSpeed;
                fadeCan.alpha = val;

                if (val <= 0f)
                {
                    val = 0f;
                    fadeCan.alpha = val;
                    yield break;
                }
                yield return null;

            }
        }
        
    }

}
