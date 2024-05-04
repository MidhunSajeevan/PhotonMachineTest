using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI player1Text;
    [SerializeField] TextMeshProUGUI player2Text;
    [SerializeField] Animator SplittingAnimator;
    [SerializeField] Animator PlayerWonAnimator;
    PhotonView photonView;

    [SerializeField] GameObject Player1coins;
    [SerializeField] GameObject Player2coins;
    [SerializeField] TextMeshProUGUI WinnerText;
    [SerializeField] GameObject Coins;
    public UnityAction OnGameOver;
    // Start is called before the first frame update
    void Start()
    {
       photonView = GetComponent<PhotonView>();
      
        if(photonView.IsMine)
        {
            string nickname = PhotonNetwork.LocalPlayer.NickName;
            playerNameText.text = nickname;


        }
        else
        {
            // Get the nickname of a remote player
            string nickname = PhotonNetwork.NickName;
            playerNameText.text = nickname;

            Debug.LogWarning("IsMine False");
        }

        OnGameOver += GameOverEvents;
   

    }

    public void OnStart()
    {
        SplittingAnimator.SetTrigger("Start");
       
        StartCoroutine(OnAnimationComplete());
        StartCoroutine(GameTime());
        
    }
    private IEnumerator OnAnimationComplete()
    {
        yield return new WaitForSeconds(3.5f);
        Player1coins.SetActive(true);
        Player2coins.SetActive(true);
    }

    public IEnumerator GameTime()
    {
        
        yield return new WaitForSeconds(10f);
        Coins.SetActive(true);
        OnGameOver.Invoke();
    }

    public void GameOverEvents()
    {
        WinnerText.gameObject.SetActive(true);
        PlayerWonAnimator.SetTrigger("OnPlayer1Won");
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
