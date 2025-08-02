using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIHandler : MonoBehaviour
{
    private string storyTitle;
    private string storyAuthor;
    private string storyContent;
    private string storyMoral;

    private bool storyReady = false;

    private const string apiURL = "https://shortstories-api.onrender.com/";

    public void RequestStory()
    {
        storyReady = false;
        StartCoroutine(FetchStoryCoroutine());
    }

    private IEnumerator FetchStoryCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("API Error: " + request.error);
            storyTitle = "Error in Getting the story.";
            storyContent = "There's something wrong with the server";
        }
        else
        {
            string json = request.downloadHandler.text;
            SimpleStoryData data = JsonUtility.FromJson<SimpleStoryData>(json);
            storyTitle = data.title;
            storyAuthor = data.author;
            storyContent = data.story;
            storyMoral = data.moral;
        }

        storyReady = true;
    }

    public bool IsStoryReady()
    {
        return storyReady;
    }

    public string GetTitle()
    {
        return storyTitle;
    }

    public string GetAuthor()
    {
        return storyAuthor;
    }

    public string GetContent()
    {
        return storyContent;
    }

    public string GetMoral()
    {
        return storyMoral;
    }

    [System.Serializable]
    private class SimpleStoryData
    {
        public string title;
        public string author;
        public string story;
        public string moral;
    }
}
