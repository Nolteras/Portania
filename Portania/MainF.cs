using System;
using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Portania
{
    public static class MainF
    {
        public static void RenderGame()
        {
            RenderConsle.Start();
        }

    }



    static class RenderConsle
    {
        static  GameLogics gameLogics = new GameLogics();
        static Random random = new Random();// Рандом
        static ConsoleWindow game = null;// Игровая консоль
        static readonly int MaxY = 40;
        static readonly int MaxX = 60;
        static readonly int GuiY = MaxY + 10;
        public static void Start()// Создание и запись в game мира
        {
            ConsoleWindow console = new ConsoleWindow(GuiY, MaxX, "Portania");// создание окна
            game = console;// запись
            GenerationWorld();// генерация мира
            while (console.WindowUpdate())
            {
                Controls();
                GuiUpdate();
            }
        }







        static void GenerationWorld()
        {
            int X = 0; // Текущий ряд
            int Y = 0; // Текущий столбец
            bool TerraGenF = false; // Завершение генерации карты

            while (TerraGenF != true) // Пока не завершится генерация, будет работать
            {
                while (Y != MaxY) // Пока текущий заполняемый столбец не будет равен последнему существуемому столбцу
                {
                    while (X != MaxX) // Пока ряд не будет заполнен
                    {
                        game.Write(Y, X, '.', Color4.Green); // Заполняет текущий тайл
                        X++; // Переходит к соседнему ряду
                    }
                    Y++; // Переходит к соседнему столбцу
                    X = 0; // Обнуляет ряд
                }
                if (Y == MaxY) // Если все столбцы заполнены
                {
                    TerraGenF = true; // Заканчивает генерацию
                }
                X = 0;
                while (X != MaxX)
                {
                    game.Write(MaxY, X, '#', Color4.White);
                    X++;
                }
                Y = 0; //Обнуляет координаты для дальнейшего использования
                X = 0;   //

                //======================================Генерация лесов======================================\\
                for (int i = 0; i < random.Next(5, 15); i++)
                {
                    forestsGen();
                }
                //======================================Генерация рек======================================\\
                riversGen();
                //======================================Генерация озёр======================================\\
                for (int i = 0; i < random.Next(3, 8); i++)
                {
                    lakesGen();
                }
                //======================================Генерация GUI======================================\\
                GuiUpdate();
            }







            //======================================Код генерации рек======================================\\
            void riversGen()
            {
                Y = random.Next(0, MaxY); // Координаты центра реки
                X = random.Next(0, MaxX); // 
                game.Write(Y, X, '.', Color4.Blue, Color4.Blue);
                game.Write(Y, X - 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                game.Write(Y, X + 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                int rY = Y;
                while ((rY - 1) != -1) //Пока Y не уйдёт в минус, будем делать реки
                {
                    game.Write(rY - 1, X, '.', Color4.Blue, Color4.Blue);
                    game.Write(rY - 1, X - 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                    game.Write(rY - 1, X + 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                    rY--;
                }
                rY = Y;
                while ((rY + 1) != MaxY) //Пока Y не станет больше, чем максимум, будем делать реки
                {
                    game.Write(rY + 1, X, '.', Color4.Blue, Color4.Blue);
                    game.Write(rY + 1, X + 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                    game.Write(rY + 1, X - 1, '.', Color4.SandyBrown, Color4.SandyBrown);
                    rY++;
                }
            }


            //======================================Код генерации озёр======================================\\
            int bigOrSmallLake = 0;
            void lakesGen()
            {
                Y = random.Next(0, MaxY); // Координаты центра озера
                X = random.Next(0, MaxX); //
                bigOrSmallLake = random.Next(1, 3);
                switch (bigOrSmallLake)
                {
                    case 1:
                        game.Write(Y, X, '~', Color4.Blue, Color4.LightCyan);
                        break;
                    case 2:
                        game.Write(Y, X, '~', Color4.Blue, Color4.LightCyan);
                        if ((Y - 1) != -1)
                        {
                            game.Write(Y - 1, X, '~', Color4.Blue, Color4.LightCyan);
                        }
                        if ((Y + 1) != MaxY)
                        {
                            game.Write(Y + 1, X, '~', Color4.Blue, Color4.LightCyan);
                        }
                        if ((X - 1) != -1)
                        {
                            game.Write(Y, X - 1, '~', Color4.Blue, Color4.LightCyan);
                        }
                        if ((X + 1) != MaxX)
                        {
                            game.Write(Y, X + 1, '~', Color4.Blue, Color4.LightCyan);
                        }
                        break;
                }
            }

            //======================================Код генерации леса======================================\\
            void forestsGen()
            {
                Y = random.Next(0, MaxY); // Выбираем случайный тайл
                X = random.Next(0, MaxX); // 
                game.Write(Y, X, 'F', Color4.ForestGreen);// Создаём на этом тайле лес
                if ((Y - 1) > -1) // Если координаты леса по X не будут уходить в минус, выращиваем на этих координатах лес
                {
                    for (int i = 0; i < random.Next(1, 10); i++)
                    {
                        if ((Y - i) > -1)
                        {
                            game.Write(Y - i, X, 'F', Color4.ForestGreen);
                            game.Write(Y - i, X - i, 'F', Color4.ForestGreen);
                        }
                    }
                }
                if ((Y + 1) < MaxY) // Если координаты леса по X не будут больше игрового поля, выращиваем на этих координатах лес
                {
                    for (int i = 0; i < random.Next(1, 10); i++)
                    {
                        if ((Y + i) < MaxY)
                        {
                            game.Write(Y + i, X, 'F', Color4.ForestGreen);
                            game.Write(Y + i, X + i, 'F', Color4.ForestGreen);
                        }
                    }
                }
                if ((X - 1) > -1) // Если координаты леса по Y не будут уходить в минус, выращиваем на этих координатах лес
                {
                    for (int i = 0; i < random.Next(1, 10); i++)
                    {
                        if ((X - i) > -1)
                        {
                            game.Write(Y, X - i, 'F', Color4.ForestGreen);
                            if ((Y - i) > -1)
                            {
                                game.Write(Y - i, X - i, 'F', Color4.ForestGreen);
                            }
                        }
                    }
                }
                if ((X + 1) < MaxX) // Если координаты леса по Y не будут больше игрового поля, выращиваем на этих координатах лес
                {
                    for (int i = 0; i < random.Next(1, 10); i++)
                    {
                        if ((X + i) < MaxX)
                        {
                            game.Write(Y, X + i, 'F', Color4.ForestGreen);
                            if ((Y + i) < MaxY)
                            {
                                game.Write(Y + i, X + i, 'F', Color4.ForestGreen);
                            }
                        }
                    }
                }
            }
        }


        //======================================Код GUI======================================\\
        static string curChar = "Null";
        static void GuiUpdate()
        {
            if (curTile == 'F')
            {
                curChar = "Forest";
            }
            if (curTile == '.')
            {
                if (curTileCol == Color4.Green)
                {
                    curChar = "Plain  ";
                }
                if (curTileCol == Color4.Blue)
                {
                    curChar = "River  ";
                }
                if (curTileCol == Color4.SandyBrown)
                {
                    curChar = "Beach  ";
                }
            }
            if (curTile == '~')
            {
                curChar = "Lake  ";
            }

            game.Write(GuiY - 9, 1, "Year", Color4.White);
            game.Write(GuiY - 9, 6, Convert.ToString(gameLogics.Year), Color4.White);

            game.Write(GuiY - 8, 1, "World Population", Color4.White);
            game.Write(GuiY - 8, 18, Convert.ToString(gameLogics.WorldPopulation), Color4.White);

            game.Write(GuiY - 1, 1, "Current tile", Color4.White);
            game.Write(GuiY - 1, 16, curChar, Color4.White);
            game.Write(GuiY - 1, 14, curTile, curTileCol, curBgTileCol);

        }


        //======================================Курсор======================================\\
        static int posX = 25;
        static int posY = 25;
        static bool isCurTileSaved = false;
        static char curTile = 'n'; //Записываем текущим тайлом какую-то ерунду, всё-равно перепишется
        static Color4 curTileCol = Color4.White;
        static Color4 curBgTileCol = Color4.White;
        static void Controls()
        {


            if (!isCurTileSaved) //перезаписываем текущий тайл и запрещаем его перезаписывать
            {
                GetCurTile();
                isCurTileSaved = true;
            }
            game.Write(posY, posX, 'X', Color4.White); //рисуем курсор


            void GetCurTile()//Сохраняем тайл
            {
                curTile = game.GetChar(posY, posX);
                curTileCol = game.GetColor(posY, posX);
                curBgTileCol = game.GetBackgroundColor(posY, posX);
            }

            if (game.KeyPressed) //Если кака-то кнопка нажата
            {
                Key key = game.GetKey();
                if (key == Key.Up) // Если это стрелка вверх
                {
                    if (posY - 1 == -1)
                    {
                        return;
                    }
                    game.Write(posY, posX, curTile, curTileCol, curBgTileCol); //Рисуем на месте курсора тайл, который был там до этого
                    posY = posY - 1; //Перемещаем по Y-координате вверх
                    GetCurTile();
                    game.Write(posY, posX, 'X', Color4.White); //Рисуем там курсор
                }
                if (key == Key.Down) // Если это стрелка вниз
                {
                    if (posY + 1 == MaxY)
                    {
                        return;
                    }
                    game.Write(posY, posX, curTile, curTileCol, curBgTileCol); //Рисуем на месте курсора тайл, который был там до этого
                    posY = posY + 1; //Перемещаем по Y-координате вниз
                    GetCurTile();
                    game.Write(posY, posX, 'X', Color4.White); //Рисуем там курсор
                }
                if (key == Key.Left) // Если стрелка влево
                {
                    if (posX - 1 == -1)
                    {
                        return;
                    }
                    game.Write(posY, posX, curTile, curTileCol, curBgTileCol); //Рисуем на месте курсора тайл, который был там до этого
                    posX = posX - 1; //Перемещаем по X-координате влево
                    GetCurTile();
                    game.Write(posY, posX, 'X', Color4.White); //Рисуем там курсор
                }
                if (key == Key.Right) // Если это стрелка вправо
                {
                    if (posX + 1 == MaxX)
                    {
                        return;
                    }
                    game.Write(posY, posX, curTile, curTileCol, curBgTileCol); //Рисуем на месте курсора тайл, который был там до этого
                    posX = posX + 1; //Перемещаем по X-координате вправо
                    GetCurTile();
                    game.Write(posY, posX, 'X', Color4.White); //Рисуем там курсор
                }
                if(key == Key.A)
                {
                    gameLogics.AddPeople();
                }
                if (key == Key.N)
                {
                    gameLogics.Logic();
                }
            }
        }
    }

    public class GameLogics //Игровая логика
    {
        public int WorldPopulation = 0; //Мировая популяция
        public int Year = 0; //Год
        int timerOfDeath = -1; //Таймер, по истечению которого старые люди будут умирать(50 лет) 
        int timerOfBirth = 0;
        int peopleToDieNormal = 0;//Старые люди
        public void Logic() 
        {
            Year++;
            timerOfDeath++;
            timerOfBirth++;
            FirstPeopleStuff();
        }

        void FirstPeopleStuff()//Вещи, связанные с первыми людьми
        {
            if(peopleToDieNormal < 0)
            {
                peopleToDieNormal = 0;
            }

            if(WorldPopulation == 0)
            {
                return;
            }

            if (timerOfDeath == 50)//Если таймер равен 50, то старые люди умирают
            {
                WorldPopulation = WorldPopulation - peopleToDieNormal;
                peopleToDieNormal = 0;
                timerOfDeath = 0;
            }

            if (timerOfDeath == 0)//Инициализируем новых старых людей
            {
                peopleToDieNormal = WorldPopulation;
            }
            if(timerOfBirth == 16)
            {
                WorldPopulation = WorldPopulation + (peopleToDieNormal / 2);//Формула размножения: Старые люди, разделённые на два
                timerOfBirth = 0;
            }
        }

        public void AddPeople()
        {
            WorldPopulation = WorldPopulation + 2;
        }
    }

    public class Village
    {

    }

}
