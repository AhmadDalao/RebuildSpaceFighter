using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
{
    [SerializeField] private Button _playButtonFromMenu;
    [SerializeField] private Button _quitButtonFromMenu;
    // Start is called before the first frame update
    void Start()
    {
        // attach listener to play button from main menu scene.
        _playButtonFromMenu.GetComponent<Button>().onClick.AddListener(gameManager.playGameButton);

        // attach listener to quit game button from main menu scene.
        _quitButtonFromMenu.GetComponent<Button>().onClick.AddListener(gameManager.quitGameButton);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
