using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public SceneChange sceneChange;

    public void PressPlay()
    {
        sceneChange.EndScene(2f);
    }
}
