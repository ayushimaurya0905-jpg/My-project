using UnityEngine;
using System.Collections.Generic;

public class UIScreenManager : MonoBehaviour
{
    public static UIScreenManager Instance { get; private set; }

    [SerializeField] private GameObject startScreen; // drag MissionScreen here

    private readonly Stack<GameObject> history = new Stack<GameObject>();
    private GameObject currentScreen;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        currentScreen = startScreen;
    }

    /// Show a new screen. The screen you're leaving gets pushed onto history
    /// so GoBack() can return to it.
    public void ShowScreen(GameObject screen)
    {
        if (currentScreen == screen) return;

        history.Push(currentScreen);
        currentScreen.SetActive(false);
        screen.SetActive(true);
        currentScreen = screen;
    }

    /// Returns to whatever screen was active before. Safe to call
    /// even if history is empty (does nothing at the root screen).
    public void GoBack()
    {
        if (history.Count == 0) return;

        currentScreen.SetActive(false);
        currentScreen = history.Pop();
        currentScreen.SetActive(true);
    }
}