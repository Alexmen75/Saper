using System;
using Core;

namespace сапер
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(44, 25);
            Console.SetBufferSize(44, 25);

            ConsoleColor value = ConsoleColor.Blue;

            Console.CursorSize = 100;

            ConsoleKey key;

            bool game = false;

            // Random all = new Random();
            //(int[,], int[,], int) map_info;

            Random i_bomb = new Random();
            Random j_bomb = new Random();
            int[,] map = new int[12,12];
            int num=0;
            int[,] score = new int[12, 12];
           // char[,] mask = new char[10, 10];
            while (true)
            {
                Console.ForegroundColor = value;
                Console.WriteLine("# Начать игру\n");
                Console.WriteLine("# Управление\n");
                Console.WriteLine("# Выход\n");
                Console.SetCursorPosition(0, 0);
                while(true)
                {
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if(Console.CursorTop!=0)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if(Console.CursorTop!=4)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                            }
                            break;
                        case ConsoleKey.Enter:
                            switch (Console.CursorTop)
                            {
                                case 0:
                                    game = true;
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("Управление куросором: Arrows");
                                    Console.WriteLine("Выбрать: Enter");
                                    Console.WriteLine("Пометить: Space");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Console.WriteLine("# Начать игру\n");
                                    Console.WriteLine("# Управление\n");
                                    Console.WriteLine("# Выход\n");
                                    Console.SetCursorPosition(0, 0);
                                    break;
                                case 4:
                                    return;
                            }
                            break;
                            
                    }
                    if(game==true)
                    {
                        break;
                    }
                }
                
               var map_info = Logic.Map_create();
                map = map_info.Item1;
                score = map_info.Item2;
                num = map_info.Item3;
                 

                /* for (int i = 0; i < 12; i++)
                 {
                     for (int j = 0; j < 12; j++)
                     {
                         map[i, j] = 0;
                         if (i <= 9 && j <= 9)
                         {
                             mask[i, j] = '▓';
                         }

                     }
                }
                 for (int i = 0; i < 10; i++)
                 {
                     map[i_bomb.Next(1, 10), j_bomb.Next(1, 10)] = 9;
                 }
                 for (int i = 1; i <= 10; i++)
                 {
                     //Console.WriteLine(i);
                     for (int j = 1; j <= 10; j++)
                     {
                         //Console.WriteLine(i);
                         if (map[i, j] != 9)
                         {
                             //Console.WriteLine("if");
                             int count_bomb = 0;
                             for (int i1 = i - 1; i1 <= i + 1; i1++)
                             {
                                 // Console.WriteLine("i"+i1);
                                 for (int j1 = j - 1; j1 <= j + 1; j1++)
                                 {
                                     //Console.WriteLine("j" + j1);
                                     if (map[i1, j1] == 9)
                                     {
                                         count_bomb++;
                                     }
                                 }
                             }
                             score[i, j] = map[i, j] = count_bomb;
                             num += count_bomb;
                         }
                     }
                 }
                */
                Console.WriteLine("\n");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("    ");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write("▓" + "   ");
                    }
                    //for (int j = 0; j < 10; j++)
                   // {
                   //     Console.Write(map[i, j] + "   ");
                  //  }
                    Console.WriteLine("\n");
                }

                Console.SetCursorPosition(4, 2);
                Console.ForegroundColor = ConsoleColor.Gray;

                int pos_T = 1;
                int pos_L = 1;
                int num_to_win = 0;
                while (game == true) 
                {
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (Console.CursorTop != 2)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                                pos_T--;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (Console.CursorLeft != 4)
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop);
                                pos_L--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (Console.CursorTop != 18 + 2)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);
                                pos_T++;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (Console.CursorLeft != 36 + 4)
                            {
                                Console.SetCursorPosition(Console.CursorLeft + 4, Console.CursorTop);
                                pos_L++;
                            }
                            break;
                        case ConsoleKey.Enter:
                            int C_L = Console.CursorLeft;
                            int C_T = Console.CursorTop;
                            Console.SetCursorPosition(0, 0);
                            Console.Write("pos_L=" + pos_L + " \tpos_T=" + pos_T + " ");
                            Console.SetCursorPosition(0, 1);
                            Console.Write("Cursor_Left=" + C_L + " \tCursor_Top=" + C_T + " ");
                            Console.SetCursorPosition(C_L, C_T);
                            if (map[pos_T, pos_L] != 9)
                            {
                                if (map[pos_T, pos_L] == 0)
                                {
                                    Console.Write("▒");

                                    Logic.Mip_map(pos_T, pos_L, map, ref score, ref num_to_win);
                                }
                                else if (map[pos_T, pos_L] > 0 && map[pos_T, pos_L] != 11)
                                {
                                    Logic.Color_reg(map[pos_T, pos_L]);
                                    Console.Write(map[pos_T, pos_L]);
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                    num_to_win += score[pos_T, pos_L];
                                    score[pos_T, pos_L] = 0;
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                //Console.SetCursorPosition(0, 30);
                                //map_Drow(map);
                                //Console.SetCursorPosition(C_L, C_T);
                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write("@");
                                for (int i = 1; i < 11; i++)
                                {
                                    //Console.Write("i=" + i);
                                    for (int j = 1; j < 11; j++)
                                    {
                                        //Console.Write("j=" + j);
                                        if (map[i, j] == 9)
                                        {
                                            Console.SetCursorPosition(j * 4, i * 2);
                                            Console.Write("@");
                                        }
                                    }
                                }
                                Console.SetCursorPosition(0,24);
                                Console.Write("You Lose");
                                Console.SetCursorPosition(0, 24);
                                Console.ReadKey();
                                game = false;
                                break;
                            }
                            Console.SetCursorPosition(0, 24);
                            Console.Write("num = " + num + " \tScore=" + num_to_win + " ");
                            Console.SetCursorPosition(C_L, C_T);
                            break;
                        case ConsoleKey.Spacebar:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("?");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case ConsoleKey.Escape: return;
                    }
                    if (num_to_win == num)
                    {
                        
                        Console.WriteLine("You Win!");
                        game = false;
                        Console.ReadKey();
                    }
                }
                num = 0;
                Console.SetCursorPosition(43, 10);
                Console.SetCursorPosition(0, 24);
                Console.Clear();
            }
        }
        /*static void Mip_map(int T,int L, int[,] map,ref int[,] score,ref int num_to_win)
        {
            for (int i = T+1; ; i++)
            {
                //Console.ReadKey();
               // Console.SetCursorPosition(40, 1);
                //Console.Write("i=" + i);
                if (i > 10)
                {
                    break;
                }
                else if (map[i, L] == 11)
                {
                    break;
                }
                else if (map[i, L] == 0)
                {
                    Console.SetCursorPosition(L * 4, i * 2);
                    Console.Write("▒");
                    map[i, L] = 11;
                }
                else if (map[i, L] > 0)
                {
                    num_to_win += score[i, L];
                    score[i, L] = 0;
                    Console.SetCursorPosition(L * 4, i * 2);
                    Color_reg(map[i, L]);
                    Console.Write(map[i, L]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                
                Mip_map(i, L, map, ref score, ref num_to_win);
            }

            for (int i = T - 1; ; i--)
            {
                //Console.ReadKey();
                //Console.SetCursorPosition(55, 0);
                //Console.Write("i=" + i);
                if (i < 1)
                {
                    Console.SetCursorPosition(55, 0);
                    //Console.Write("break 1");
                    break;
                }
                else if (map[i, L] == 11)
                {
                    Console.SetCursorPosition(55, 0);
                    //Console.Write("break 2");
                    break;
                }
                else if (map[i, L] == 0)
                {
                    Console.SetCursorPosition(L * 4, i * 2);
                    Console.Write("▒");
                    map[i, L] = 11;
                }
                else if (map[i, L] > 0)
                {
                    num_to_win += score[i, L];
                    score[i, L] = 0;
                    Console.SetCursorPosition(L * 4, i * 2);
                    Color_reg(map[i, L]);
                    Console.Write(map[i, L]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                Mip_map(i, L, map, ref score, ref num_to_win);
            }
            
            for (int j = L - 1; ; j--)
            {
                //Console.ReadKey();
               // Console.SetCursorPosition(70, 0);
               // Console.Write("j=" + i);
                if (j < 1)
                {
                    //Console.SetCursorPosition(70, 0);
                    //Console.Write("break 1");
                    break;
                }
                else if (map[T, j] == 11)
                {
                    //Console.SetCursorPosition(70, 0);
                    //Console.Write("break 2");
                    break;
                }
                else if (map[T, j] == 0)
                {
                    Console.SetCursorPosition(j * 4, T * 2);
                    Console.Write("▒");
                    map[T, j] = 11;
                }
                else if (map[T, j] > 0)
                {
                    num_to_win += score[T, j];
                    score[T, j] = 0;
                    Console.SetCursorPosition(j * 4, T * 2);
                    Color_reg(map[T, j]);
                    Console.Write(map[T, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                Mip_map(T, j, map, ref score, ref num_to_win);
            }
            for (int j = L + 1; ; j++)
            {
                //Console.ReadKey();
                //Console.SetCursorPosition(40, 0);
                // Console.Write("j=" + i);
                if (j > 10)
                {
                    //Console.SetCursorPosition(40, 0);
                    //Console.Write("break 1");
                    break;
                }
                else if (map[T, j] == 11)
                {
                    //Console.SetCursorPosition(40, 0);
                    //Console.Write("break 2");
                    break;
                }
                else if (map[T, j] == 0)
                {
                    Console.SetCursorPosition(j * 4, T * 2);
                    Console.Write("▒");
                    map[T, j] = 11;
                }
                else if (map[T, j] > 0)
                {
                    num_to_win += score[T, j];
                    score[T, j] = 0;
                    Console.SetCursorPosition(j * 4, T * 2);
                    Color_reg(map[T, j]);
                    Console.Write(map[T, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                Mip_map(T, j, map, ref score, ref num_to_win);
            }
        }*/
        /*static void Color_reg(int num_color)
        {
            switch (num_color)
            {
                case 1: Console.ForegroundColor = ConsoleColor.Green; break;
                case 2: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 3: Console.ForegroundColor = ConsoleColor.Red; break;
                default: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                
            }

            
        }*/
       /* static void map_Drow(int [,] map)
        {
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(map[i, j] + "   ");
                }
                Console.WriteLine("\n");
            }
        }*/
    }
}
