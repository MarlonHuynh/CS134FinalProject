using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Color selectedBorderColor = Color.yellow;

    [Header("Stage Sprites")]
    public Sprite[] stage0Sprites = new Sprite[9]; // car stage
    public Sprite[] stage1Sprites = new Sprite[9]; // tree stage
    public Sprite[] stage2Sprites = new Sprite[9]; // human stage

    [Header("Stage Config")]
    private string[] stageInstructions = {
        "Select all images containing a CAR",
        "Select all images containing a TREE",
        "Select all images containing a HUMAN"
    };

    // which indices are correct for each stage
    private int[][] correctIndices = {
        new int[] { 0, 3, 7 },   // car stage
        new int[] { 1, 4, 6 },   // tree stage
        new int[] { 2, 5, 8 }    // human stage
    };

    private int _currentStage = 0;
    private bool[] _selected = new bool[9];
    private bool _isAnimating = false;
    private bool _allComplete = false;

    void OnEnable()
    {
        if (_allComplete)
        {
            instructionText.text = "All tasks complete! Well done.";
            stageText.text = "Verified";
            submitButton.interactable = false;
        }
        else
        {
            LoadStage();
        }
    }

    void LoadStage()
    {
        for (int i = 0; i < 9; i++)
        {
            _selected[i] = false;
            SetBorder(i, false);

            Sprite[] current = GetCurrentSprites();
            if (current != null && current[i] != null)
            {
                gridImages[i].sprite = current[i];
                gridImages[i].color = Color.white;
            }
            else
            {
                // placeholder: green = correct, red = incorrect
                gridImages[i].sprite = null;
                gridImages[i].color = IsCorrectIndex(i) ? Color.green : Color.red;
            }
        }

        instructionText.text = stageInstructions[_currentStage];
        stageText.text = "Task " + (_currentStage + 1) + " of 3";
        submitButton.interactable = true;
        _isAnimating = false;
    }

    Sprite[] GetCurrentSprites()
    {
        switch (_currentStage)
        {
            case 0: return stage0Sprites;
            case 1: return stage1Sprites;
            case 2: return stage2Sprites;
            default: return null;
        }
    }

    bool IsCorrectIndex(int index)
    {
        foreach (int correct in correctIndices[_currentStage])
            if (correct == index) return true;
        return false;
    }

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

    public void OnSubmit()
    {
        if (_isAnimating) return;

        bool correct = CheckAnswer();

        if (correct)
            StartCoroutine(CorrectSequence());
        else
            StartCoroutine(WrongSequence());
    }

    bool CheckAnswer()
    {
        HashSet<int> selected = new HashSet<int>();
        for (int i = 0; i < 9; i++)
            if (_selected[i]) selected.Add(i);

        HashSet<int> correct = new HashSet<int>(correctIndices[_currentStage]);
        return selected.SetEquals(correct);
    }

    IEnumerator CorrectSequence()
    {
        _isAnimating = true;
        submitButton.interactable = false;

        // flash correct images green
        Color[] originalColors = new Color[9];
        for (int i = 0; i < 9; i++)
            originalColors[i] = gridImages[i].color;

        foreach (int i in correctIndices[_currentStage])
            gridImages[i].color = Color.green;

        yield return new WaitForSeconds(0.8f);

        // restore colors
        for (int i = 0; i < 9; i++)
            gridImages[i].color = originalColors[i];

        desktopManager.AddPoints(1);
        _currentStage++;

        if (_currentStage >= 3)
        {
            _allComplete = true;
            instructionText.text = "All tasks complete! Well done.";
            stageText.text = "Verified";
            submitButton.interactable = false;
            _isAnimating = false;
            // Update use computer goal
            goalsManager.goalUseComputer = true; 
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

        // flash all cells red
        Color[] originalColors = new Color[9];
        for (int i = 0; i < 9; i++)
        {
            originalColors[i] = gridImages[i].color;
            gridImages[i].color = Color.red;
        }

        yield return new WaitForSeconds(0.5f);

        // restore and reset selection
        for (int i = 0; i < 9; i++)
        {
            gridImages[i].color = originalColors[i];
            _selected[i] = false;
            SetBorder(i, false);
        }

        submitButton.interactable = true;
        _isAnimating = false;
    }
}