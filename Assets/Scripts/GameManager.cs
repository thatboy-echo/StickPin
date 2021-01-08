using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform spawnPoint;
    private Pin currentPin;
    private bool isGameOver = false;
    private int score = 0;
    private Camera mainCamera;

    public Text scoreText;
    public GameObject pinPrefab;
    public GameObject circleObject;
    public float speed = 3;



	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        SpawnPin();
	}

    private void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currentPin.StartFly();
            SpawnPin();
        }
        if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Q))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

    void SpawnPin()
    {
        currentPin = GameObject.Instantiate(pinPrefab, spawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        circleObject.GetComponent<BallRotate>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if( Mathf.Abs( mainCamera.orthographicSize-4 )<0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
