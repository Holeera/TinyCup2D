using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClickToAdvanceText : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI clickPromptText;

    [Header("Text Settings")]
    [TextArea] public string[] messages;
    [SerializeField] private float idleTimeThreshold = 5f;
    [SerializeField] private float clickCooldown = 1f;
    [SerializeField] private string nextSceneName;

    private int currentMessageIndex = 0;
    private float idleTimer = 0f;
    private bool promptVisible = false;
    private bool canClick = false;
    private CanvasGroup promptCanvasGroup;

    private void Start()
    {
        if (mainText == null || clickPromptText == null || messages.Length == 0)
        {
            Debug.LogError("Setup missing for ClickToAdvanceText.");
            enabled = false;
            return;
        }

        mainText.text = messages[0];

        promptCanvasGroup = clickPromptText.GetComponent<CanvasGroup>();
        if (promptCanvasGroup == null)
        {
            promptCanvasGroup = clickPromptText.gameObject.AddComponent<CanvasGroup>();
        }

        promptCanvasGroup.alpha = 0;
        clickPromptText.gameObject.SetActive(false);

        // Apply initial cooldown so first click is blocked briefly
        StartCoroutine(InitialCooldown());
    }

    private IEnumerator InitialCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(clickCooldown);
        canClick = true;
    }

    private void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTimeThreshold && !promptVisible)
        {
            ShowPrompt();
        }

        if (canClick && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            StartCoroutine(HandleClickWithCooldown());
        }
    }

    private IEnumerator HandleClickWithCooldown()
    {
        canClick = false;

        if (promptVisible)
        {
            HidePrompt();
        }

        idleTimer = 0f;
        AdvanceTextOrScene();

        yield return new WaitForSeconds(clickCooldown);
        canClick = true;
    }

    private void AdvanceTextOrScene()
    {
        currentMessageIndex++;

        if (currentMessageIndex < messages.Length)
        {
            mainText.text = messages[currentMessageIndex];
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ShowPrompt()
    {
        promptVisible = true;
        clickPromptText.gameObject.SetActive(true);
        StartCoroutine(FadePrompt(true));
    }

    private void HidePrompt()
    {
        promptVisible = false;
        StartCoroutine(FadePrompt(false));
    }

    private IEnumerator FadePrompt(bool fadeIn)
    {
        float duration = 0.5f;
        float start = fadeIn ? 0 : 1;
        float end = fadeIn ? 1 : 0;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            promptCanvasGroup.alpha = Mathf.Lerp(start, end, t);
            yield return null;
        }

        promptCanvasGroup.alpha = end;

        if (!fadeIn)
        {
            clickPromptText.gameObject.SetActive(false);
        }
    }
}