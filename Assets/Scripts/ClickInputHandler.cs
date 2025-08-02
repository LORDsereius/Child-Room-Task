using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private InputActionSystem inputActions;

    [SerializeField] private PointAndClickMovement movement;

    private IInteractable currentInteractable;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new InputActionSystem();
    }

    private void OnEnable()
    {
        inputActions.Child.Click.performed += OnClick;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Child.Click.performed -= OnClick;
        inputActions.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 clickPosition = inputActions.Child.ClickMove.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(clickPosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && !hit.collider.CompareTag("RaycastBlocker"))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                var newInteractable = hit.collider.GetComponent<IInteractable>();

                if (currentInteractable != null && currentInteractable != newInteractable)
                {
                    currentInteractable.CancelInteraction();
                }
            
                if (newInteractable != currentInteractable)
                {
                    currentInteractable = newInteractable;
                    currentInteractable.Interact();   
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable.CancelInteraction();
                    currentInteractable = null;
                }

                movement.MoveTo(hit.point);
                Debug.Log("Move to: " + hit.point);
            }
        }
    }
}
