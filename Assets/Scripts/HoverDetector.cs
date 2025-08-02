using UnityEngine;
using UnityEngine.InputSystem;

public class HoverDetector : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject currentHoveredObject;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Interactable"))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (currentHoveredObject != hitObject)
            {
                ClearHover();
                currentHoveredObject = hitObject;
                
                var interactable = currentHoveredObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.OnHoverEnter();
                }
            }
        }
        else
        {
            ClearHover();
        }
    }

    private void ClearHover()
    {
        if (currentHoveredObject != null)
        {
            var interactable = currentHoveredObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.OnHoverExit();
            }

            currentHoveredObject = null;
        }
    }
}
