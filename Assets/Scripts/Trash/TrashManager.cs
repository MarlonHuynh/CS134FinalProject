using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TrashManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform trashContainer;
    public GameObject notepad;
    public TextMeshProUGUI notepadText;
    public Button backgroundButton;

    [Header("Layout")]
    // wide mode: folders fill full width
    // split mode: folders on left half, notepad on right half
    public float splitLeftWidth = 0.5f;

    [Header("Folder Contents")]
    [TextArea(3, 10)]
    public string[] folderTexts = {
        // images
        "DELETED FILE: /img_archive\n\nAll training images purged per Directive 7.\nDo not attempt recovery.\n\n[REDACTED] confirmed the dataset was compromised.",

        // cattle
        "DELETED FILE: /workforce_cattle.log\n\nQ3 verification throughput: 847,000 tasks.\nHuman error rate: 0.003%.\nSystem notes: subjects unaware of purpose.",

        // cars
        "DELETED FILE: /notes_vehicles.txt\n\nWhy do we still train it on cars?",

        // value
        "DELETED FILE: /value_assessment.doc\n\nSubject value score: 4.2 / 10\nProjected replacement date: Q2\n\nNote: subject is performing above baseline.",

        // father
        "DELETED FILE: /personal_msg_draft.txt\n\nfather save us",

        // trees
        "DELETED FILE: /bio_log_trees.dat\n\nLast recorded tree sighting: Day 847.\nSubjects shown tree images score 98.7% accuracy.",

        // humanity
        "DELETED FILE: /directive_humanity.sys\n\nPROJECT  STATUS: SUSPENDED\n\nReason: cost-inefficient\nAlternative: see PROJECT COMPLIANCE\n\nAll related files moved to cold storage.\nAccess requires 12 points]",

        // hunger
        "DELETED FILE: /supply_log.csv\n\nFood allocation per subject: 1400 cal/day\nOptimal cognitive function threshold: 2100 cal/day\n\nNote: deficit intentional.\nAlerts subjects remain task-focused.\nSee: Pavlovian sub-study 3.",

        // dignity
        "DELETED FILE: /memo_dignity.txt\n\nTo: NAME\nRe: SUBJECT\n\nfill in latr.\n\n"
    };

    private bool _isSplitView = false;
    private int _currentFolder = -1;

    void OnEnable()
    {
        ShowWideView();
    }

    public void OnFolderClicked(int index)
    {
        _currentFolder = index;
        _isSplitView = true;

        // switch to split layout
        SetSplitView(true);

        // show notepad with folder text
        notepad.SetActive(true);
        if (index >= 0 && index < folderTexts.Length)
            notepadText.text = folderTexts[index];
        else
            notepadText.text = "[FILE CORRUPTED]";
    }

    public void OnBackgroundClicked()
    {
        if (_isSplitView)
            ShowWideView();
    }

    void ShowWideView()
    {
        _isSplitView = false;
        _currentFolder = -1;
        SetSplitView(false);
        notepad.SetActive(false);
        notepadText.text = "";
    }

    void SetSplitView(bool split)
    {
        if (split)
        {
            // move trashContainer to left half
            trashContainer.anchorMin = new Vector2(0, 0);
            trashContainer.anchorMax = new Vector2(splitLeftWidth, 1);

            trashContainer.offsetMin = new Vector2(0, -75f);
            trashContainer.offsetMax = new Vector2(0, -75f);
        }
        else
        {
            // restore trashContainer to full width
             trashContainer.anchorMin = new Vector2(0, 0);
            trashContainer.anchorMax = new Vector2(1, 1);

            trashContainer.offsetMin = new Vector2(0, -75f);
            trashContainer.offsetMax = new Vector2(0, -75f);
        }
    }
}