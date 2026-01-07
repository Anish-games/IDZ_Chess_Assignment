using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using Chess.Scripts.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    [SerializeField] private GameObject _captureHighlightPrefab;
    private GameObject[,] _chessBoard;

    internal static ChessBoardPlacementHandler Instance;

    private void Awake() {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j) {
        try {
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(int row, int col, ChessPiece piece = null, bool isCapture = false) {
        var tile = GetTile(row, col)?.transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        var highlight = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        
        if (piece != null && highlight != null) {
            var moveHandler = highlight.AddComponent<MoveHandler>();
            if (moveHandler != null) {
                moveHandler.Initialize(piece, row, col, isCapture);
            }
        }
    }

    internal void HighlightCapture(int row, int col, ChessPiece piece = null, bool isCapture = true) {
        var tile = GetTile(row, col)?.transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        var prefab = _captureHighlightPrefab != null ? _captureHighlightPrefab : _highlightPrefab;
        var highlight = Instantiate(prefab, tile.transform.position, Quaternion.identity, tile.transform);
        
        if (_captureHighlightPrefab == null && highlight != null) {
            var spriteRenderer = highlight.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) {
                spriteRenderer.color = new Color(1f, 0.3f, 0.3f, 0.8f);
            }
        }
        
        if (piece != null && highlight != null) {
            var moveHandler = highlight.AddComponent<MoveHandler>();
            if (moveHandler != null) {
                moveHandler.Initialize(piece, row, col, isCapture);
            }
        }
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }
}