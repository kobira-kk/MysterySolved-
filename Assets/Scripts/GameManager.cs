using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // 定数定義 壁方向 
    public const int WALL_FRONT = 1; // 前
    public const int WALL_RIGHT = 2; // 右
    public const int WALL_BACK  = 3; // 後
    public const int WALL_LEFT  = 4; // 左

    // ENUM練習 ボタンカラー
    enum ButtonColor {
        green = 0,
        red = 1,
        blue = 2,
        white = 3
    }

    public GameObject panelWalls; // 壁全体

    // ボタン: トンカチ
    public GameObject buttonHammer;
    // アイコン: トンカチ
    public GameObject buttonHammerIcon;

    // ボタン: 鍵
    public GameObject buttonKey;
    // アイコン: 鍵
    public GameObject buttonKeyIcon;

    // ボタン豚の貯金箱
    public GameObject buttonPig;

    // ボタン 金庫
    public GameObject[] buttonLamp = new GameObject[3];
    // ボタンの絵
    public Sprite[] buttonPicture = new Sprite[4];
   
    // トンカチの絵
    public Sprite hammerPicture;
    // 鍵の絵
    public Sprite keyPicture;

    // テキストUI
    public GameObject buttonMessage;
    public GameObject buttonMessageText;

    int wallNo;

    // ゲームの進行状態
    private bool doesHaveHammer;
    private bool doesHaveKey;
    private readonly ButtonColor[] buttonColor = new ButtonColor[3];

    // Start is called before the first frame update
    void Start(){
        // 初期
        wallNo = WALL_RIGHT;
        doesHaveHammer = false;
        doesHaveKey = false;
        buttonColor[0] = ButtonColor.green;
        buttonColor[1] = ButtonColor.blue;
        buttonColor[2] = ButtonColor.red;

    }

    // Update is called once per frame
    void Update(){
        
    }

    // 右ボタンを押した
    public void PushButtonRight() {
        wallNo++;　// 方向を右方向へ

        // 「左」の1つ右は「前」
        if(wallNo > WALL_LEFT) {
            wallNo = WALL_FRONT;
        }
        DisplayWall();
        ClearButtons();
    }

    // 左ボタンを押した
    public void PushButtonLeft() {
        wallNo--;　// 方向を左方向へ

        // 「前」の1つ左は「左」
        if(wallNo < WALL_FRONT) {
            wallNo = WALL_LEFT;
        }
        DisplayWall();
        ClearButtons();
    }

    // 画面移動の制御
    void DisplayWall() {
        Debug.Log(wallNo);
        switch(wallNo) {
            case WALL_FRONT: // 前
                panelWalls.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case WALL_RIGHT: // 右
                panelWalls.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
                break;
            case WALL_BACK: // 後
                panelWalls.transform.localPosition = new Vector3(-2000.0f, 0.0f, 0.0f);
                break;
            case WALL_LEFT: // 左
                panelWalls.transform.localPosition = new Vector3(-3000.0f, 0.0f, 0.0f);
                break;
        }
    }

    // メッセージを表示
    void DisplayMessage(string mes) {
        buttonMessage.SetActive(true);
        buttonMessageText.GetComponent<Text>().text = mes;
    }

    // メモをタップ
    public void PushButtonMemo() {
        DisplayMessage("エッフェル塔と書いてある。");
    }

    // メッセージをタップ
    public void PushButtonMessage() {
        buttonMessage.SetActive(false); // メッセージを消す
    }

    // 金庫のボタン1をタップ
    public void PushButtonLamp1() {
        ChangeButtonColor(0);
    }
    // 金庫のボタン2をタップ
    public void PushButtonLamp2() {
        ChangeButtonColor(1);
    }
    // 金庫のボタン3をタップ
    public void PushButtonLamp3() {
        ChangeButtonColor(2);
    }

    void ChangeButtonColor(int buttonNo) {
        buttonColor[buttonNo]++;
        if(buttonColor[buttonNo] > ButtonColor.white) {
            buttonColor[buttonNo] = ButtonColor.green;
        }
        // ボタンの画像を変更
        buttonLamp[buttonNo].GetComponent<Image>().sprite = buttonPicture[(int)buttonColor[buttonNo]];

        // ボタンの色チェック
        if(buttonColor[0] == ButtonColor.blue 
            && buttonColor[1] == ButtonColor.white
            && buttonColor[2] == ButtonColor.red) {
            // まだトンカチを手に入れていない
            if(!doesHaveHammer) {
                DisplayMessage("金庫の中にトンカチが入っていた。");
                buttonHammer.SetActive(true);
                buttonHammerIcon.GetComponent<Image>().sprite = hammerPicture;
                doesHaveHammer = true;
            }
        }
    }

    // ハンマー取得ボタンの削除
    public void PushButtonHammer() {
        buttonHammer.SetActive(false);
    }

    // 豚の貯金箱タップ
    public void PushButtonPig() {
        // トンカチの有無
        if(!doesHaveHammer) {
            DisplayMessage("素手では割れない！");
        } else {
            DisplayMessage("貯金箱が割れて中から鍵が出てきた！");
            buttonPig.SetActive(false); // 貯金箱非表示
            buttonKey.SetActive(true); // ゲット表示
            buttonKeyIcon.GetComponent<Image>().sprite = keyPicture;
            doesHaveKey = true;
        }
    }

    // 鍵ゲットのタップ
    public void PushButtonkey() {
        buttonKey.SetActive(false);
    }

    // ボックスをタップ
    public void PushButtonBox() {
        if(!doesHaveKey) {
            DisplayMessage("鍵がどこかにあるはずだ。");
        } else {
            SceneManager.LoadScene("ClearScene");
        }
    }

    // 各種表示をクリア
    void ClearButtons() {
        buttonHammer.SetActive(false);
        buttonKey.SetActive(false);
        buttonMessage.SetActive(false);
    }

}
