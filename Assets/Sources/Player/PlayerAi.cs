﻿using System.Collections;
using System.Collections.Generic;
using TicTakToe.Manager;
using TicTakToe.Ui;
using TicTakToe.Utils;
using UnityEngine;

namespace TicTakToe.Player
{
    public class PlayerAi : GamePlayer 
    {
        public PlayerSymbol aiSymbol = PlayerSymbol.O;

        [SerializeField]
        private float turnDelay = 0.5f;

        [SerializeField]
        private int rndCornerIterations = 5;

        private List<Vector2> prevTurns;
        
        private List<Vector2> enemyTurns;

        private void Awake()
        {
            ResetAI ();
            
            turnDelay = Mathf.Max(turnDelay, 0.1f);
            enemyTurns = new List<Vector2>();
            prevTurns = new List<Vector2>();
        }

        
        public void ActivateForTurn ()
        {
            GridCell lastClickedCell = GameManager.Instance.GameController.GetLastClickedCell();
            if (lastClickedCell != null)
            {
                enemyTurns.Add(lastClickedCell.Cell2DIndex);
            }
            StartCoroutine(TurnDelayCoroutine());
        }

        private IEnumerator TurnDelayCoroutine()
        {
            yield return new WaitForSeconds(turnDelay);
            PerformTurn();
        }

        
        private void PerformTurn()
        {
            GridCell[,] cells = GameManager.Instance.GameController.GetCellMatrix();
            GridCell center = cells[1, 1];
            GridCell[] corners = { cells[0, 0], cells[0, 2], cells[2, 0], cells[2, 2] };
            GridCell targetCell;

            if (enemyTurns.Count < 2)
            {
                if (center.CellAssignedSymbol == CellSymbol.EMPTY)
                {
                    targetCell = center;
                }
                else
                {
                    targetCell = RandomCornerTurn(corners, 1000);
                }
            }
            else
            {
                targetCell = SmartTurn(cells, prevTurns);

                if(targetCell == null)
                {
                    targetCell = SmartTurn(cells, enemyTurns);
                }

                if (targetCell == null)
                {
                    targetCell = RandomCornerTurn(corners, rndCornerIterations);

                    if (targetCell == null)
                    {
                        targetCell = RandomTurn(true);
                    }
                }
            }

            targetCell.setCell ();
            prevTurns.Add(targetCell.Cell2DIndex);
        }

    
        private GridCell RandomCornerTurn(GridCell[] corners, int iterations = 1)
        {
            for(int i = 0; i < iterations; i++)
            {
                GridCell cornerCell = corners[Random.Range(0, corners.Length)];

                if (cornerCell.CellAssignedSymbol == CellSymbol.EMPTY)
                {
                    return cornerCell;
                }
            }
            return null;
        }

        
        private GridCell SmartTurn(GridCell[,] cells, List<Vector2> turns)
        {
            for (int i = 0; i < turns.Count; i++)
            {
                Vector2 outer = turns[i];

                for (int j = i + 1; j < turns.Count; j++)
                {
                    Vector2 inner = turns[j];
                    Vector2 dir = inner - outer;

                    inner += dir;
                    inner.x %= 3;
                    inner.y %= 3;

                    if (inner.x < 0)
                    {
                        inner.x += 3;
                    }
                    if (inner.y < 0)
                    {
                        inner.y += 3;
                    }
                    if (cells[(int)inner.x, (int)inner.y].CellAssignedSymbol == CellSymbol.EMPTY)
                    {
                        return cells[(int)inner.x, (int)inner.y];
                    }
                }
            }

            return null;
        }

        private GridCell RandomTurn(bool performUntilSuccess)
        {
            GridCell[] cellList = GameManager.Instance.GameController.GetCellList();
            do
            {
                int index = Random.Range(0, cellList.Length);

                if(cellList[index].CellAssignedSymbol == CellSymbol.EMPTY)
                {
                    return cellList[index];
                }

            } 
            while (performUntilSuccess);

            return null;
        }

        public void ResetAI()
        {
            prevTurns = new List<Vector2>();
            enemyTurns = new List<Vector2>();
        }
    }
}