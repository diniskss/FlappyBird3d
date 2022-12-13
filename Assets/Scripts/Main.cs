using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class Main : MonoBehaviour
{
    public float Forse;
    public float Speed;
    public Rigidbody rb;
    public GameObject StepPrefab;
    public GameObject Steps;
    private GameObject[] steps;
    private bool GameStarted;
    private int Score = 0;
    public TMP_Text ScoreText;

    void Start()
    {
        ScoreText.text = Convert.ToString("Рекорд: " + PlayerPrefs.GetInt("Score"));
    }
    void Update()
    {
        if (GameStarted)
        {
            StepAddRemove();
            ScoreText.text = Convert.ToString(Score);
        }
    }
    void FixedUpdate()
    {
        if (GameStarted)
        {
            Steps.transform.position -= Vector3.forward * Speed * Time.deltaTime;

        }
    }
    public void Jump()
    {
        rb.velocity = new Vector3(0, Forse, 0);
        Debug.Log(rb.velocity);
    }
    void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene(0);
    }
    private void StepAddRemove()
    {
        steps = new GameObject[5]{
            GameObject.Find("Step1"),
            GameObject.Find("Step2"),
            GameObject.Find("Step3"),
            GameObject.Find("Step4"),
            GameObject.Find("Step5"),
            };

        for (int i = 0; i < 5; i++)
        {
            if (steps[i].transform.position.z < -142f)
            {
                string name = steps[i].name;
                Destroy(steps[i]);

                steps[i] = Instantiate(StepPrefab,
                new Vector3(8.5f, UnityEngine.Random.Range(-104f, -10f), 237.8f),
                Quaternion.identity, Steps.transform) as GameObject;

                steps[i].name = name;
                Score++;
                PlayerPrefs.SetInt("Score", Score);
            }
        }

    }
    public void StartGame()
    {
        GameStarted = true;
        rb.isKinematic = false;
        Destroy(GameObject.Find("StartButton"));
    }
}