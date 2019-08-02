using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private float t = 0.0f;

    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;

    public AudioSource musicSource;
    
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text startText;
    public static bool hardMode;
    private int score;
    private bool gameOver;
    private bool restart;

    void Start ()
    {
        hardMode = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        startText.text = "Press 'G' to start normal Press 'H' for hardmode";
        score = 0;
        UpdateScore();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
            {
                Application.Quit();
            }
        if (Input.GetKeyDown (KeyCode.G))
            {
                StartCoroutine (SpawnWaves ());
                startText.text = "";
            }
        if (Input.GetKeyDown (KeyCode.H))
            {
                StartCoroutine (HardMode ());
                startText.text = "";
                hardMode = true;
                  
            }
        if (hardMode == true)
            {
                var main = ps1.main;
                main.simulationSpeed = Mathf.Lerp(1 , 7 , t);
                var secondary = ps2.main;
                secondary.simulationSpeed = Mathf.Lerp(1 , 7 , t);  
                t += 0.5f * Time.deltaTime;
            }
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.L))
            {
                SceneManager.LoadScene("ShooterScene");
            }
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'L' to Restart";
                restart = true;
                break;
            }
        }
    }
    IEnumerator HardMode ()
    {
        yield return new WaitForSeconds (startWait * 3);
        while (true)
        {
            for (int i = 0; i < hazardCount * 1.5; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait / 2);
            }
            yield return new WaitForSeconds (waveWait / 2);

            if (gameOver)
            {
                restartText.text = "Press 'L' to Restart";
                restart = true;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }   

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
         if (score >= 150)
          {
            winText.text = "You win!";
            gameOverText.text = "Game Created by Dylan Cranford";
            gameOver = true;
            restart = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;
           }
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over! Game Created by Dylan Cranford";
        gameOver = true; 
        musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = false;
            musicSource.volume = 1;
    }
}

