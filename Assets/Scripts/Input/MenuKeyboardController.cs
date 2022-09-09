using System.Collections;
using System.Collections.Generic;
using TKOU.SimAI;
using UnityEngine;
using UnityEngine.UI;

public class MenuKeyboardController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIMenuHighlighter highlighter;

    [Header("Settings")]
    [SerializeField] bool wrap;

    RectTransform rt;
    int currentIndex = 0;
    int indexCount;
    List<Button> _buttons = new List<Button>();
    GameController gameController;
    bool isInputActive;

    void Awake()
    {
        rt = GetComponent<RectTransform>();

        _buttons.AddRange(GetComponentsInChildren<Button>());
        indexCount = _buttons.Count;
    }

    public void Init(GameController gameControllerRef)
    {
        gameController = gameControllerRef;
        isInputActive = true;

        gameController.OnGameRun += React_OnGameRun;
        gameController.OnGameEnd += React_OnGameEnd;
    }

    void Start()
    {
        StartCoroutine(SetIndexLate());
    }

    IEnumerator SetIndexLate()
    {
        yield return null;
        SetIndex(0);
    }

    public void ActivateHighlighter(bool value)
    {
        highlighter.gameObject.SetActive(value);
    }

    public void HandleUpdate()
    {
        if (!isInputActive) return;
        if (Input.GetKeyDown(KeyCode.DownArrow)) IncrementIndex();
        if (Input.GetKeyDown(KeyCode.UpArrow)) DecrementIndex();
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) AcceptButton();
    }

    void AcceptButton()
    {
        _buttons[currentIndex].onClick?.Invoke();
    }

    void IncrementIndex()
    {
        if (currentIndex >= indexCount-1)
        {
            if (wrap) SetIndex(0);
        }
        else
            SetIndex(currentIndex + 1);
    }

    void SetIndex(int idx)
    {
        currentIndex = idx;
        highlighter.ShowAt(_buttons[idx].transform);
    }

    void DecrementIndex()
    {
        if (currentIndex <= 0)
        {
            if (wrap) SetIndex(indexCount-1);
        }
        else
            SetIndex(currentIndex - 1);
    }

    void React_OnGameRun()
    {
        isInputActive = false;
    }

    void React_OnGameEnd()
    {
        isInputActive = true;
    }
}
