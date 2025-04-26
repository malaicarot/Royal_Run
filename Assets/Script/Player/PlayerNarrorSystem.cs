using UnityEngine;

public class PlayerNarrorSystem : MonoBehaviour, IObserver
{
    [SerializeField] Subject playerSubject;


    const string hitString = "Hit";
    [SerializeField] Animator animator;
    [SerializeField] float speedDown = -2;
    LevelGenerator levelGenerator;
    float countDown = 1f;
    float timer = 0;



    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();

    }


    void Update()
    {
        timer += Time.deltaTime;
    }
    public void OnNotify(PlayerAction action)
    {
        Debug.Log("Player Narror System: NOTIFIED!");
        switch (action)
        {
            case PlayerAction.Blocked:
                if (timer < countDown) return;
                levelGenerator.ChangeMoveSpeed(speedDown);
                animator.SetTrigger(hitString);
                GameManagers.ManagerSingleton.AddScore(-5);
                timer = 0;
                break;
            case PlayerAction.PickedCoin:
                GameManagers.ManagerSingleton.AddScore(10);
                SoundManagers.soundManagersSingleTon.SFXSound(1);
                break;
            case PlayerAction.PickedApple:
                GameManagers.ManagerSingleton.AddScore(5);
                SoundManagers.soundManagersSingleTon.SFXSound(0);
                break;
            default:
                break;
        }
    }

    void OnEnable()
    {
        playerSubject.AddObserver(this);
    }

    void OnDisable()
    {
        playerSubject.RemoveObserver(this);
    }

}
