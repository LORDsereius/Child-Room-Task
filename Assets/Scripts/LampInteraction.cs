using UnityEngine;

public class LampInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]    private float outlineThickness = 0.005f;
    [SerializeField]    private Material outline, star;

    private bool isEmissionOn = false;
    private void Awake()
    {
        star = GetComponent<Renderer>().material;
    }
    public void Interact()
    {
        isEmissionOn = !isEmissionOn;
        if (isEmissionOn)
        {
            Debug.Log("On");
            star.EnableKeyword("_EMISSION");
            star.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        }
        else
        {
            Debug.Log("Off");
            star.DisableKeyword("_EMISSION");
            star.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        }
    }

    public void CancelInteraction()
    {

    }

    public void OnHoverEnter()
    {
        outline.SetFloat("_Thickness", outlineThickness);
    }

    public void OnHoverExit()
    {
        outline.SetFloat("_Thickness", 0);
    }
}
