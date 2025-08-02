using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryUIHandler : MonoBehaviour
{
    public APIHandler fetcher;
    public TMP_Text titleText;
    public TMP_Text storyText;
    //public TMP_Text loadingText;

    public void OnGetStoryButtonClicked()
    {
        StartCoroutine(LoadStory());
    }

    private IEnumerator LoadStory()
    {
        //loadingText.gameObject.SetActive(true);
        //loadingText.text = "در حال بارگذاری داستان...";

        titleText.text = "Loading...";
        storyText.text = "Loading...";

        fetcher.RequestStory();

        while (!fetcher.IsStoryReady())
        {
            yield return null;
        }

        //loadingText.gameObject.SetActive(false);
        
        UpdateStoryUI();
    }

    public void UpdateStoryUI()
    {
        titleText.text = fetcher.GetTitle();
        storyText.text = fetcher.GetContent();
    }
}
