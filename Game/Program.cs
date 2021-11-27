using System;

namespace Game{

    class Program{

        static void Main(){

            int [] cards = {11, 13, 14, 12, 11, 13, 8};
            byte PlayerNum = 1;
            byte i = 0; // Индекс массива колоды
            string [] nameCards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Туз", "Валет", "Дама", "Король"};
            string NameCard (int a) => nameCards[a-2];   // Метод определяет название карты
            bool Answer (string? str) => str == "y";        // Метод возвращает TRUE если игрок согласен
            void Print (string str) {Console.WriteLine(str);}       // Достало писать каждый раз cw и удалять System
                      
            int Turn (int Player, int [] cards){
                int sumTus = 0, Tus = 0, officer = 0;
                if (cards[i] > 11) officer = cards[i] - 10;
                if (cards[i+1] > 11) officer += cards[i+1] - 10;
                int sum = cards[i] + cards[i+1] - officer;
                officer = 0;
                if (cards[i] == 11) sumTus += 10;
                if (cards[i+1] == 11) sumTus += 10;
                if (sum > 21) sum -= sumTus;
                
                Print($"Игрок {Player}, тебе выпали карты {NameCard(cards[i])} и {NameCard(cards[i+1])}.\nИх сумма {sum}. Желаешь взять ещё карту? y/n");
                i += 2; // 3я карта в массиве от старта игрока
                while (Answer(Console.ReadLine())){
                    if (cards[i] > 11) officer = cards[i] - 10;
                    sum += cards[i] - officer;
                    officer = 0;
                    if (cards[i] == 11) Tus += 10;
                    if (sum > 21 && sumTus == 10) {sum -= sumTus; sumTus = 0;}
                    if (sum > 21) {sum -= Tus; Tus = 0;}
                    Print($"Вот тебе еще карта: {NameCard(cards[i])}. Сумма карт {sum}. Ещё? y/n");  
                    i++;              
                }
                Print($"Sum cards Player {PlayerNum}: {sum}"); //// строка для проверки
                return sum;
            }

            void Game (){
                Turn(PlayerNum, cards);
                PlayerNum++;
                Console.Clear();
                Turn(PlayerNum, cards);
                Console.Clear();
            }
            
            Game();

            
        }

    }

}