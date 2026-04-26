/*
    Purpose: Manages the captcha minigame across 4 days.
    - Each day has 3 captcha stages with custom sprites and correct answer indices
    - Points persist across days (managed by DesktopManager)
    - Call SetDay(int day) from GoalsManager when a new day starts
    - Call ResetForNewDay() from GoalsManager
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CaptchaDay
{
    [Header("Instructions (3 stages)")]
    public string stage0Instruction = "Select all images containing a CAR";
    public string stage1Instruction = "Select all images containing a TREE";
    public string stage2Instruction = "Select all images containing a HUMAN";

    [Header("Correct Indices (0-8) per stage")]
    public int[] stage0Correct = new int[] { 0, 3, 7 };
    public int[] stage1Correct = new int[] { 1, 4, 6 };
    public int[] stage2Correct = new int[] { 2, 5, 8 };

    [Header("Sprites (9 per stage)")]
    public Sprite[] stage0Sprites = new Sprite[9];
    public Sprite[] stage1Sprites = new Sprite[9];
    public Sprite[] stage2Sprites = new Sprite[9];
}

public class CaptchaManager : MonoBehaviour
{
    [Header("References")]
    public GoalsManager goalsManager;
    public DesktopManager desktopManager;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI stageText;
    public Button submitButton;
    public GameObject gridContainer;

    [Header("Grid Images")]
    public Image[] gridImages = new Image[9];

    [Header("Days (0 = Day 1, 1 = Day 2, etc.)")]
    public CaptchaDay[] days = new CaptchaDay[4];

    // Internal state
    private int _currentDay = 0;
    private int _currentStage = 0;
    private bool[] _selected = new bool[9];
    private bool _isAnimating = false;
    private bool _allComplete = false;

    void OnEnable()
    {
        Debug.Log("CaptchaManager OnEnable, _currentDay: " + _currentDay + " _allComplete: " + _allComplete);
        if (_allComplete)
        {
            ShowCompletedState();
        }
        else
        {
            LoadStage();
        }
    }

    // Called from GoalsManager when a new day starts
    // Matches trueDay (1-4) and converts to 0-indexed
    public void SetDay(int trueDay)
    {
        _currentDay = Mathf.Clamp(trueDay - 1, 0, days.Length - 1);
    }

    // Called from GoalsManager's sections to reset captcha for new day
    public void ResetForNewDay(int trueDay)
    {
        SetDay(trueDay);
        _currentStage = 0;
        _allComplete = false;
        Debug.Log("Captcha reset for trueDay: " + trueDay + " -> _currentDay: " + _currentDay);
        if (gameObject.activeInHierarchy)
            LoadStage();
    }

    void LoadStage()
    {
        if (days == null || days.Length == 0) return;

        CaptchaDay day = days[_currentDay];
        Sprite[] sprites = GetCurrentSprites(day);
        int[] correct = GetCurrentCorrect(day);

        for (int i = 0; i < 9; i++)
        {
            _selected[i] = false;
            SetBorder(i, false);

            if (sprites != null && i < sprites.Length && sprites[i] != null)
            {
                gridImages[i].sprite = sprites[i];
                gridImages[i].color = Color.white;
            }
            else
            {
                // Placeholder: green = correct, red = incorrect
                gridImages[i].sprite = null;
                gridImages[i].color = IsCorrectIndex(i, correct) ? Color.green : Color.red;
            }
        }

        instructionText.text = GetCurrentInstruction(day);
        stageText.text = "Task " + (_currentStage + 1) + " of 3";
        submitButton.interactable = true;
        _isAnimating = false;
    }

    string GetCurrentInstruction(CaptchaDay day)
    {
        switch (_currentStage)
        {
            case 0: return day.stage0Instruction;
            case 1: return day.stage1Instruction;
            case 2: return day.stage2Instruction;
            default: return "";
        }
    }

    Sprite[] GetCurrentSprites(CaptchaDay day)
    {
        switch (_currentStage)
        {
            case 0: return day.stage0Sprites;
            case 1: return day.stage1Sprites;
            case 2: return day.stage2Sprites;
            default: return null;
        }
    }

    int[] GetCurrentCorrect(CaptchaDay day)
    {
        switch (_currentStage)
        {
            case 0: return day.stage0Correct;
            case 1: return day.stage1Correct;
            case 2: return day.stage2Correct;
            default: return new int[] {};
        }
    }

    bool IsCorrectIndex(int index, int[] correct)
    {
        foreach (int c in correct)
            if (c == index) return true;
        return false;
    }

//GRID INTERACTION

    public void OnGridImageClicked(int index)
    {
        if (_isAnimating) return;
        _selected[index] = !_selected[index];
        SetBorder(index, _selected[index]);
    }

    void SetBorder(int index, bool active)
    {
        Outline outline = gridImages[index].GetComponent<Outline>();
        if (outline != null)
            outline.enabled = active;
    }

    // ON SUBMIT

    public void OnSubmit()
    {
        if (_isAnimating) return;

        CaptchaDay day = days[_currentDay];
        int[] correct = GetCurrentCorrect(day);
        bool isCorrect = CheckAnswer(correct);

        if (isCorrect)
            StartCoroutine(CorrectSequence());
        else
            StartCoroutine(WrongSequence());
    }

    bool CheckAnswer(int[] correct)
    {
        HashSet<int> selected = new HashSet<int>();
        for (int i = 0; i < 9; i++)
            if (_selected[i]) selected.Add(i);

        HashSet<int> correctSet = new HashSet<int>(correct);
        return selected.SetEquals(correctSet);
    }

    // SEQUENCES

    IEnumerator CorrectSequence()
    {
        _isAnimating = true;
        submitButton.interactable = false;

        CaptchaDay day = days[_currentDay];
        int[] correct = GetCurrentCorrect(day);

        // Flash correct images green
        Color[] originalColors = new Color[9];
        for (int i = 0; i < 9; i++)
            originalColors[i] = gridImages[i].color;

        foreach (int i in correct)
            gridImages[i].color = Color.green;

        yield return new WaitForSeconds(0.8f);

        // Restore colors
        for (int i = 0; i < 9; i++)
            gridImages[i].color = originalColors[i];

        // Award point, points persist across days via DesktopManager
        desktopManager.AddPoints(1);
        _currentStage++;

        if (_currentStage >= 3)
        {
            _allComplete = true;
            ShowCompletedState();
            // Mark goal as complete in GoalsManager
            goalsManager.goalUseComputer = true;
            goalsManager.updateGoalText();
        }
        else
        {
            LoadStage();
        }
    }

    IEnumerator WrongSequence()
    {
        _isAnimating = true;
        submitButton.interactable = false;

        // Flash all cells red
        Color[] originalColors = new Color[9];
        for (int i = 0; i < 9; i++)
        {
            originalColors[i] = gridImages[i].color;
            gridImages[i].color = Color.red;
        }

        yield return new WaitForSeconds(0.5f);

        // Restore and reset selection
        for (int i = 0; i < 9; i++)
        {
            gridImages[i].color = originalColors[i];
            _selected[i] = false;
            SetBorder(i, false);
        }

        // On day 4, corrupt the points display on any wrong answer
        if (_currentDay >= 3) // day 4 = index 3
        {
            desktopManager.CorruptPoints();
        }

        submitButton.interactable = true;
        _isAnimating = false;
    }

    void ShowCompletedState()
    {
        instructionText.text = "All tasks complete! Well done.";
        stageText.text = "Verified";
        submitButton.interactable = false;
        _isAnimating = false;
    }
}