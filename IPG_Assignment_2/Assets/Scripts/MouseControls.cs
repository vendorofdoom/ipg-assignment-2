using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MouseControls : MonoBehaviour
{
    [Header("Cursor Icons")]
    public Texture2D cursorPlain;
    public Texture2D cursorNavTarget;
    public Texture2D cursorInteract;
    public Texture2D cursorAdd;
    public Texture2D cursorInfo;

    [Header("Herdsperson Properties")]
    public HerdspersonController hpController;
    public Transform hpTransform;
    public Inventory hpInventory;
    public float hpReachDist;

    [Header("NavMesh Properties")]
    public float NavMeshClickHitDist;

    void Start()
    {
        Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
    }

    //https://answers.unity.com/questions/973606/how-can-i-tell-if-the-mouse-is-over-the-game-windo.html
    bool IsMouseOverGameWindow 
    { 
        get 
        { 
            return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y);
        }
    }

    private void PickFlower(GameObject flower)
    {
        hpInventory.addFlower(flower.GetComponent<Flower>().flowerType); // Add to inventory
        flower.SetActive(false); // Flower picked so disappear!

    }

    private void HandleMouseInput()
    {
        // Change cursor depending on mouse position and handle certain mouse down events

        // TODO: should change to a switch statement?
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit) && !EventSystem.current.IsPointerOverGameObject()) // && !EventSystem.current.IsPointerOverGameObject() to check we're not over a UI element
        {

            // Interactable object hover
            // TODO: Implement mouse click handle
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.Auto);
            }


            // Collectible proximity hover and click handle
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Collectible"))
            {

                if (Vector3.Distance(hit.transform.position, hpTransform.position) < hpReachDist)
                {
                    Cursor.SetCursor(cursorAdd, new Vector2(cursorAdd.width / 2, cursorAdd.height / 2), CursorMode.Auto); // Center this cursor icon a la: https://wintermutedigital.com/post/2020-01-29-the-ultimate-guide-to-custom-cursors-in-unity/

                    if (Input.GetMouseButtonDown(0) & hit.collider.gameObject.CompareTag("Flower"))
                    {
                        Debug.Log("Flower picked!");
                        PickFlower(hit.collider.gameObject);
                        //hpInventory.DisplayUI();
                    }
                
                }

            }


            // Cow hover cursor
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Cow"))
            {
                Cursor.SetCursor(cursorInfo, Vector2.zero, CursorMode.Auto);
            }


            // Check if we've hit navmesh and if mouse down, set a new target for the herdsperson
            else
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

        // Default to show simple cursor
        else
        {
            Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
        }
    }


    private void Update()
    {

        if (IsMouseOverGameWindow) // no point changing cursor if outside game window
        {
            HandleMouseInput();
        }
        

    }

}



