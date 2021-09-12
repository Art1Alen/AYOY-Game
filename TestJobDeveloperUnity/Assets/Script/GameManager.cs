using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }

    [SerializeField] private float _slowMotionFactor = .1f;
    [SerializeField] private Transform _startTranform;
    [SerializeField] private Transform _goalTranform;
    [SerializeField] private BollController _boll;

    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }

    private void Start()
    {
        EntireDistance = _goalTranform.position.z - _startTranform.position.z;
    }

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void StartGame()
    {
        GameStarted = this;
        Debug.Log("Game Started");
    }

    public void EndGame(bool win)
    {
        GameEnded = true;
        Debug.Log("Game Ended");

        if (!win)
        {
            // Рестарт цены 
            Invoke("RestartGame", 2 * _slowMotionFactor);
            Time.timeScale = _slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Invoke("RestartGame", 2);
        }
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void Update()
    {
        DistanceLeft = Vector3.Distance(_boll.transform.position, _goalTranform.position);
       
        if (DistanceLeft > EntireDistance)
            DistanceLeft = EntireDistance;

        if (_boll.transform.position.z > _goalTranform.transform.position.z)
            DistanceLeft = 0;

        Debug.Log("Traveled  distance is" + DistanceLeft + "entire distance is" + EntireDistance);
    }
}
