using UnityEngine;
using System.Collections;
public class UIPanelAnimation : MonoBehaviour
{
    private RectTransform panel;
    public float slideDuration = 0.5f;
    private bool isVisible = false;

    private Vector2 hiddenPosition;
    private Vector2 shownPosition;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        float panelWidth = Screen.width * 0.33f;
        hiddenPosition = new Vector2(-panelWidth, 0);
        shownPosition = Vector2.zero;
        panel.anchoredPosition = hiddenPosition;
    }

    public void TogglePanel()
    {
        StopAllCoroutines();
        isVisible = !isVisible;
        StartCoroutine(SlidePanel(isVisible ? shownPosition : hiddenPosition));

        Time.timeScale = isVisible ? 0f : 1f;
    }

    private IEnumerator SlidePanel(Vector2 target)
    {
        float elapsed = 0f;
        Vector2 start = panel.anchoredPosition;

        while (elapsed < slideDuration)
        {
            panel.anchoredPosition = Vector2.Lerp(start, target, elapsed / slideDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        panel.anchoredPosition = target;
    }
}
