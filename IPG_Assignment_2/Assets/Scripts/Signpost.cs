using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Signpost : MonoBehaviour
{
    public bool endGameCriteriaMet = false;
    public List<Cow> Cows;


    private void OnMouseDown()
    {
        if (endGameCriteriaMet)
        {
            SceneManager.LoadScene("Ending");
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
