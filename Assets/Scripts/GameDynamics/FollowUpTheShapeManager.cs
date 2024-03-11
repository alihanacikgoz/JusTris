using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpTheShapeManager : MonoBehaviour
{
    private ShapeManager _followUpShape;
    private bool _isHitTheBottom = false;
    public Color color = new Color(1f,1f,1f, 0.02f);

    public void CreateFollowUpShapeFNC(ShapeManager shape, BoardManager board)
    {
        if (!_followUpShape)
        {
            _followUpShape = Instantiate(shape, shape.transform.position, shape.transform.rotation) as ShapeManager;
            _followUpShape.name = "FollowUpShape";

            SpriteRenderer[] allSprites = _followUpShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in allSprites)
            {
                sprite.color = color;
            }
                
        }
        else
        {
            _followUpShape.transform.position = shape.transform.position;
            _followUpShape.transform.rotation = shape.transform.rotation;
        }
        _isHitTheBottom = false;
        while (!_isHitTheBottom)
        {
            _followUpShape.MoveDownFNC();
            if (!board.InValidPosition(_followUpShape))
            {
                _followUpShape.MoveUpFNC();
                _isHitTheBottom = true;
            }
        }
            
    }

    public void FollowUpShapeDestroyFNC()
    {
        Destroy(_followUpShape.gameObject);
    }
}
