using System.Linq;

namespace atm
{
    //https://www.youtube.com/watch?v=qBI7Qnz9Zho&t=759s
    // creo la classe cardHolder che conterra' ogni utente della banca
    public class CardHolder
    {
        private string cardNum;
        int pin;
        string firstName;
        string lastName;
        double balance;

        // questo e' un costruttore, permette di creare degli oggetti assegnandogli direttamente dei valori
        public CardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
        {
            this.cardNum = cardNum;
            this.pin = pin;
            this.firstName = firstName;
            this.lastName = lastName;
            this.balance = balance;
        }
        public string getCardNum
        {
            get { return cardNum; }
            set { cardNum = value; }
        }

        public int getPin
        {
            get { return pin; }
            set { pin = value; }
        }

        public string getfirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string getLastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public double getBalance
        {
            get { return balance; }
            set { balance = value; }
        }

    }

    
    internal class Program
    {
        static void printOptions(CardHolder currentUser)
        {
            Console.Clear();
            Console.WriteLine("Welcome {0} {1}", currentUser.getfirstName, currentUser.getLastName);
            Console.WriteLine("Please choose from one of the following options...\n");
            Console.WriteLine("1. Deposit                          4. Create new bank account");
            Console.WriteLine("2. Withdraw                         5. Change account");
            Console.WriteLine("3. Show balance                     6. Exit");
        }

        static void deposit(CardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to deposit?");
            double deposit = double.Parse(Console.ReadLine());
            currentUser.getBalance += deposit;
            Console.WriteLine("You now have {0}$ in your bank", currentUser.getBalance);
        }

        static void withdraw(CardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to withdraw?");
            double withdrawl = double.Parse(Console.ReadLine());
            // check if user has enough money
            if(withdrawl > currentUser.getBalance)
                Console.WriteLine("Insufficient money!");
            else
            {
                currentUser.getBalance -= withdrawl;
                Console.WriteLine("You now have {0}$ in your bank", currentUser.getBalance);
            }
        }

        static void balance(CardHolder currentUser)
        {
            Console.WriteLine("You now have {0}$ in your bank", currentUser.getBalance);
        }

        static void createUser(List<CardHolder> users)
        {
            Console.Write("Insert the pin of the card(10 numbers): ");
            string newPin;
            do
            {
                try
                {
                    newPin = Console.ReadLine();
                    int.Parse(newPin);

                    if(newPin.Length == 10)
                        break;
                    else
                        Console.WriteLine("You must insert 10 numbers!");
                }
                catch(Exception) { Console.WriteLine("You must insert only numbers!"); }
            } while(true);

            Console.Write("Insert the pin of the card(4 numbers): ");
            int newCode;
            do
            {
                try
                {
                    newCode = int.Parse(Console.ReadLine());
                    string idk = newCode.ToString();
                    if(idk.Length == 4)
                        break;
                    else
                        Console.WriteLine("You must insert 4 numbers!");
                }
                catch(Exception) { Console.WriteLine("You must insert only numbers!"); }
            } while(true);

            Console.Write("Insert the first name: ");
            string newFirstName = Console.ReadLine();

            Console.Write("Insert the last name: ");
            string newLastName = Console.ReadLine();

            Console.Write("Insert the balance of the bank account: ");
            double newBalance;

            do
            {
                try
                {
                    newBalance = double.Parse(Console.ReadLine());
                    break;
                }
                catch(Exception) { Console.WriteLine("You must insert only numbers!"); }
            } while(true);

            users.Add(new CardHolder(newPin, newCode, newFirstName, newLastName, newBalance));
            Console.WriteLine("Account created successfully!");
        }

        static void Main(string[] args)
        {
            // creo il data base che conterra' gli utenti iscritti nella banca

            CardHolder changeUser(List<CardHolder> users)
            {
                // chiedo all'utente di inserire il numero della carta
                Console.Write("Please insert your debit card number: ");
                string debitCardNum = "";
                // creo la variabile currentUser di tipo CardHolder che conterra' l'utente che corrisponde al numero di carta inserito
                CardHolder currentUser;

                // creo un ciclo infinito finche' l'utente non inserisce un numero di carta valido
                while(true)
                {
                    // utilizzo try perche' l'utente e' stupido e potrebbe non inserire una string(?)
                    debitCardNum = Console.ReadLine();
                    // check in our db (Data Base) --> our list of users
                    // letteralmente la funzione migliore del mondo, cicla in modo autonomo tutta la lista (db) finche' non trova un corrispondente al valore
                    // dentro le parentesi e quando lo trova assrga a currentUser tutti i dati che corrispondo a quell'oggetto
                    currentUser = users.FirstOrDefault(b => b.getCardNum == debitCardNum);
                    if(currentUser != null)
                        break;
                    else
                        Console.WriteLine("Card number not recognized. Please try again.");
                }

                // chiedo all'utente di inserire il suo pin
                Console.Write("Please enter your pin: ");
                int userPin = 0;
                // creo un ciclo infinito finche' l'utente non inserisce il pin che corrisponde al numero di carta che ha inserito prima
                while(true)
                {
                    // utilizzo try per non incorrere in errori in caso l'utente non inserisca solo numeri
                    try
                    {
                        userPin = int.Parse(Console.ReadLine());
                        if(currentUser.getPin == userPin)
                            break;
                        else
                            Console.WriteLine("You insert the wrong pin!");
                    }
                    catch { Console.WriteLine("You insert the wrong pin!"); }
                }
                return currentUser;
            }

            List<CardHolder> users = new List<CardHolder>();

            users.Add(new CardHolder("4710368259", 1234, "Emma", "Piccoli", 6969.69));
            users.Add(new CardHolder("2851306947", 9988, "Tiziano", "Blue", 10000.01));
            users.Add(new CardHolder("9316504827", 3516, "Capra", "Verdi", 5.50));

            // printo all'utente
            Console.WriteLine("----------------------");
            Console.WriteLine("|Welcome to simpleATM|");
            Console.WriteLine("----------------------");

            CardHolder currentUser = changeUser(users);

            // dentro questo do while c'e' tutto il programma finale!
            int opt = 0;
            do
            {
                printOptions(currentUser);
                while(true)
                    try
                    {
                        opt = int.Parse(Console.ReadLine());
                        if(opt == 1 || opt == 2 || opt == 3 || opt == 4 || opt == 5 || opt == 6)
                            break;
                        else
                            Console.WriteLine("You must insert a valid number (1, 2, 3, 4)!");
                    }
                    catch { Console.WriteLine("You must insert a number!"); }
                
                
                switch(opt)
                {
                    case 1:
                        deposit(currentUser);
                        opt = 0;
                        Console.ReadLine();
                        break;
                    case 2:
                        withdraw(currentUser);
                        opt = 0;
                        Console.ReadLine();
                        break;
                    case 3:
                        balance(currentUser);
                        opt = 0;
                        Console.ReadLine();
                        break;
                    case 4:
                        createUser(users);
                        opt = 0;
                        Console.ReadLine();
                        break;
                    case 5:
                        currentUser = changeUser(users);
                        opt = 0;
                        Console.ReadLine();
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("you entered an invalid number!");
                        opt = 0;
                        Console.ReadLine();
                        break;
                }
            } while(opt!=6);
        }
    }
}
