using System;

namespace Minesweeper
{
    class Cell
    {
        public bool is_mine;
        public int bombs;
        public bool opened;
        public bool flagged;
        public Cell() { is_mine = false; bombs = 0; opened = false; flagged = false; }
        public Cell(bool mine, int bom)
        {
            is_mine = mine;
            bombs = bom;
            opened = false;
            flagged = false;
        }
        public bool openable()
        {
            if (flagged == true)
                return false;
            else if (opened == false && bombs == 0)
                return true;
            else if(opened == false && bombs != 9)
            {
                Minefield.win_counter++;
                opened = true;
                return false;
            }
            return false;
        }
    }
    class Minefield
    {
        private Cell[,] field;
        int m;
        int n;
        int difficulty;
        public static int win_counter;
        public Minefield(int mm = 10, int nn = 10, int dif = 1)
        {
            field = new Cell[mm, nn];
            m = mm;
            n = nn;
            difficulty = dif;
            win_counter = 0;
        }
        public bool win()
        {
            Console.WriteLine("current: " + win_counter + "\nexpected: " + (n * m - ((n + 1) * difficulty)));
            if (win_counter >= n * m - ((n + 1) * difficulty))
                return true;
            return false;
        }
        public void fieldPrint()
        {
            Console.Write($"{"",-2}");
            for (int i = 0; i < m; i++)
            {
                Console.Write($"{i + 1,3}");
            }
            Console.WriteLine();
            for (int i = 0; i < m; i++)
            {
                Console.Write($"{i + 1,-3} ");
                for (int j = 0; j < n; j++)
                {
                    if (field[i, j].opened == false)
                    {
                        if (field[i, j].flagged == true)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write($"{"#",-3}");
                            Console.ResetColor();
                        }
                        else
                            Console.Write($"{"#",-3}");
                    }
                    else if (field[i, j].is_mine == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{"*",-3}");
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (field[i, j].bombs)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;
                            case 7:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                break;
                            case 8:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                        }
                        Console.Write($"{field[i, j].bombs,-3}");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }
        public void generate()
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    field[i, j] = new Cell(false, 0);
                }
            }
            Random rnd = new Random();
            int rand;
            int rand2;
            for (int i = 0; i < m + 1; i++)
            {
                for (int j = 0; j < difficulty; j++)
                {
                    rand = rnd.Next(n);
                    rand2 = rnd.Next(m);
                    while (field[rand2, rand].is_mine != false)
                    {
                        rand = rnd.Next(n);
                        rand2 = rnd.Next(m);
                    }
                    field[rand2, rand].is_mine = true;
                    field[rand2, rand].bombs = 9;
                }
            }
            int counter = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field[i, j].is_mine == true)
                        continue;
                    //up
                    if (i == 0)
                    {
                        //upper left corner
                        if (j == 0)
                        {
                            //right
                            if (field[i, j + 1].is_mine == true)
                                counter++;
                            //down right
                            if (field[i + 1, j + 1].is_mine == true)
                                counter++;
                            //down
                            if (field[i + 1, j].is_mine == true)
                                counter++;
                        }
                        else if (j == n - 1)//upper right corner
                        {
                            //down
                            if (field[i + 1, j].is_mine == true)
                                counter++;
                            //down left
                            if (field[i + 1, j - 1].is_mine == true)
                                counter++;
                            //left
                            if (field[i, j - 1].is_mine == true)
                                counter++;
                        }
                        else //whole upper border
                        {
                            //right
                            if (field[i, j + 1].is_mine == true)
                                counter++;
                            //down right
                            if (field[i + 1, j + 1].is_mine == true)
                                counter++;
                            //down
                            if (field[i + 1, j].is_mine == true)
                                counter++;
                            //down left
                            if (field[i + 1, j - 1].is_mine == true)
                                counter++;
                            //left
                            if (field[i, j - 1].is_mine == true)
                                counter++;
                        }
                    }
                    else if (i == m - 1)
                    {
                        //bottom left corner
                        if (j == 0)
                        {
                            //up
                            if (field[i - 1, j].is_mine == true)
                                counter++;
                            //up right
                            if (field[i - 1, j + 1].is_mine == true)
                                counter++;
                            //right
                            if (field[i, j + 1].is_mine == true)
                                counter++;

                        }
                        else if (j == n - 1) // bottom right corner
                        {
                            //up
                            if (field[i - 1, j].is_mine == true)
                                counter++;
                            //left
                            if (field[i, j - 1].is_mine == true)
                                counter++;
                            //up left
                            if (field[i - 1, j - 1].is_mine == true)
                                counter++;
                        }
                        else //bottom
                        {
                            //up
                            if (field[i - 1, j].is_mine == true)
                                counter++;
                            //up right
                            if (field[i - 1, j + 1].is_mine == true)
                                counter++;
                            //right
                            if (field[i, j + 1].is_mine == true)
                                counter++;
                            //left
                            if (field[i, j - 1].is_mine == true)
                                counter++;
                            //up left
                            if (field[i - 1, j - 1].is_mine == true)
                                counter++;
                        }
                    }
                    else if (j == 0) // left side
                    {
                        //up
                        if (field[i - 1, j].is_mine == true)
                            counter++;
                        //up right
                        if (field[i - 1, j + 1].is_mine == true)
                            counter++;
                        //right
                        if (field[i, j + 1].is_mine == true)
                            counter++;
                        //down right
                        if (field[i + 1, j + 1].is_mine == true)
                            counter++;
                        //down
                        if (field[i + 1, j].is_mine == true)
                            counter++;
                    }
                    else if (j == n - 1) // right side
                    {
                        //down
                        if (field[i + 1, j].is_mine == true)
                            counter++;
                        //down left
                        if (field[i + 1, j - 1].is_mine == true)
                            counter++;
                        //left
                        if (field[i, j - 1].is_mine == true)
                            counter++;
                        //up left
                        if (field[i - 1, j - 1].is_mine == true)
                            counter++;
                        //up
                        if (field[i - 1, j].is_mine == true)
                            counter++;
                    }
                    else
                    {
                        //up
                        if (field[i - 1, j].is_mine == true)
                            counter++;
                        //up right
                        if (field[i - 1, j + 1].is_mine == true)
                            counter++;
                        //right
                        if (field[i, j + 1].is_mine == true)
                            counter++;
                        //down right
                        if (field[i + 1, j + 1].is_mine == true)
                            counter++;
                        //down
                        if (field[i + 1, j].is_mine == true)
                            counter++;
                        //down left
                        if (field[i + 1, j - 1].is_mine == true)
                            counter++;
                        //left
                        if (field[i, j - 1].is_mine == true)
                            counter++;
                        //up left
                        if (field[i - 1, j - 1].is_mine == true)
                            counter++;
                    }
                    field[i, j].bombs = counter;
                    counter = 0;
                }
            }
        }
        public void opener(int i, int j)
        {
            if (field[i, j].is_mine == false)
                win_counter++;
            //up
            if (i == 0)
            {
                //upper left corner
                if (j == 0)
                {
                    //right
                    if (field[i, j + 1].openable())
                    {
                        field[i, j + 1].opened = true;
                        opener(i, j + 1);
                        // if (field[i, j + 1].bombs != 0)
                        //    return;
                    }
                    //down right
                    if (field[i + 1, j + 1].openable())
                    {
                        field[i + 1, j + 1].opened = true;
                        opener(i + 1, j + 1);
                        //if (field[i + 1, j + 1].bombs != 0)
                        // return;
                    }
                    //down
                    if (field[i + 1, j].openable())
                    {
                        field[i + 1, j].opened = true;
                        opener(i + 1, j);
                        //if (field[i + 1, j].bombs != 0)
                        //return;
                    }
                }
                else if (j == n - 1)//upper right corner
                {
                    //down
                    if (field[i + 1, j].openable() == true)
                    {
                        field[i + 1, j].opened = true;
                        opener(i + 1, j);
                    }
                    //down left
                    if (field[i + 1, j - 1].openable() == true)
                    {
                        field[i + 1, j - 1].opened = true;
                        opener(i + 1, j - 1);
                    }
                    //left
                    if (field[i, j - 1].openable() == true)
                    {
                        field[i, j - 1].opened = true;
                        opener(i, j - 1);
                    }
                }
                else //whole upper border
                {
                    //right
                    if (field[i, j + 1].openable() == true)
                    {
                        field[i, j + 1].opened = true;
                        opener(i, j + 1);
                    }
                    //down right
                    if (field[i + 1, j + 1].openable() == true)
                    {
                        field[i + 1, j + 1].opened = true;
                        opener(i + 1, j + 1);
                    }
                    //down
                    if (field[i + 1, j].openable() == true)
                    {
                        field[i + 1, j].opened = true;
                        opener(i + 1, j);
                    }
                    //down left
                    if (field[i + 1, j - 1].openable() == true)
                    {
                        field[i + 1, j - 1].opened = true;
                        opener(i + 1, j - 1);
                    }
                    //left
                    if (field[i, j - 1].openable() == true)
                    {
                        field[i, j - 1].opened = true;
                        opener(i, j - 1);
                    }
                }
            }
            else if (i == m - 1)
            {
                //bottom left corner
                if (j == 0)
                {
                    //up
                    if (field[i - 1, j].openable() == true)
                    {
                        field[i - 1, j].opened = true;
                        opener(i - 1, j);
                    }
                    //up right
                    if (field[i - 1, j + 1].openable() == true)
                    {
                        field[i - 1, j + 1].opened = true;
                        opener(i - 1, j + 1);
                    }
                    //right
                    if (field[i, j + 1].openable() == true)
                    {
                        field[i, j + 1].opened = true;
                        opener(i, j + 1);
                    }
                }
                else if (j == n - 1) // bottom right corner
                {
                    //up
                    if (field[i - 1, j].openable() == true)
                    {
                        field[i - 1, j].opened = true;
                        opener(i - 1, j);
                    }
                    //left
                    if (field[i, j - 1].openable() == true)
                    {
                        field[i, j - 1].opened = true;
                        opener(i, j - 1);
                    }
                    //up left
                    if (field[i - 1, j - 1].openable() == true)
                    {
                        field[i - 1, j - 1].opened = true;
                        opener(i - 1, j - 1);
                    }
                }
                else //bottom
                {
                    //up
                    if (field[i - 1, j].openable() == true)
                    {
                        field[i - 1, j].opened = true;
                        opener(i - 1, j);
                    }
                    //up right
                    if (field[i - 1, j + 1].openable() == true)
                    {
                        field[i - 1, j + 1].opened = true;
                        opener(i - 1, j + 1);
                    }
                    //right
                    if (field[i, j + 1].openable() == true)
                    {
                        field[i, j + 1].opened = true;
                        opener(i, j + 1);
                    }
                    //left
                    if (field[i, j - 1].openable() == true)
                    {
                        field[i, j - 1].opened = true;
                        opener(i, j - 1);
                    }
                    //up left
                    if (field[i - 1, j - 1].openable() == true)
                    {
                        field[i - 1, j - 1].opened = true;
                        opener(i - 1, j - 1);
                    }
                }
            }
            else if (j == 0) // left side
            {
                //up
                if (field[i - 1, j].openable() == true)
                {
                    field[i - 1, j].opened = true;
                    opener(i - 1, j);
                }
                //up right
                if (field[i - 1, j + 1].openable() == true)
                {
                    field[i - 1, j + 1].opened = true;
                    opener(i - 1, j + 1);
                }
                //right
                if (field[i, j + 1].openable() == true)
                {
                    field[i, j + 1].opened = true;
                    opener(i, j + 1);
                }
                //down right
                if (field[i + 1, j + 1].openable() == true)
                {
                    field[i + 1, j + 1].opened = true;
                    opener(i + 1, j + 1);
                }
                //down
                if (field[i + 1, j].openable() == true)
                {
                    field[i + 1, j].opened = true;
                    opener(i + 1, j);
                }
            }
            else if (j == n - 1) // right side
            {
                //down
                if (field[i + 1, j].openable() == true)
                {
                    field[i + 1, j].opened = true;
                    opener(i + 1, j);
                }
                //down left
                if (field[i + 1, j - 1].openable() == true)
                {
                    field[i + 1, j - 1].opened = true;
                    opener(i + 1, j - 1);
                }
                //left
                if (field[i, j - 1].openable() == true)
                {
                    field[i, j - 1].opened = true;
                    opener(i, j - 1);
                }
                //up left
                if (field[i - 1, j - 1].openable() == true)
                {
                    field[i - 1, j - 1].opened = true;
                    opener(i - 1, j - 1);
                }
                //up
                if (field[i - 1, j].openable() == true)
                {
                    field[i - 1, j].opened = true;
                    opener(i - 1, j);
                }
            }
            else
            {
                //up
                if (field[i - 1, j].openable() == true)
                {
                    field[i - 1, j].opened = true;
                    opener(i - 1, j);
                }
                //up right
                if (field[i - 1, j + 1].openable() == true)
                {
                    field[i - 1, j + 1].opened = true;
                    opener(i - 1, j + 1);
                }
                //right
                if (field[i, j + 1].openable() == true)
                {
                    field[i, j + 1].opened = true;
                    opener(i, j + 1);
                }
                //down right
                if (field[i + 1, j + 1].openable() == true)
                {
                    field[i + 1, j + 1].opened = true;
                    opener(i + 1, j + 1);
                }
                //down
                if (field[i + 1, j].openable() == true)
                {
                    field[i + 1, j].opened = true;
                    opener(i + 1, j);
                }
                //down left
                if (field[i + 1, j - 1].openable() == true)
                {
                    field[i + 1, j - 1].opened = true;
                    opener(i + 1, j - 1);
                }
                //left
                if (field[i, j - 1].openable() == true)
                {
                    field[i, j - 1].opened = true;
                    opener(i, j - 1);
                }
                //up left
                if (field[i - 1, j - 1].openable() == true)
                {
                    field[i - 1, j - 1].opened = true;
                    opener(i - 1, j - 1);
                }
            }
        }
        public bool openCell(int x, int y)
        {
            if (x > m || x < 0 || y > n || y < 0)
            {
                Console.WriteLine("Cell is outside of borders.");
                return false;
            }
            else if (field[x, y].opened == true)
                Console.WriteLine("Cell was already opened.");
            else if(field[x,y].flagged == true)
            {
                Console.WriteLine("Flagged Cell.");
                return false;
            }
            else
            {
                field[x, y].opened = true;
                if (field[x, y].is_mine == false)
                    win_counter++;
                if (field[x, y].bombs == 0)
                    opener(x, y);
            }

            if (field[x, y].is_mine == true)
                return true;

            return false;
        }
        public void flag(int x, int y)
        {
            if (x > m || x < 0 || y > n || y < 0)
            {
                Console.WriteLine("Cell is outside of borders.");
            }
            else if (field[x, y].opened == true)
                Console.WriteLine("Cell was already opened.");
            else
            {
                field[x, y].flagged = true;
            }
        }
        public void unflag(int x, int y)
        {
            if (x > m || x < 0 || y > n || y < 0)
            {
                Console.WriteLine("Cell is outside of borders.");
            }
            else if (field[x, y].opened == true)
                Console.WriteLine("Cell was already opened.");
            else
            {
                field[x, y].flagged = false;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            int x, y;
            string z;
            Minefield myField = new Minefield(10,10,1);
            myField.generate();
            while (true)
            {
                myField.fieldPrint();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nFirst write command open/flag/unflag. Then write coordinates of Cell you want to open/flag/unflag (format: command x y, where x is horizontal coordinate and y is vertical coordinate).");
                Console.ResetColor();
                line = Console.ReadLine();
                Console.Clear();
                z = line.Split()[0];
                x = Convert.ToInt32(line.Split()[1]);
                y = Convert.ToInt32(line.Split()[2]);
                if (z == "open")
                {
                    if (myField.openCell(x - 1, y - 1))
                    {
                        myField.fieldPrint();
                        Console.WriteLine("Game Over");
                        break;
                    }
                }
                else if(z == "flag")
                {
                    myField.flag(x - 1, y - 1);
                }
                else if (z == "unflag")
                {
                    myField.unflag(x - 1, y - 1);
                }
                else
                {
                    Console.WriteLine("Unknown Command.");
                }
                if (myField.win())
                {
                    myField.fieldPrint();
                    Console.WriteLine("You win!");
                    break;
                }
            }
        }
    }
}
