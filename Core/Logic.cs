using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Logic
    {
        public static Tuple<int[,], int[,], int>  Map_create()
        {
            int[,] map_t = new int[12, 12];
            int[,] score_t = new int[12, 12];
            //int[,] map_t = new int[12, 12];
            int num_t = 0;
            Random i_bomb = new Random();
            Random j_bomb = new Random();
            for (int i = 0; i < 10; i++)
            {
                map_t[i_bomb.Next(1, 10), j_bomb.Next(1, 10)] = 9;
            }
            for (int i = 1; i <= 10; i++)
            {
                //Console.WriteLine(i);
                for (int j = 1; j <= 10; j++)
                {
                    //Console.WriteLine(i);
                    if (map_t[i, j] != 9)
                    {
                        //Console.WriteLine("if");
                        int count_bomb = 0;
                        for (int i1 = i - 1; i1 <= i + 1; i1++)
                        {
                            // Console.WriteLine("i"+i1);
                            for (int j1 = j - 1; j1 <= j + 1; j1++)
                            {
                                //Console.WriteLine("j" + j1);
                                if (map_t[i1, j1] == 9)
                                {
                                    count_bomb++;
                                }
                            }
                        }
                        score_t[i, j] = map_t[i, j] = count_bomb;
                        num_t+= count_bomb;
                    }
                }
            }
            return Tuple.Create(map_t, score_t, num_t);
        }
       public static void Mip_map(int T, int L, int[,] map, ref int[,] score, ref int num_to_win)
        {
            for (int i = T + 1; ; i++)
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
        }
        public static void Color_reg(int num_color)
        {
            switch (num_color)
            {
                case 1: Console.ForegroundColor = ConsoleColor.Green; break;
                case 2: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 3: Console.ForegroundColor = ConsoleColor.Red; break;
                default: Console.ForegroundColor = ConsoleColor.DarkRed; break;

            }
        }
    }
}
