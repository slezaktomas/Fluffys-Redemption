using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class BlackJackMiniGame : MonoBehaviour
{
    public GameObject infoText;
    public TMP_Text info;
    public GameObject scoreText;
    public TMP_Text score;
    public Button hitButton;
    public Button standButton;
    public GameObject player;
    public GameObject hitPlayer;
    public GameObject dealer;
    public GameObject hitDealer;
    public List<GameObject> cardPrefabs;

    public List<GameObject> deck;
    public List<GameObject> playerHand;
    public List<GameObject> dealerHand;

    public GameObject DealerHealth;
    public GameObject PlayerHealth;

    private int playerScore;
    private int dealerScore;
    private bool gameEnded = false;

    private HurtBoss hurtBossScript;
    private HurtPlayer hurtPlayerScript;
    public GameObject loserScrean;
    public GameObject winnerScreen;

    private void Awake()
    {
        info = infoText.GetComponent<TMP_Text>();
        score = scoreText.GetComponent<TMP_Text>();
    }

    void Start()
    {
        hitButton.onClick.AddListener(Hit);
        standButton.onClick.AddListener(Stand);
        
        InitializeDeck();
        ShuffleDeck();
        DealInitialCards();
    }

    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.E))
        {
            Reset();
            gameEnded = false;
        }
    }

    void Stand()
    {
        GameObject dealerSecondCardPrefab = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], dealer.transform);
        dealerHand.Add(dealerSecondCardPrefab);
        deck.RemoveAt(0);
        UpdateScore();
        while (dealerScore < 17)
        {
            GameObject dealerCardPrefab = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], hitDealer.transform);
            dealerHand.Add(dealerCardPrefab);
            deck.RemoveAt(0);
            UpdateScore();
        }
        if (dealerScore > 21 || dealerScore < playerScore)
        {
            info.text = "Boss busts! Fluffy wins. \n Press E to start next round";
            HurtBoss hurtBossScript = DealerHealth.GetComponent<HurtBoss>();
            if (hurtBossScript != null)
            {
                hurtBossScript.Hurt();
            }
        }
        else if (dealerScore > playerScore)
        {
            info.text = "Boss wins. \n Press E to start next round";
            HurtPlayer hurtPlayerScript = PlayerHealth.GetComponent<HurtPlayer>();
            if (hurtPlayerScript != null)
            {
                hurtPlayerScript.Hurt();
            }
        }
        else
        {
            info.text = "It's a draw. \n Press E to start next round";
        }

        gameEnded = true;
        DisableButtons();
    }

    void Reset()
    {
        dealerScore = 0;
        playerScore = 0;
        info.text = "";
        hitButton.interactable = true;
        standButton.interactable = true;
        deck.Clear();
        playerHand.Clear();
        dealerHand.Clear();
        DestroyChildren(player.transform);
        DestroyChildren(dealer.transform);
        DestroyChildren(hitPlayer.transform);
        DestroyChildren(hitDealer.transform);
        InitializeDeck();
        ShuffleDeck();
        DealInitialCards();
    }
    void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    void Hit()
    {
        GameObject playerCardPrefab = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], hitPlayer.transform);
        playerHand.Add(playerCardPrefab);
        deck.RemoveAt(0);
        UpdateScore();
        
        if (playerScore == 21)
        {
            info.text = "Fluffy wins with a perfect score of 21! \n Press E to start next round";
            HurtBoss hurtBossScript = DealerHealth.GetComponent<HurtBoss>();
            if (hurtBossScript != null)
            {
                hurtBossScript.Hurt();
            }
            DisableButtons();
            gameEnded = true;
        }
        if (playerScore > 21)
        {
            info.text = "Fluffy busts! Boss wins. \n Press E to start next round";
            HurtPlayer hurtPlayerScript = PlayerHealth.GetComponent<HurtPlayer>();
            if (hurtPlayerScript != null)
            {
                hurtPlayerScript.Hurt();
            }
            gameEnded = true;
            DisableButtons();
        }
    }

    void InitializeDeck()
    {
        deck.Clear();
        
        string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        
        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                GameObject card = new GameObject(value + " of " + suit);
                deck.Add(card);
            }
        }
    }

    void ShuffleDeck()
    {
        deck = deck.OrderBy(x => Random.value).ToList();
    }

    void DealInitialCards()
    {
        playerHand.Clear();
        dealerHand.Clear();

        for (int i = 0; i < 2; i++)
        {
            GameObject playerCardPrefab = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], player.transform);
            playerHand.Add(playerCardPrefab);
            deck.RemoveAt(0);
        }
        GameObject dealerCardPrefab = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Count)], dealer.transform);
        dealerHand.Add(dealerCardPrefab);
        deck.RemoveAt(0);

        UpdateScore();
    }

    void UpdateScore()
    {
        playerScore = CalculateHandScore(playerHand);
        dealerScore = CalculateHandScore(dealerHand);

        score.text = "Player: " + playerScore + "\nDealer: " + dealerScore;
    }

    int CalculateHandScore(List<GameObject> hand)
    {
        int score = 0;
        bool hasAce = false;

        foreach (var card in hand)
        {
            string cardName = card.name;
            if (cardName.Contains("Ace"))
            {
                hasAce = true;
            }

            int value = card.GetComponent<Card>().value;
            score += value;
        }

        if (hasAce && score + 10 <= 21)
        {
            score += 10;
        }

        return score;
    }

    void DisableButtons()
    {
        hitButton.interactable = false;
        standButton.interactable = false;
    }
}
