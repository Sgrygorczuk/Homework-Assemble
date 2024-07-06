using UnityEngine;

public class EndGoal : MonoBehaviour
{
    //==================================================================================================================
    // Variables  
    //==================================================================================================================
    
    //Used to tell how much the points the end goal gives out 
    [SerializeField] private int score;
    //Used to create the text that shows points scored 
    [SerializeField] private GameObject preFab;
    //Used to tell where the text should spawn 
    private Transform _spawnPoint;
    //Used to tell the shooting script that player can click the button 
    private Shooter _shooter;
    //Used to tell the game flow script how many points the player earned 
    private GameFlow _gameFlow;
    //Used to play a SFX for player reaching the end goal 
    private AudioSource _audioSource;
    
    //==================================================================================================================
    // Base Methods  
    //==================================================================================================================
    
    //Connects all of the components 
    private void Start()
    {
        _shooter = GameObject.Find("Shooter").GetComponent<Shooter>();
        _gameFlow = GameObject.Find("GameFlow").GetComponent<GameFlow>();
        _audioSource = GetComponent<AudioSource>();
        _spawnPoint = transform.GetChild(0).transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        //Passes on the score 
        _gameFlow.ScorePoint(score);
        //Creates the prefab and updates it's value 
        var instantiate = Instantiate(preFab, _spawnPoint.position, Quaternion.identity);
        instantiate.GetComponent<ScoreAdd>().SetValue(score);
        //Resets button 
        _shooter.ResetButton();
        //Plays SFX 
        _audioSource.Play();
        //Destroys the ball 
        Destroy(other.gameObject);
    }
}
