using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class PreparationManager : MonoBehaviour
{

    GameObject clickedGameObject;
    private Transform selectedPiece = null;
    private bool isSelected = false;

    public int tileWidth = 2;
    public int tileHeight = 4;
    public GameObject playerObserveTilesParent;
    Transform[,] playerObservePieceArray;

    string folderPath = "Assets/Resources/Sprite/Character";
    string folderPathResources = "Sprite/Character";
    
    List<string> fileList = new List<string>();
    List<bool> isShopPieceSelected = new List<bool>();
    List<GameObject> shopPieceList = new List<GameObject>();

    public Transform inventoryWindow;
    public Transform shopListWindow;


    void Start()
    {
        playerObservePieceArray = new Transform[tileWidth, tileHeight];

        // ショップリストの表示
        string[] fs = Directory.GetFiles(@folderPath, "*");

        foreach (string f in fs)
        {
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(f);
            // metaファイル排除用
            if (asset != null)
            {
                fileList.Add(f);
                isShopPieceSelected.Add(false);
            }
        }
        int shopItemOffsetX = 40;
        int shopItemOffsetY = -40;
        int shopItemStrideX = 50;
        int shopItemStrideY = -50;
        int shopItemMaxWidth = 280;

        int shopItemWidth = 40;
        int shopItemHeight = 40;

        for (int i = 0; i< fileList.Count; i++)
        {
            string filename = Path.GetFileNameWithoutExtension(fileList[i]);
            GameObject gameObject = new GameObject(filename);

            filename = Path.Combine(folderPathResources, filename);
            Sprite sprite = Resources.Load<Sprite>(filename);

            Image img = gameObject.AddComponent<Image>();
            img.sprite = sprite;

            //適切な位置に配置
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            rectTransform.SetParent(shopListWindow);

            int positionX = shopItemOffsetX + (shopItemStrideX * i) % shopItemMaxWidth;
            int positionY = shopItemOffsetY + ((shopItemStrideX * i) / shopItemMaxWidth) * shopItemStrideY;
            rectTransform.localPosition = new Vector3(positionX, positionY, 0);
            rectTransform.sizeDelta = new Vector2(shopItemWidth, shopItemHeight);
            int tmpIndex = i; //lambda式の引数用、値渡し
            gameObject.AddComponent<Button>().onClick.AddListener(() => { isShopPieceClicked(tmpIndex); });

            shopPieceList.Add(gameObject);
        }

    }

    void Update()
    {

        // Click確認
        if (Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Debug.DrawRay(ray.origin, ray.direction * 1, Color.green, 5, false);
            
            // Collider objectの存在確認（RayHit)
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;

                //非選択モードか確認
                if (!isSelected)
                {
                    //  PieceAbstract objectか判定
                    selectedPiece = clickedGameObject.FindInParents<PieceAbstract>();
                    if (selectedPiece != null)
                    {
                        // 選択状態をTrueに
                        isSelected = true;                        
                    }
                }
                //選択モードの場合
                else
                {
                    //  Tile objecetの判定
                    if (clickedGameObject.gameObject.tag == "Tile")
                    {
                        // 既に駒が配置されていた場合は、その駒とswap
                        if (clickedGameObject.HasChild())
                        {
                            foreach(Transform child in clickedGameObject.transform)
                            {
                                //念のためPieceか確認
                                if (child.HasComponent<PieceAbstract>())
                                {
                                    swapPiece(child, selectedPiece);
                                    //Childは一つのはずだが、念のためBreak
                                    break;
                                }
                            }
                        }
                        //  tileにpieceObjectを配置
                        else
                        {
                            setPiece(selectedPiece, clickedGameObject.transform);
                        }
                        // effect解除
                    }
                    else if(clickedGameObject.FindInParents<PieceAbstract>()){
                        //swap処理
                        swapPiece(selectedPiece, clickedGameObject.FindInParents<PieceAbstract>().transform);
                    }
                    // 選択状態をFalseに
                    isSelected = false;
                    selectedPiece = null;
                }
            }
            // RayCastがhitしなかった場合
            else
            {
                isSelected = false;
                selectedPiece = null;
            }
        }
    }

    public void isShopPieceClicked(int index)
    {
        Debug.Log(index);
        int selectedIndex = isShopPieceSelected.IndexOf(true);
        //既にいずれかのPieceが選択済みの場合
        if (selectedIndex >= 0)
        {
            if (index != selectedIndex)
            {
                isShopPieceSelected[selectedIndex] = false;
                isShopPieceSelected[index] = true;
                outlineShopPiece(selectedIndex, false);
                outlineShopPiece(index, true);
            }
            // 既に選択済みのPieceを選択した場合
            else
            {
                isShopPieceSelected[index] = false;
                outlineShopPiece(index, false);
            }
        }
        else
        {
            isShopPieceSelected[index] = true;
            outlineShopPiece(index, true);
        }
    }

    private void outlineShopPiece(int index, bool enable)
    {
        if (enable)
        {
            Outline outline = shopPieceList[index].AddComponent<Outline>();
            outline.effectColor = new Color(1, 0, 0);
            outline.effectDistance = new Vector2(2, 2);
        }
        else
        {
            Destroy(shopPieceList[index].GetComponent<Outline>());
        }
    }

    public void BuyPiece()
    {
        ////選択チェック

        ////Goldチェック

        ////空きチェック

        ////駒配置
        //GameObject obj = (GameObject)Resources.Load(resourcePath);

        //Transform piece = Instantiate(obj, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity).transform;

        ////Pieceの親オブジェクトにTileを設定
        //piece.parent = tile.transform;
        ////TileObjectを親としたローカルポジションを設定
        //piece.localPosition = new Vector3(0, 0, 0);
        //piece.localRotation = Quaternion.Euler(0, 0, 0);

    }

    private void setPieceToObserveTile(PieceAbstract piece)
    {
        //foreach (Transform tileTransform in playerObserveTilesParent.transform)
        //{
        //    if (!tileTransform.HasChild())
        //    {
        //        setPiece()
        //        break;
        //    }
        //}

    }

    private void setPiece(Transform piece, Transform tile)
    {
        //Pieceの親オブジェクトにTileを設定
        piece.parent = tile.transform;
        //TileObjectを親としたローカルポジションを設定
        piece.localPosition = new Vector3(0, 0, 0);
    }

    private void swapPiece(Transform piece1, Transform piece2) 
    {
        //各Pieceの親となるTileオブジェクトを見つける
        Transform tile1 = piece1.parent;
        Transform tile2 = piece2.parent;

        //親となるTileをそれぞれ入れかえ
        piece1.parent = tile2;
        piece2.parent = tile1;

        //駒を新しい親Tileの場所に移動（Swap）
        piece2.localPosition = new Vector3(0, 0, 0);
        piece1.localPosition = new Vector3(0, 0, 0);
    }

};