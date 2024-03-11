using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    
    public int yukseklik = 24, genislik = 10;
    private Transform[,] _grids;
    public int _completedLines = 0;

    private void Awake()
    {
        _grids = new Transform[genislik, yukseklik];
    }

    private void Start()
    { 
        BosKareleriOlusturFNC();
    }
    bool WithinTheBoardFNC(int x, int y)
    {
        return (x >= 0 && x < genislik && y >= 0);
    }
    bool IsSquareFull(int x, int y, ShapeManager shape)
    {
        return (_grids[x,y] != null && _grids[x,y].parent != shape.transform);
    }
    public bool InValidPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorToIntFNC(child.position);
            if (!WithinTheBoardFNC((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (pos.y < yukseklik)
            {
                if (IsSquareFull((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void BringTheShapeInToTheGridFNC(ShapeManager shape)
    {
        if (shape == null)
            return;

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorToIntFNC(child.position);
            _grids[(int)pos.x, (int)pos.y] = child;
        }
    }
    Vector2 VectorToIntFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
    void BosKareleriOlusturFNC()
    {
        if (tilePrefab != null)
        {
            for (int x = 0; x < genislik; x++)
            {
                for (int y = 0; y < yukseklik; y++)
                {
                    Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    tile.name = "Kare (" + x.ToString() + "," + y.ToString() + ")";
                    tile.parent = this.transform;
                }
            }
        }
        else
        {
            print("TilePrefab eksik!");
        }
        
    }
    bool IsLineCompletedFNC(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (_grids[x, y] == null)
            {
                return false;
            }
        }
        //SoundManager.instance.PlaySoundEffectFNC(4);
        return true;
    }
    void ClearTheLineFNC(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (_grids[x, y] != null)
            {
                Destroy(_grids[x, y].gameObject);
                if ( _grids[x,y].gameObject.transform.parent.childCount<=0)
                {
                    Destroy(_grids[x, y].gameObject.transform.parent.gameObject);
                }
                _grids[x, y] = null;
            }
            _grids[x, y] = null;
        }
    }
    void MoveOneLineDownFNC(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (_grids[x,y] != null)
            {
                _grids[x, y - 1] = _grids[x, y];
                _grids[x, y] = null;
                _grids[x, y - 1].position += Vector3.down;
            }
        }
    }
    void MoveAllLinesDownFNC(int y)
    {
        for (int i = y; i < yukseklik; ++i)
        {
            MoveOneLineDownFNC(i);
        }
    }
    public void ClearAllLinesFNC()
    {
        _completedLines = 0;
        for (int y = 0; y < yukseklik; ++y)
        {
            if (IsLineCompletedFNC(y))
            {
                _completedLines++;
                ClearTheLineFNC(y);
                MoveAllLinesDownFNC(y + 1);
                y--;
            }
        }
    }

    public bool IsOutOfBounds(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= yukseklik -1)
            {
                return true;
            }
        }

        return false;
    }
    
}
