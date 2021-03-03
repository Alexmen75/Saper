using System;
using Core;

namespace сапер
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor value = ConsoleColor.Blue; // переменная цвета 

            Console.CursorSize = 100;//размер курсора максимальный

            ConsoleKey key;//переменная , которая будет служить для чтения ввода с клавиатуры

            bool game = false;// логическая переменная для проверки состояния запуска игры 

            int[,] map = new int[12,12];//массив чисел из которого будет браться информация о карте
            int num=0;// переменная для подсчета количества очков, нужных для победы
            int[,] score = new int[12, 12];// массив чисел, нужный для получения информации о том, сколько очков будет получено
           // char[,] mask = new char[10, 10];
            while (true)// начало цикла 
            {
                Console.ForegroundColor = value;// устанавливаем синий цвет для вывода 
                Console.WriteLine("# Начать игру\n");// наименование строчек меню
                Console.WriteLine("# Управление\n");
                Console.WriteLine("# Выход\n");
                Console.SetCursorPosition(0, 0);// установка курсора на позицию 0х0у
                while(true)//цикл меню игры 
                {
                    key = Console.ReadKey(true).Key;// чтение введеной с клавиатуры кнопки
                    switch (key)// Здесь проводится настройка управления курсором в меню
                    {
                        case ConsoleKey.UpArrow:
                            if(Console.CursorTop!=0)// данная проверка есть везде, для того, чтобы курсор не выходил далье, чем нужно, так же предостерегает от вылета из программы
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);// перемещение курсора вверх
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if(Console.CursorTop!=4)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 2);// перемещение курсора вниз
                            }
                            break;
                        case ConsoleKey.Enter:
                            switch (Console.CursorTop)// идет проверка местанахождения курсора
                            {
                                case 0:
                                    game = true;// курсор на 0х0у - игра началась
                                    Console.Clear();// отчистка консоли
                                    break;
                                case 2:// вывод информация об управлении
                                    Console.Clear();
                                    Console.WriteLine("Управление куросором: Arrows");
                                    Console.WriteLine("Выбрать: Enter");
                                    Console.WriteLine("Пометить: Space");
                                    Console.ReadKey();
                                    Console.Clear();

                                    Console.WriteLine("# Начать игру\n");// после отистки консоли будет возвращаться в главное меню
                                    Console.WriteLine("# Управление\n");
                                    Console.WriteLine("# Выход\n");
                                    Console.SetCursorPosition(0, 0);
                                    break;
                                case 4:
                                    return;// завершение работы программы при выборе "Выход "
                            }
                            break;
                            
                    }
                    if(game==true)
                    {
                        break;// если игра началась, то остановить цикл Меню
                    }
                }
                
               var map_info = Logic.Map_create();// получение информации о карте из библиотеки Core/
                map = map_info.Item1;// заполнение карты всеми нужными значениями
                score = map_info.Item2;// получение информации о том, сколько очков дает каждая ячейка
                num = map_info.Item3;// количество очков, нужное для победы 
                 
                Console.WriteLine("\n");
                for (int i = 0; i < 10; i++)// вывод "Маски карты"
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

                Console.SetCursorPosition(4, 2);// установка позиции курсора 
                Console.ForegroundColor = ConsoleColor.Gray;//установка 

                int pos_T = 1;// переменные  для отслеживания положения курсора в массиве 
                int pos_L = 1;
                int num_to_win = 0;//счетчик до победы
                while (game == true) // начало цикла игры
                {
                    key = Console.ReadKey(true).Key;//чтение с клавиатуры
                    switch (key)// здесь идет логика перемещения курсора 
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
                            int C_L = Console.CursorLeft;// переменные нужные для возвращения курсора на позицию 
                            int C_T = Console.CursorTop;
                            Console.SetCursorPosition(0, 0);
                            Console.Write("pos_L=" + pos_L + " \tpos_T=" + pos_T + " ");// вспомогательные выводы, для понимания процесса
                            Console.SetCursorPosition(0, 1);
                            Console.Write("Cursor_Left=" + C_L + " \tCursor_Top=" + C_T + " ");
                            Console.SetCursorPosition(C_L, C_T);
                            if (map[pos_T, pos_L] != 9)// 9- бомба, если выбранная ячейка не равна 9, то продолжать работу 
                            {
                                if (map[pos_T, pos_L] == 0)// если ячейка пустая то 
                                {
                                    Console.Write("▒");// вывести на позицию пустую фигуру

                                    Logic.Mip_map(pos_T, pos_L, map, ref score, ref num_to_win); // запустить процесс нахождения пустых ячеек до цифр
                                }
                                else if (map[pos_T, pos_L] > 0 && map[pos_T, pos_L] != 11)// если выбранная ячейка равна какому-то количеству очков (11 означает, что ячейка уже открыта)
                                {
                                    Logic.Color_reg(map[pos_T, pos_L]);// проверка нужного цвета(1-зеленый, 2 желтый, 3 - крассный , > 3 темно-крассный 
                                    Console.Write(map[pos_T, pos_L]);// вывод цифры с нужным цветом на выбранной позиции
                                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);// так как после вывода курсор перемещается вправо его нужно вернуть на нужную позицию
                                    num_to_win += score[pos_T, pos_L];// подсчет полученных очков 
                                    score[pos_T, pos_L] = 0;// отчищает карту очков , на выбранной позиции, чтобы при нажатии на уже открытую цифру очки не начислялись
                                    Console.ForegroundColor = ConsoleColor.Gray;// возвращение цвета вывода 
                                }

                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;// если выбранная ячейка равна 9 
                                Console.Write("@");// вывести знак бомбы 
                                for (int i = 1; i < 11; i++) // вывод всех бомб, что есть на карте 
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
                                Console.SetCursorPosition(50, 5);
                                Console.Write("You Lose");
                                Console.SetCursorPosition(0, 25);
                                Console.ReadKey();
                                game = false;// остановка цикла игры 
                                break;
                            }
                            Console.SetCursorPosition(0, 25);
                            Console.Write("num = " + num + " \tScore=" + num_to_win + " ");// вывод количества очков , нужного для победы и очков игрока 
                            Console.SetCursorPosition(C_L, C_T);// возвращение курсора на нужные позиции
                            break;
                        case ConsoleKey.Spacebar:// "Флажок для пометки бомбы "
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("?");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case ConsoleKey.Escape: return;// выход 
                    }
                    if (num_to_win == num)// завершение игры , при открытии всех цифр
                    {
                        
                        Console.WriteLine("You Win!");
                        game = false;
                        Console.ReadKey();
                    }
                }
                Console.SetCursorPosition(50, 10);
                Console.SetCursorPosition(0, 25);
                Console.Clear();
            }
        }
    }
}
