using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryUIHandler : MonoBehaviour
{
    public APIHandler fetcher;
    public TMP_Text titleText;
    //public TextMeshPro authorText;
    public TMP_Text storyText;
    //public TextMeshPro moralText;

    public void UpdateStoryUI()
    {
        titleText.text  = fetcher.GetTitle();
        //authorText.text = fetcher.GetAuthor();
        storyText.text  = fetcher.GetContent();
        //moralText.text  = fetcher.GetMoral();
    }
}