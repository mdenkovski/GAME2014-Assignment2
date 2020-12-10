using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// GameController.cs
/// Last Edit Dec 12, 2020
/// -updated rotation to account for landsape ref resolution
/// - commented outsome debug lines
///  - added functions to increase score and update lives labels
///  -added audio effect for increase score
///  - gets the stats programatically
/// </summary>

public class GameController : MonoBehaviour
{
    //public TMP_Text SceneLabel;
    public TMP_Text LivesLabel;
    public TMP_Text ScoreLabel;

    public CanvasScaler scaler;

    private float livesLabelHalfWidth;
    private float livesLabelHalfHeight;
    private float scoreLabelHalfWidth;
    private float scoreLabelHalfHeight;

    private int m_score;
    private GameStats stats;

    public Vector2 scale;


    //AUdio effects
    public AudioSource PointsIncreaseAudio;

    private void Start()
    {
        Vector2 ScreenRes = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
        Vector2 RefRes = scaler.referenceResolution;
        //correct orientation of ref res based on orientation of screen
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            float tempX = RefRes.x;
            RefRes.x = RefRes.y;
            RefRes.y = tempX;

        }
        scale = ScreenRes / RefRes;

        livesLabelHalfWidth = scale.x * LivesLabel.rectTransform.rect.width * 0.5f;
        livesLabelHalfHeight = scale.y * LivesLabel.rectTransform.rect.height * 0.5f;
        scoreLabelHalfWidth = scale.x * ScoreLabel.rectTransform.rect.width * 0.5f;
        scoreLabelHalfHeight = scale.y * ScoreLabel.rectTransform.rect.height * 0.5f;

        //get the score from the game stats
        stats = FindObjectOfType<GameStats>();
        m_score = stats.Score;
        //set our score label
        ScoreLabel.text = "Score: " + m_score.ToString();

        //debug the position adjustment values
        //Rect safeArea = Screen.safeArea;
        //Debug.Log("X: " + safeArea.width);
        //Debug.Log("Y: " + safeArea.height);
        //Debug.Log("width: " + Screen.width);
        //Debug.Log("height: " + Screen.height);

        //Debug.Log("safe area left: " + Screen.safeArea.xMin);
        //Debug.Log("safe area right: " + Screen.safeArea.xMax);

        //Debug.Log("half width: " + livesLabelHalfWidth);
        //Debug.Log("half height: " + livesLabelHalfHeight);
        //Debug.Log("Scaler ref:  " + scaler.referenceResolution);
        //Debug.Log("Scaler multiplier:  " + scale);
        
    }


    // Update is called once per frame
    void Update()
    {
        LivesLabel.rectTransform.position = new Vector2(Screen.safeArea.xMin + livesLabelHalfWidth, Screen.safeArea.yMax - livesLabelHalfHeight);
        ScoreLabel.rectTransform.position = new Vector2(Screen.safeArea.xMax - scoreLabelHalfWidth, Screen.safeArea.yMax - scoreLabelHalfHeight);
    }

    public void UpdateLives(int lives)
    {
        LivesLabel.text = "Lives: " + lives.ToString();
    }
    /// <summary>
    /// Increase score by x amount (int)
    /// </summary>
    /// <param name="score"></param>
    public void IncreaseScore(int score)
    {
        m_score += score;
        ScoreLabel.text = "Score: " + m_score.ToString();
        PointsIncreaseAudio.Play();
    }

    /// <summary>
    /// when we transition to a new scene and destory the controller we pass on our score to the game stats that is preserved
    /// </summary>
    private void OnDestroy()
    {
        stats.Score = m_score;
    }


    /// <summary>
    /// set the status of if the game was won or not
    /// </summary>
    /// <param name="won"></param>
    public void SetGameWonStatus(bool won)
    {
        stats.GameWon = won;
    }
}
