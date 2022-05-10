using UnityEngine;
using UnityEngine.UI;

public class MessageBox : Singleton<MessageBox>
{
    public Text title;
    public Text message;
    public Button button;
    public GameObject body;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            body.SetActive(false);
        });
    }

    public void SetData(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;
    }

    private static void Show(string title, string message)
    {
        Instance.body.SetActive(true);
        Instance.SetData(title, message);
    }

    public static void ShowSuccess(string message)
    {
        Show("Success", message);
    }

    public static void ShowError(string message)
    {
        Show("Error", message);
    }
}