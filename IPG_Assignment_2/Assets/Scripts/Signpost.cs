using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signpost : MonoBehaviour
{
    public bool endGameCriteriaMet = false;
    public List<Cow> Cows;
    public SceneChange sceneChange;

    private void OnMouseDown()
    {
        if (endGameCriteriaMet)
        {
            sceneChange.EndScene(5f);
        }
        else
        {
            // TODO: Display a hint saying to come back once all the cows have their crowns?
        }
    }

    private void Update()
    {

        if (!endGameCriteriaMet)
        {
            bool allCowsInCrowns = true;
            foreach (Cow c in Cows)
            {
                allCowsInCrowns &= c.isWearingCrown;
            }
            endGameCriteriaMet = allCowsInCrowns;
        }
    }
}
