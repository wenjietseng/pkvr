using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class QuestionnaireController : MonoBehaviour
{
    public int participantID;
    public TMP_Text mainText;
    public TMP_Text smallInstruction;
    public TMP_Text largeInstruction;
    public GameObject scale;
    public List<GameObject> scales;
    public List<int> responses;
    private List<QuestionnaireData> items = new List<QuestionnaireData>();
    // SPES: All items were designed to be answered on a 5-point Likert scale ranging from 1 (= I do not agree at all) to 5 (= I fully agree).
    private QuestionnaireData sl01;
    private QuestionnaireData sl02;
    private QuestionnaireData sl03;
    private QuestionnaireData sl04;
    private QuestionnaireData pa01;
    private QuestionnaireData pa02;
    private QuestionnaireData pa05;
    private QuestionnaireData pa08;
    private QuestionnaireData r01;
    private QuestionnaireData r02;
    private QuestionnaireData r03;
    private QuestionnaireData r04;
    private QuestionnaireData r05;
    private QuestionnaireData r06;
    private QuestionnaireData r07;
    private QuestionnaireData r08;
    private QuestionnaireData r09;
    private QuestionnaireData r10;
    private QuestionnaireData r11;
    private QuestionnaireData r12;
    private QuestionnaireData r13;
    /// <summary>
    ///  not using them beacause our experiment doesn't involve touch/tactile stimuli.
    /// </summary>
    //private QuestionnaireData r14;
    //private QuestionnaireData r15;
    //private QuestionnaireData r16;

    private GameObject currentScaleGO;
    private bool isStart;
    private bool isAllowedCheck;
    private bool isEnd;
    private int currentScale;
    public int currentItem;
    private StreamWriter questionnaireWriter;
    // public FadeEffect fadeEffect;


    void Start()
    {
        // fadeEffect = this.GetComponent<FadeEffect>();
        // fadeEffect.fadeInEffect();

        string questionnairePath = Helpers.CreateDataPath(participantID, "_questionnaire");
        questionnaireWriter = new StreamWriter(questionnairePath, true);

        isStart = false;
        scale.SetActive(false);
        sl01 = new QuestionnaireData("P" + participantID.ToString(), "I felt like I was actually there in the environment of the presentation.");
        sl02 = new QuestionnaireData("P" + participantID.ToString(), "It seemed as though I actually took part in the action of the presentation.");
        sl03 = new QuestionnaireData("P" + participantID.ToString(), "It was as though my true location had shifted into the environment in the presentation.");
        sl04 = new QuestionnaireData("P" + participantID.ToString(), "I felt as though I was physically present in the environment of the presentation.");
        pa01 = new QuestionnaireData("P" + participantID.ToString(), "The objects in the presentation gave me the feeling that I could do things with them.");
        pa02 = new QuestionnaireData("P" + participantID.ToString(), "I had the impression that I could be active in the environment of the presentation.");
        pa05 = new QuestionnaireData("P" + participantID.ToString(), "I felt like I could move around among the objects in the presentation.");
        pa08 = new QuestionnaireData("P" + participantID.ToString(), "It seemed to me that I could do whatever I wanted in the environment of the presentation.");

        r01 = new QuestionnaireData("P" + participantID.ToString(), "I felt out of my body");
        r02 = new QuestionnaireData("P" + participantID.ToString(), "I felt as if my (real) body were drifting toward the virtual body or as if the virtual body were drifting toward my (real) body.");
        r03 = new QuestionnaireData("P" + participantID.ToString(), "I felt as if the movements of the virtual body were influencing my own movements.");
        r04 = new QuestionnaireData("P" + participantID.ToString(), "It felt as if my (real) body were turning into an 'avatar' body.");
        r05 = new QuestionnaireData("P" + participantID.ToString(), "At some point it felt as if my real body was starting to take on the posture or shape of the virtual body that I saw.");
        r06 = new QuestionnaireData("P" + participantID.ToString(), "I felt like I was wearing different clothes from when I came to the laboratory.");
        /// <summary>
        /// R7 allows for experiment specifics to customize the question based on the independent variable of the study.
        /// i.e. if a specific body swaporif athreat isinvolved, such as, ?I felt as if my body was older? or ?I felt as if my hand was attacked.?
        /// </summary>
        r07 = new QuestionnaireData("P" + participantID.ToString(), "I felt as if my body had changed.");

        /// <summary>
        /// R8 and R9 can be adapted to non-threat situations such as ?I felt a realistic sensation in my body when I saw my hand?
        /// or ?I felt that my own body could have been affected by the virtual world.?
        /// </summary>
        r08 = new QuestionnaireData("P" + participantID.ToString(), "I felt a realistic sensation in my body when I saw my body.");
        r09 = new QuestionnaireData("P" + participantID.ToString(), "I felt that my own body could be affected by the virtual world.");

        r10 = new QuestionnaireData("P" + participantID.ToString(), "I felt as if the virtual body was my body.");
        r11 = new QuestionnaireData("P" + participantID.ToString(), "At some point it felt that the virtual body resembled my own (real) body in terms of shape skin tone or other visual features.");
        r12 = new QuestionnaireData("P" + participantID.ToString(), "I felt as if my body was located where I saw the virtual body.");
        r13 = new QuestionnaireData("P" + participantID.ToString(), "I felt like I could control the virtual body as if it was my own body.");

        /// <summary>
        /// not using them beacause our experiment doesn't involve touch/tactile stimuli.
        /// </summary>
        //r14 = new QuestionnaireData("P" + participantID.ToString(), "Q14.\tIt seemed as if I felt the touch of the floor in the location where I saw the virtual feet touched.");
        //r15 = new QuestionnaireData("P" + participantID.ToString(), "Q15.\tIt seemed as if the touch I felt was caused by the floor touching the virtual feet.");
        //r16 = new QuestionnaireData("P" + participantID.ToString(), "Q16.\t It seemed as if my feet was touching the virtual floor.");

        items.Add(r01);
        items.Add(r02);
        items.Add(r03);
        items.Add(r04);
        items.Add(r05);
        items.Add(r06);
        items.Add(r07);
        items.Add(r08);
        items.Add(r09);
        items.Add(r10);
        items.Add(r11);
        items.Add(r12);
        items.Add(r13);

        responses = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        Helpers.Shuffle(items);
        currentItem = 0;
        currentScale = 4;
        currentScaleGO = scales[currentScale - 1];
        smallInstruction.text = "";
        largeInstruction.text = "Press A to Start.";
        isAllowedCheck = false;

        foreach (var s in scales) s.SetActive(false);
    }

    void Update()
    {
        if (!isStart)
        {
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
            //if (Input.GetKeyDown(KeyCode.A))
            {
                if (!isEnd)
                {
                    isStart = true;
                    mainText.text = items[currentItem].item;
                    scale.SetActive(true);
                    currentScaleGO.SetActive(true);
                    smallInstruction.text = (currentItem + 1).ToString("F0") + "/13";
                    largeInstruction.text = "Use Left/Right to select a response and press A to confrim.";
                }
            }
        }
        else
        {
            // collecting questionnaire data
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentScale > 1)
                {
                    currentScaleGO.SetActive(false);
                    currentScale -= 1;
                    scales[currentScale - 1].SetActive(true);
                    currentScaleGO = scales[currentScale - 1];
                }
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
            //else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentScale < 7)
                {
                    currentScaleGO.SetActive(false);
                    currentScale += 1;
                    scales[currentScale - 1].SetActive(true);
                    currentScaleGO = scales[currentScale - 1];
                }
            }
            else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
            //else if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentItem < 13)
                {
                    responses[currentItem] = currentScale;
                    currentScaleGO.SetActive(false);
                    Debug.LogWarning(currentItem + ", " +
                                        items[currentItem].item + ", " +
                                        responses[currentItem].ToString("F0"));

                    if (!isAllowedCheck)
                    {
                        currentItem += 1;
                        if (currentItem < 12)
                        {
                            currentScale = 4;
                            currentScaleGO = scales[currentScale - 1];
                        }
                    }
                    else
                    {
                        foreach (var s in scales) s.SetActive(false);
                        scales[responses[currentItem] - 1].SetActive(true);
                        currentScale = responses[currentItem];
                        currentScaleGO = scales[responses[currentItem] - 1];
                    }
                    currentScaleGO.SetActive(true);

                    if (currentItem < 13)
                    {
                        mainText.text = items[currentItem].item;
                        smallInstruction.text = (currentItem + 1).ToString("F0") + "/13";
                    }
                    else
                    {
                        smallInstruction.text = "Use Left/Right to select a response and press A to confrim.\n" + currentItem.ToString("F0") + "/13";
                        largeInstruction.text = "Use Up/Down to check your responses and Press B to end the test.";
                        isAllowedCheck = true;
                        currentItem = 12;
                    }
                }
            }

            if (isAllowedCheck)
            {
                // once fill out everything
                if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch))
                //if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (currentItem > 0)
                    {
                        currentItem -= 1;
                        smallInstruction.text = "Use Left/Right to select a response and press A to confrim.\n" + (currentItem + 1).ToString("F0") + "/13";
                        mainText.text = items[currentItem].item;
                        foreach (var s in scales) s.SetActive(false);
                        scales[responses[currentItem] - 1].SetActive(true);
                        currentScale = responses[currentItem];
                        currentScaleGO = scales[responses[currentItem] - 1];
                    }
                }
                else if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
                //else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (currentItem < 12)
                    {
                        currentItem += 1;
                        smallInstruction.text = "Use Left/Right to select a response and press A to confrim.\n" + (currentItem + 1).ToString("F0") + "/13";
                        mainText.text = items[currentItem].item;
                        foreach (var s in scales) s.SetActive(false);
                        scales[responses[currentItem] - 1].SetActive(true);
                        currentScale = responses[currentItem];
                        currentScaleGO = scales[responses[currentItem] - 1];
                    }

                }
                else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
                //else if (Input.GetKeyDown(KeyCode.B))
                {
                    StartCoroutine(WriteQuestionnaireData());
                    Debug.LogWarning("End, Write data");
                    isStart = false;
                    isEnd = true;
                    scale.SetActive(false);
                    smallInstruction.text = "";
                    largeInstruction.text = "";
                    mainText.text = "This is the end of the study.\nPlease contact the experimentor, thanks!";
                }
            }
        }
    }

    IEnumerator WriteQuestionnaireData()
    {
        questionnaireWriter.Write("ParticipantID" + "," +
                                  "Item" + "," +
                                  "Response" + "\n");
        for (int i = 0; i < 13; i++)
        {
            questionnaireWriter.Write(items[i].participantID + "," +
                                        items[i].item + "," +
                                        responses[i] + "\n");
        }
        questionnaireWriter.Flush();
        questionnaireWriter.Close();
        yield return 0;
    }

    public struct QuestionnaireData
    {
        public string participantID;
        public string item;

        public QuestionnaireData(string participantID, string item)
        {
            this.participantID = participantID;
            this.item = item;
        }
    }
}


public static class Helpers
{
    public static void Shuffle<T>(this IList<T> list)
    {
        // https://forum.unity.com/threads/randomize-array-in-c.86871/
        // https://stackoverflow.com/questions/273313/randomize-a-listt
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int n = 0; n < list.Count; n++)
        {
            T tmp = list[n];
            int r = UnityEngine.Random.Range(n, list.Count);
            list[n] = list[r];
            list[r] = tmp;
        }
    }

    public static string CreateDataPath(int id, string note = "")
    {
        string fileName = "P" + id.ToString() + note + ".csv";
#if UNITY_EDITOR
        return Application.dataPath + "/Data/" + fileName;
#elif UNITY_ANDROID
        return Application.persistentDataPath + fileName;
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/" + fileName;
#else
        return Application.dataPath + "/" + fileName;
#endif
    }

    public static float RandomGaussian(float minValue = 0f, float maxValue = 1.0f)
    {
        //https://discussions.unity.com/t/normal-distribution-random/66530/4
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    public static float DegreeToRadian(float deg)
    {
        return deg * Mathf.PI / 180;
    }
}