using System;
using TicTacToe.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class GridCell : MonoBehaviour
    {
        public GridCellType CellType => cellType;
        public bool IsEmpty => cellType == GridCellType.NONE;

        [SerializeField] private GameObject iconX;
        [SerializeField] private GameObject iconO;
        [SerializeField] private Button cellButton;

        private int indexX;
        private int indexY;
        private GridCellType cellType;
        private Action<GridCell> onCellClicked;

        private const string NAME_FORMAT = "GridCell_{0}_{1}";

        public void Initialize(int x, int y, Action<GridCell> cellClicked = null)
        {
            indexX = x;
            indexY = y;
            gameObject.name = string.Format(NAME_FORMAT, indexX, indexY);
            onCellClicked = cellClicked;
            SetCellType(GridCellType.NONE);
        }

        private void OnEnable()
        {
            cellButton.onClick.AddListener(() => onCellClicked?.Invoke(this));
        }

        private void OnDisable()
        {
            cellButton.onClick.RemoveAllListeners();
        }

        public void SetCellType(GridCellType type)
        {
            cellType = type;
            UpdateCell();
        }

        private void UpdateCell()
        {
            iconX.SetActive(cellType == GridCellType.X);
            iconO.SetActive(cellType == GridCellType.O);
        }
    }
}
