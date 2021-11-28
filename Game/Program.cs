using System;

namespace Game
{

    class Program
    {

        static void Main()
        {
            int[] deckOfCards =
            {2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6,
            7, 7, 7, 7, 8, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 11, 11,
            11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14};    // колода карт
            int[] mixedCards = Mixing(deckOfCards);     // Сортировка колоды
            byte PlayerNum = 1;
            int iCard = 0; // Индекс карты из массива колоды
            Game(PlayerNum, mixedCards, iCard);     // Запуск игры 
        }

        public static int[] Mixing(int[] array)
        {        // Метод тасует колоду
            int j, temporary;
            for (int i = 0; i < array.Length; i++)
            {
                temporary = array[i];
                j = new Random().Next(i, array.Length);
                array[i] = array[j];
                array[j] = temporary;
            }
            return array;
        }

        public static string NameCard(int a)
        {      // Метод определяет название карты
            string[] nameCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Туз", "Валет", "Дама", "Король" };
            return nameCards[a - 2];
        }

        public static bool Answer(string? str) => str == "y";        // Метод возвращает TRUE если игрок согласен

        public static void Print(string str) { Console.WriteLine(str); }       // Достало писать каждый раз cw и удалять System

        public static (int sum, int count) Turn(int Player, int[] cards, int iCard)
        {      // Метод подсчета очков (возвращает кортеж (сумма очков, индекс карты))
            var result = (sum: 0, count: iCard);
            int sumTus = 0, Tus = 0, officer = 0;
            int sumICard = iCard;
            if (cards[iCard] > 11) officer = cards[iCard] - 10;
            if (cards[iCard + 1] > 11) officer += cards[iCard + 1] - 10;
            result.sum = cards[iCard] + cards[iCard + 1] - officer;
            officer = 0;
            if (cards[iCard] == 11) sumTus += 10;
            if (cards[iCard + 1] == 11) sumTus += 10;
            if (result.sum > 21) result.sum -= sumTus;

            Print($"Игрок {Player}, тебе выпали карты {NameCard(cards[iCard])} и {NameCard(cards[iCard + 1])}.\nИх сумма {result.sum}. Желаешь взять ещё карту? y/n");
            iCard += 2; // 3я карта в массиве от старта игрока
            while (Answer(Console.ReadLine()))
            {
                if (cards[iCard] > 11) officer = cards[iCard] - 10;
                result.sum += cards[iCard] - officer;
                officer = 0;
                if (cards[iCard] == 11) Tus += 10;
                if (result.sum > 21 && sumTus == 10) { result.sum -= sumTus; sumTus = 0; }
                if (result.sum > 21) { result.sum -= Tus; Tus = 0; }
                Print($"Вот тебе еще карта: {NameCard(cards[iCard])}. Сумма карт {result.sum}. Ещё? y/n");
                iCard++;
            }
            result.count = iCard;
            Print($"Sum cards Player {Player}: {result.sum}"); //// строка для проверки
            return result;
        }
        public static void PrintWinner(int[] score) //метод печатает победителя, принимает массив с очками игроков
        {
            int max = 0;
            int winner = -1;
            for (int i = 0; i < score.Length; i++)
            {
                if (score[i] > max && score[i] <= 21) max = score[i];
                winner = i + 1;
            }
            if (max == 0) Console.WriteLine("Ничья!");
            else Console.WriteLine($"Победил игрок {winner}, набрав {max} очков");
        }
        public static void Game(int PlayerNum, int[] mixedCards, int iCard)
        {      // Метод игры для n-x игроков 

            (int sum, int count) countCard = (0, 0);
            int n = 3;
            int[] score = new int[n];
            while (PlayerNum <= n)
            {
                countCard = Turn(PlayerNum, mixedCards, iCard);
                iCard = countCard.count;
                Print("Использовано карт в игре: " + iCard.ToString());
                score[PlayerNum-1] = countCard.sum;
                PlayerNum++;
            }
            PrintWinner(score);
        }

    }

}
