using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public bool doFade = false;
    public bool changeScene = false;
    public string nextSceneName = "";

    [SerializeField]
    [Range(0f, 1f)]
    private float endAlphaVal;

    [SerializeField]
    [Range(1f, 2f)]
    private float fadeDuration;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        StartScene(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (doFade)
        {
            if (elapsedTime <= fadeDuration)
            {
                if (canvasGroup.alpha < endAlphaVal)
                {
                    canvasGroup.alpha += Time.deltaTime / fadeDuration;
                }
                else
                {
                    canvasGroup.alpha -= Time.deltaTime / fadeDuration;
                }
            }
            else
            {
                canvasGroup.alpha = endAlphaVal;
                if (changeScene && nextSceneName != "")
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                doFade = false;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    private void StartScene(float duration)
    {
        canvasGroup.alpha = 1f;
        TriggerFade(0f, duration);
    }

    public void EndScene(float duration)
    {
        changeScene = true;
        canvasGroup.alpha = 0f;
        Debug.Log("Pressed");
        TriggerFade(1f, duration);
    }

    private void TriggerFade(float endAlpha, float duration)
    {
            if (!doFade)
            {
                doFade = true;
                endAlphaVal = endAlpha;
                fadeDuration = duration;
                elapsedTime = 0f;
            }
    }

}
