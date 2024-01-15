using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManagerScript : MonoBehaviour
{
    System.Random rnd;
    public GameObject leftFishPrefab;
    public GameObject rightFishPrefab;
    public GameObject jellyfishPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        spawnFish();
        spawnJellyfish();
    }
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void spawnFish()
    {
        double rndFrame = rnd.NextDouble() * 100;
        float rndSize = (float)(rnd.NextDouble() * 1.0 + 0.2);
        float rndY = (float)(rnd.NextDouble() * 10 - 5);
        float rndVel = (float)(rnd.NextDouble() * 1.7 + 0.7);
        if (rndFrame < 0.5)
        {
            GameObject _gameObject = Instantiate(rightFishPrefab, new Vector2(-13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(rndSize, rndSize, rndSize);
        }
        else if (rndFrame > 99.5)
        {
            GameObject _gameObject = Instantiate(leftFishPrefab, new Vector2(13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(rndSize, rndSize, rndSize);
        }
    }

    private void spawnJellyfish()
    {
        double rndFrame = rnd.NextDouble() * 100;
        float rndX = (float)(rnd.NextDouble() * 22 - 11);
        if (rndFrame < 0.2)
        {
            GameObject _gameObject = Instantiate(jellyfishPrefab, new Vector2(rndX, -8), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
    }
}
