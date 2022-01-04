using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorPlain;
    public Texture2D cursorNavTarget;
    public Texture2D cursorInteract;

    void Start()
    {
        Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {

        // 1. Check if we hit an interactable
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {

                Debug.Log("Interactable");
                Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.Auto);
                return;
            }
            else
            {        
                // 2. Check if we hit a point on the navmesh
                NavMeshHit navMeshHit;

                if (NavMesh.SamplePosition(hit.transform.position, out navMeshHit, 0.2f, NavMesh.AllAreas))
                {
                    Debug.Log("NavMesh");
                    Cursor.SetCursor(cursorNavTarget, Vector2.zero, CursorMode.Auto);
                    return;
                }

            }

        }

        // 3. If all else fails just show the simple cursor
        Debug.Log("Default");
        Cursor.SetCursor(cursorPlain, Vector2.zero, CursorMode.Auto);


    }

}
