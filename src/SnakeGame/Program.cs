using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace SnakeGame
{
    public enum CellType
    {
        EMPTY = 0,
        FOOD = 1,
        SNAKEBODY = 2,
    }

    public class Cell
    {
        private int row;
        private int col;
        private CellType cellType;

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public CellType getCellType()
        {
            return cellType;
        }

        public void setCellType(CellType type)
        {
            this.cellType = type;
        }

        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }
    }

    public class Board
    {
        readonly int ROW_COUNT;
        readonly int COLUMN_COUNT;
        private Cell[,] Cells;

        public Board(int rows, int cols)
        {
            this.ROW_COUNT = rows;
            this.COLUMN_COUNT = cols;
            this.Cells = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    this.Cells[i, j] = new Cell(i, j);
                    this.Cells[i, j].setCellType(CellType.EMPTY);
                }
            }
        }

        public int getRow()
        {
            return ROW_COUNT;
        }

        public int getCol()
        {
            return COLUMN_COUNT;
        }

        public Cell[,] getCells()
        {
            return Cells;
        }

        public void generateFood()
        {
            Random rand = new Random();
            int row = rand.Next() % ROW_COUNT;
            int col = rand.Next() % COLUMN_COUNT;
            Cells[row, col].setCellType(CellType.FOOD);
        }
    }

    public class Snake
    {
        private List<Cell> snakePartList;
        private Cell head;

        public Snake(Cell head)
        {
            this.head = head;
            this.snakePartList = new List<Cell>();
            snakePartList.Add(head);
        }

        public Cell getHead()
        {
            return head;
        }

        public void grow()
        {
            snakePartList.Add(head);
        }

        public void move(int direction)
        {
            int row = head.getRow();
            int col = head.getCol();
        }

        public bool checkForCrash(Cell nextCell)
        {
            if (nextCell.getCellType() == CellType.SNAKEBODY)
            {
                return true;
            }
            return false;
        }

        public void move(Cell nextCell)
        {
            head = nextCell;
            Cell tail = snakePartList[0];
            tail.setCellType(CellType.EMPTY);
            snakePartList.Add(nextCell);
            //nextCell.setCellType(CellType.SNAKEBODY);
            snakePartList.RemoveAt(0);
        }
    }

    public class Game
    {
        private Board board;
        private bool gameOver;
        private int direction;
        private Snake snake;
        public static readonly int DIRECTION_NONE = 0, DIRECTION_LEFT = -1, DIRECTION_RIGHT = 1, DIRECTION_UP = -2, DIRECTION_DOWN = 2;

        public Game(Board board, Snake snake)
        {
            this.board = board;
            this.snake = snake;
        }

        public void update()
        {
            Console.WriteLine("Going to update the game");
            if (!gameOver)
            {
                if (direction != DIRECTION_NONE)
                {
                    Cell nextCell = GetNextCell(snake.getHead());

                    if (nextCell == null || snake.checkForCrash(nextCell))
                    {
                        gameOver = true;
                    }
                    else
                    {
                        snake.move(nextCell);

                        if (nextCell.getCellType() == CellType.FOOD)
                        {
                            snake.grow();
                            board.generateFood();
                        }
                        nextCell.setCellType(CellType.SNAKEBODY);
                    }
                }
            }
        }

        public Cell GetNextCell(Cell snakeHead)
        {
            int row = snakeHead.getRow();
            int col = snakeHead.getCol();
            int rowCount = board.getRow();
            int colCount = board.getRow();

            if (direction == DIRECTION_LEFT)
            {
                col--;
            }
            else if (direction == DIRECTION_RIGHT)
            {
                col++;
            }
            else if (direction == DIRECTION_UP)
            {
                row--;
            }
            else if (direction == DIRECTION_DOWN)
            {
                row++;
            }

            Cell nextCell = null;

            if (row >= 0 && row < rowCount && col >= 0 && col < colCount)
            {
                nextCell = board.getCells()[row, col];
            }

            return nextCell;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the Game");
            Board board = new Board(8, 8);
            Cell initPos = new Cell(0, 0);
            Snake initsnake = new Snake(initPos);

            Game newGame = new Game(board, initsnake);
            newGame.direction = DIRECTION_RIGHT;
            newGame.gameOver = false;

            for (int i = 0; ; i++)
            {
                if (i % 5 == 0)
                {
                    board.generateFood();
                }
                newGame.update();

                if (newGame.gameOver == true)
                {
                    break;
                }
            }

            Console.WriteLine("Gamee Over");
        }
    }
}
