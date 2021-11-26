using System;

namespace Game{

    class Program{

        static void Main(){

            int [] cards = {11, 4, 6, 10, 11, 7, 8};
            byte PlayerNum = 1;
            byte i = 0; // Индекс массива колоды
            string [] nameCards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "Туз", "Валет", "Дама", "Король"};
            string NameCard (int a) => nameCards[a-2];
            bool Answer (string? str) => str == "y";
                      
            int Turn (int Player, int [] cards){
                int sum = 0;
                byte sumTus = 0, Tus = 0;
                if (cards[i] == 11) sumTus += 10;
                if (cards[i+1] == 11) sumTus += 10;
                sum = cards[i] + cards[i+1];
                if (sum > 21) sum -= sumTus;
                Console.WriteLine($"Игрок {Player}, тебе выпали карты {NameCard(cards[i])} и {NameCard(cards[i+1])}.\nИх сумма {sum}. Желаешь взять ещё карту? y/n");
                i += 2; // 3я карта в массиве от старта игрока
                while (Answer(Console.ReadLine())){
                    sum += cards[i];
                    if (cards[i] == 11) Tus += 10;
                    if (sum > 21 && sumTus == 10) {sum -= sumTus; sumTus = 0;}
                    if (sum > 21) {sum -= Tus; Tus = 0;}
                    Console.WriteLine($"Вот тебе еще карта: {NameCard(cards[i])}. Сумма карт {sum}. Ещё? y/n");  
                    i++;              
                }
                System.Console.WriteLine("Sum cards Player{0}: {1}" , PlayerNum, sum); //// строка для проверки
                return sum;
            }

            void Game (){
                Turn(PlayerNum, cards);
                PlayerNum++;
                Turn(PlayerNum, cards);
            }
            
            Game();

            
        }

    }

}