using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class StoryData
{
    public string _id;
    public string title;
    public string author;
    public string story;
    public string moral;
}

public class APIHandler : MonoBehaviour
{
    private string apiUrl = "https://shortstories-api.onrender.com/";

    private string storyId;
    private string storyTitle;
    private string storyAuthor;
    private string storyContent;
    private string storyMoral;

    public void FetchStory()
    {
        StartCoroutine(GetStory());
    }

    private IEnumerator GetStory()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch story: " + request.error);
        }
        else
        {
            StoryData data = JsonUtility.FromJson<StoryData>(request.downloadHandler.text);

            storyId      = data._id;
            storyTitle   = data.title;
            storyAuthor  = data.author;
            storyContent = data.story;
            storyMoral   = data.moral;

            Debug.Log("Story fetched successfully!");
        }
    }

    public string GetTitle()   => storyTitle;
    public string GetAuthor()  => storyAuthor;
    public string GetContent()   => storyContent;
    public string GetMoral()   => storyMoral;
}
