using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

public class MouseControls : MonoBehaviour
{
    [Header("Cursor Icons")]
    public Texture2D cursorPlain;
    public Texture2D cursorNavTarget;
    public Texture2D cursorInteract;
    public Texture2D cursorAdd;
    public Texture2D cursorInfo;
    public Texture2D cursorStar;

    [Header("Herdsperson Properties")]
    public HerdspersonController hpController;
    public Transform hpTransform;
    public FlowersCollection hpInventory;
    public FlowerHUD flowerHUD;
    public float hpReachDist;

    [Header("NavMesh Properties")]
    public float NavMeshClickHitDist;

    [Header("Signpost")]
    [SerializeField]
    private Signpost signpost;

    [Header("Post Processing")]
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;

    [Header("UI")]
    public CanvasGroup narrativeUI;

    bool IsMouseOverGameWindow //https://answers.unity.com/questions/973606/how-can-i-tell-if-the-mouse-is-over-the-game-windo.html
    {
        get
        {
            return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y);
        }
    }

    void Start()
    {
        Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    private void Update()
    {
        if (IsMouseOverGameWindow) // no point changing cursor if outside game window
        {
            HandleMouseInput();
        }
    }
    private void HandleMouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !EventSystem.current.IsPointerOverGameObject()) // && !EventSystem.current.IsPointerOverGameObject() to check we're not over a UI element
        {


            switch (hit.transform.gameObject.tag.ToString())
            {
                case ("Cabin"):

                    CabinMouseover();
                    break;

                case ("Mushroom"):
                    MushroomMouseover(hit.collider.gameObject);
                    break;

                case ("Flower"):
                    FlowerMouseover(hit);
                    break;

                case ("Signpost"):
                    SignpostMouseover();
                    break;

                case ("Cow"):
                    Cursor.SetCursor(cursorInfo, Vector2.zero, CursorMode.Auto);
                    break;

                default:
                    NavMeshCheck(hit);
                    break;
            }

        }

        // Default to show simple cursor
        else
        {
            Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
        }
    }

    private void CabinMouseover()
    {
        Cursor.SetCursor(cursorInfo, Vector2.zero, CursorMode.Auto);
        if (Input.GetMouseButtonDown(0) && narrativeUI.gameObject.activeSelf != true)
        {
            narrativeUI.gameObject.SetActive(true);
        }
    }

    private void MushroomMouseover(GameObject mushroom)
    {
        if (Vector3.Distance(mushroom.transform.position, hpTransform.position) < hpReachDist)
        {
            Cursor.SetCursor(cursorAdd, new Vector2(cursorAdd.width / 2, cursorAdd.height / 2), CursorMode.Auto);

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Intoxicated(2.5f, 2.5f, 0f));
                mushroom.SetActive(false);
            }
        }    
    }

    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples
    IEnumerator Intoxicated(float lerpInTime, float lerpOutTime, float duration)
    {
        float time = 0f;

        while (time < lerpInTime)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, time / lerpInTime);
            colorGrading.hueShift.value = Mathf.Lerp(0, 180, time / lerpInTime);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        while ((time >= lerpInTime) && (time < lerpInTime + lerpOutTime))
        {

            chromaticAberration.intensity.value = Mathf.Lerp(1f, 0f, (time - lerpInTime) / lerpOutTime);
            colorGrading.hueShift.value = Mathf.Lerp(-180, 0, (time - lerpInTime) / lerpInTime);

            time += Time.deltaTime;
            yield return null;
        }

        chromaticAberration.intensity.value = 0f;
        colorGrading.hueShift.value = 0f;
    }

    private void SignpostMouseover()
    {
        if (signpost.endGameCriteriaMet)
        {
            Cursor.SetCursor(cursorStar, new Vector2(cursorStar.width / 2, cursorStar.height / 2), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.Auto);
        }
    }

    private void FlowerMouseover(RaycastHit hit)
    {

        if (Vector3.Distance(hit.transform.position, hpTransform.position) < hpReachDist)
        {
            Cursor.SetCursor(cursorAdd, new Vector2(cursorAdd.width / 2, cursorAdd.height / 2), CursorMode.Auto); // Center this cursor icon a la: https://wintermutedigital.com/post/2020-01-29-the-ultimate-guide-to-custom-cursors-in-unity/
            if (Input.GetMouseButtonDown(0))
            {
                hpInventory.AddFlower(hit.collider.gameObject.GetComponent<Flower>().flowerType);
                hit.collider.gameObject.SetActive(false); // Flower picked so disappear!
                flowerHUD.UpdateUI();
            }
        }
    }

    private void NavMeshCheck(RaycastHit hit)
    {
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(hit.transform.position, out navMeshHit, NavMeshClickHitDist, NavMesh.AllAreas))
        {
            Cursor.SetCursor(cursorNavTarget, Vector2.zero, CursorMode.Auto);
            if (Input.GetMouseButtonDown(0))
            {
                hpController.SetNewTarget(hit.transform.position);
            }
        }
        else
        {
            Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
        }
    }

}



