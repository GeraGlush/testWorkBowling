using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [HideInInspector] public bool finishSceneStart;

    [SerializeField] private List<GameObject> balls;
    public Transform[] ballsPositions;

    public GameObject destroyPS;
    public GameObject ballPrefab;
    public Transform ballPrefabParent;

    public int startBallCount = 1;


    private void Start()
    {
        AddBallCount(startBallCount);
    }

    public void DeledeBallCount(int _ballCount)
    {
        for (int i = 0; i < _ballCount; i++)
        {
            if (balls.Count > 0)
            {
                DeledeBall(balls[balls.Count-1]);
            }
        }
    }

    public void AddBallCount(int _ballCount)
    {
        for (int i = 0; i < _ballCount; i++)
        {
            if (FreePositionForBall() != null)
            {
                CreateBall();
            }
        }
    }

    /// <summary>
    /// Список мячей в куче
    /// </summary>
    /// <returns>Для других скриптов</returns>
    public List<GameObject> TakeBallList()
    {
        return balls;
    }

    /// <summary>
    /// Поиск свободной позиции для спавна мяча в куче.
    /// </summary>
    /// <returns>Если не находит - возвращяет null</returns>
    private Transform FreePositionForBall()
    {
        foreach (Transform position in ballsPositions)
        {
            if (position.gameObject.activeSelf)
            {
                return position;
            }
        }

        return null;
    }
    
    
    private void CreateBall()
    {
        Transform transformToCreate = FreePositionForBall();

        if (transformToCreate != null)
        {
            GameObject _ball = Instantiate(ballPrefab, transformToCreate.position, Quaternion.identity, ballPrefabParent);
            _ball.GetComponent<Ball>().myStartPosition = transformToCreate;
            transformToCreate.gameObject.SetActive(false);
            balls.Add(_ball);
        }
    }

    public void DeledeBall(GameObject _ballToDelede)
    {
        balls.Remove(_ballToDelede);
        Instantiate(destroyPS, _ballToDelede.GetComponent<Ball>().myStartPosition.position, Quaternion.identity);
        _ballToDelede.GetComponent<Ball>().myStartPosition.gameObject.SetActive(true);
        Destroy(_ballToDelede);
        
        if (balls.Count == 0 && !finishSceneStart) // при удалении всех мячей при финише - проигрыша быть не должно
        {
            FindObjectOfType<GameManager>().Fail(); // если мячей равно 0 - игрок проигрывает
        }
    }
}
