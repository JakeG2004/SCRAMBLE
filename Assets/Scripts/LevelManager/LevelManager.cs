using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private Vector3 _stats = Vector3.zero;

    void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }

        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ServerCorrect()
    {
        IncrementScore();
        _stats.y++;
    }

    public void ServeIncorrect()
    {
        DecrementScore();
        _stats.z++;
    }

    private void IncrementScore()
    {
        _stats.x++;
    }

    private void DecrementScore()
    {
        _stats.x--;
    }

    public Vector3 GetScore()
    {
        return _score;
    }
}
