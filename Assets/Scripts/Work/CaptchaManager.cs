using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptchaManager : MonoBehaviour
{
    [Header("References")]
    public DesktopManager desktopManager;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI stageText;
    public Button submitButton;
    public GameObject gridContainer; // parent of the 9 image buttons

    [Header("Grid Images")]

    // go back and assign 9 Image components in Inspector after photobashing sprites...need car, tree, and humans
    public Image[] gridImages = new Image[9]; 
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color neutralColor = Color.white;
    public Color selectedBorderColor = Color.yellow;
    private bool _allComplete = false;

    [Header("Stage Config")]
    // for each stage, mark which grid indices (0-8) are correct
    // Stage 0 = Car, Stage 1 = Tree, Stage 2 = Human
    private string[] stageInstructions = {
        "Select all images containing a CAR",
        "Select all images containing a TREE",
        "Select all images containing a HUMAN"
    };

    // which indices are correct for each stage // go back and edit these to match images
    private int[][] correctIndices = {
        // car stage: indices 0, 3, 7 are cars
        new int[] { 0, 3, 7 },
        // tree stage: indices 1, 4, 6 are trees
        new int[] { 1, 4, 6 },
        // human stage: indices 2, 5, 8 are humans
        new int[] { 2, 5, 8 }
    };

    private int _currentStage = 0;
    private bool[] _selected = new bool[9];
    private bool _isAnimating = false;

    void OnEnable()
    {
        if (_allComplete)
        {
            // show completed state, dont reset
            instructionText.text = "All tasks complete! Well done.";
            stageText.text = "Verified";
            submitButton.interactable = false;
        }
        else
        {
            LoadStage();
        }
    }

    void ResetCaptcha()
    {
        _currentStage = 0;
        LoadStage();
    }

    void LoadStage()
    {
        // clear selection
        for (int i = 0; i < 9; i++)
        {
            _selected[i] = false;
            SetBorder(i, false);
            // set placeholder colors: green = correct, red = incorrect for that stage
            gridImages[i].color = IsCorrectIndex(i) ? correctColor : incorrectColor;
        }

        instructionText.text = stageInstructions[_currentStage];
        stageText.text = "Task " + (_currentStage + 1) + " of 3";
        submitButton.interactable = true;
        _isAnimating = false;
    }

    bool IsCorrectIndex(int index)
    {
        foreach (int correct in correctIndices[_currentStage])
            if (correct == index) return true;
        return false;
    }

    public void OnGridImageClicked(int index)
    {
            Debug.Log("Cell clicked: " + index);
            
        if (_isAnimating) return;
        _selected[index] = !_selected[index];
        SetBorder(index, _selected[index]);
    }

    void SetBorder(int index, bool active)
    {
        // outline component on each image shows selection
        Outline outline = gridImages[index].GetComponent<Outline>();
        if (outline != null)
            outline.enabled = active;
    }

    public void OnSubmit()
    {
            Debug.Log("Submit clicked");

        if (_isAnimating) return;

        bool correct = CheckAnswer();

        if (correct)
            StartCoroutine(CorrectSequence());
        else
            StartCoroutine(WrongSequence());
    }

    bool CheckAnswer()
    {
        // selected indices must exactly match correct indices
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

        // flash all correct images bright green
        foreach (int i in correctIndices[_currentStage])
            gridImages[i].color = Color.green;

        yield return new WaitForSeconds(0.8f);

        // add point to captcha point tracker
        desktopManager.AddPoints(1);
        _currentStage++;

        if (_currentStage >= 3)
        {
            // all captchas complete
            _allComplete = true;
            instructionText.text = "All tasks complete! Well done.";
            stageText.text = "Verified";
            submitButton.interactable = false;
            _isAnimating = false;
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

        // flash all images red briefly
        Color[] originalColors = new Color[9];
        for (int i = 0; i < 9; i++)
        {
            originalColors[i] = gridImages[i].color;
            gridImages[i].color = Color.red;
        }

        yield return new WaitForSeconds(0.5f);

        // restore colors and reset selection
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