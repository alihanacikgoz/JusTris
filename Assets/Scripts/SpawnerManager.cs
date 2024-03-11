using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SpawnerManager : MonoBehaviour
{
    [SerializeField] ShapeManager[] allShapes;
    [SerializeField] private Image _image;
    private ShapeManager[] _shapeOnQueue = new ShapeManager[2];

    private void Awake()
    {
        MakeNullFNC();
    }

    public ShapeManager CreateAShape()
    {
        ShapeManager shape = null;
        shape = GetTheNextShapeFNC();
        shape.gameObject.SetActive(true);
        shape.transform.position = transform.position;
        if (allShapes != null)
        {
            return shape;
        }
        else
        {
            print("Şekiller dizisi boş!");
            return null;
        }
    }

    ShapeManager GetTheNextShapeFNC()
    {
        ShapeManager nextShape = null;
        if (_shapeOnQueue[0])
        {
            nextShape = _shapeOnQueue[0];
        }

        for (int i = 1; i < _shapeOnQueue.Length; i++)
        {
            _shapeOnQueue[i - 1] = _shapeOnQueue[i];
            _image.sprite = _shapeOnQueue[i-1].shapeSprite;
        }

        StartCoroutine(ImageShowInFadeRoutine());
        _shapeOnQueue[_shapeOnQueue.Length - 1] = null;
        FillTheQueueFNC();
        return nextShape;
    }

    void MakeNullFNC()
    {
        for (int i = 0; i < _shapeOnQueue.Length; i++)
        {
            _shapeOnQueue[i] = null;
        }
        FillTheQueueFNC();
    }

    IEnumerator ImageShowInFadeRoutine()
    {
        _image.GetComponent<CanvasGroup>().alpha = 0f;
        _image.GetComponent<RectTransform>().localScale = Vector3.zero;

        yield return new WaitForSeconds(.1f);
        
        _image.GetComponent<CanvasGroup>().DOFade(1f, .5f);
        _image.GetComponent<RectTransform>().DOScale(Vector3.one, .5f);
    }

    void FillTheQueueFNC()
    {
        Sprite[] virtualSprites = new Sprite[_shapeOnQueue.Length];
        for (int i = 0; i < _shapeOnQueue.Length; i++)
        {
            if (!_shapeOnQueue[i])
            {
                _shapeOnQueue[i] = Instantiate(CreateRandomShape(), transform.position, Quaternion.identity) as ShapeManager;
                _shapeOnQueue[i].gameObject.SetActive(false);
            }
        }
    }

    public ShapeManager CreateRandomShape()
    {
        int randomShape = Random.Range(0, allShapes.Length);
        if (allShapes[randomShape])
        {
            return allShapes[randomShape];
        }
        else
        {
            return null;
        }
    }
}