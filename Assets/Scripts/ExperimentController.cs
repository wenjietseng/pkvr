using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperimentController : MonoBehaviour
{
    [Header("Participant Info")]
    public int participantID = 0;
    public enum GenderMatchedAvatar { Men = 0, Women = 1 };
    public GenderMatchedAvatar genderMatchedAvatar;

    [Header("Conditions")]
    public int layoutBlockNum = 0;
    /// <summary> a Latin square for 4 conditions
    /// 4 layouts of physical targets
    /// A C B D | P0, P4, P8 
    /// B A D C | P1, P5, P9
    /// C D A B | P2, P6, P10
    /// D B C A | P3, P7, P11
    /// </summary>
    int[,] latinSquare4x4 = new int[4, 4] { {1, 3, 2, 4},
                                            {2, 1, 4, 3},
                                            {3, 4, 1, 2},
                                            {4, 2, 3, 1} };
    public enum VisuomotorType { Sync = 1, Async = 2, Async_begin = 3, Async_end = 4 };
    public VisuomotorType vmType;
    public int currentConditionNum;
    public float currentTime;
    private const float exposureDuration = 18f;
    private bool isAvatarRunning;
    private bool isQuestionnaireRunning;
    private bool isSwappedAvatars;
    public bool isQuestionnaireDone;


    //[Header("Avatars")]

    //[Header("Questionnaire")]
    private QuestionnaireController questionnaireController;



    void Start()
    {
        // enable the gender matched avatar
        questionnaireController = this.GetComponent<QuestionnaireController>();
        PrepareCondition();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // when start the experiment, we need to consider how to switch controller and hand tracking...
            // show avatar
            currentTime = 0f;
            isAvatarRunning = true;
            Debug.LogWarning("Please add instructions for the avatar part to a UI.");
            isSwappedAvatars = false;
        }



        if (currentConditionNum < 4)
        {
            if (isAvatarRunning)
            { 

                currentTime += Time.deltaTime;

                // difference between the four conditions.
                if (currentTime < exposureDuration)
                {
                    if (vmType == VisuomotorType.Async_begin || vmType == VisuomotorType.Async_end)
                    {
                        if (currentTime < (exposureDuration / 2))
                        {
                            Debug.LogWarning("first avatar");
                        }
                        else
                        {
                            if (!isSwappedAvatars)
                            {
                                isSwappedAvatars = true;
                                // swapp avatar for async/sync.
                                
                            }

                            if (isSwappedAvatars) Debug.LogWarning("swapped avatar in the a_begin or a_end conditions.");
                        }

                    }
                    else // A or S will show either async or sync avatar.
                    {
                        Debug.LogWarning("We don't do anything here");
                    }
                }
                else
                {
                    isAvatarRunning = false;
                    // stop showing avatar, disable avatars
                    isQuestionnaireRunning = true;
                    questionnaireController.questionnaireCanvas.SetActive(true);
                    questionnaireController.InitializeQuestionnaire();
                    // enable questionnaire gameobjects
                    // instruct the participant to pick up controllers
                }

            }


            if (isQuestionnaireRunning)
            {
                currentTime += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Q) || isQuestionnaireDone)
                {
                    isQuestionnaireDone = false;
                    Debug.LogWarning("Question is done");
                    questionnaireController.questionnaireCanvas.SetActive(false);
                    isQuestionnaireRunning = false;
                    currentConditionNum += 1;
                    PrepareCondition();
                    // Write data
                    // reset variables of the avatar and questionnaire part
                }


            }
        }
        else
        {
            Debug.LogWarning("The end.");
        }
    }


    private void PrepareCondition()
    {
        // also ajust sync/async avatar options here
        if (latinSquare4x4[participantID % 4, currentConditionNum % 4] == 1)
        {
            vmType = VisuomotorType.Sync;
        }
        else if (latinSquare4x4[participantID % 4, currentConditionNum % 4] == 2)
        {
            vmType = VisuomotorType.Async;
        }
        else if (latinSquare4x4[participantID % 4, currentConditionNum % 4] == 3)
        {
            vmType = VisuomotorType.Async_begin;
        }
        else if (latinSquare4x4[participantID % 4, currentConditionNum % 4] == 4)
        {
            vmType = VisuomotorType.Async_end;
        }
        Debug.LogWarning("The current condition is: " + vmType.ToString());
    }
}
