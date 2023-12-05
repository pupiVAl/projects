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
            // this.NameVar permette di non fare confusione all'interno del costruttore quando entrambe le var hanno lo stesso nome
            this.cardNum = cardNum;
            this.pin = pin;
            this.firstName = firstName;
            this.lastName = lastName;
            this.balance = balance;
        }
        // queste funzioni permettono allo stesso tempo di restituire e modificare il valore della variabile dell'oggetto
        public string ModCardNum
        {
            get { return cardNum; }
            set { cardNum = value; }
        }

        public int ModPin
        {
            get { return pin; }
            set { pin = value; }
        }

        public string ModFirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string ModLastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public double ModBalance
        {
            get { return balance; }
            set { balance = value; }
        }

    }

    internal class Program
    {
        // funzione che stampa le opzioni all'utente
        static void printOptions(CardHolder currentUser)
        {
            Console.Clear();
            Console.WriteLine("Benvenut* {0} {1}", currentUser.ModFirstName, currentUser.ModLastName);
            Console.WriteLine("Per favore scegli uno delle seguenti azioni...\n");
            Console.WriteLine("1. Deposita                          4. Crea nuovo account");
            Console.WriteLine("2. Ritira                            5. Cambia account");
            Console.WriteLine("3. Mostra bilancio                   6. Esci");
        }

        // funzione che permette all'utente di depositare
        static void deposit(CardHolder currentUser)
        {
            Console.WriteLine("Quanti soldi vorresti depositare? ");
            double deposit = double.Parse(Console.ReadLine());
            currentUser.ModBalance += deposit;
            Console.WriteLine("Adesso hai {0}$ nel tuo conto", currentUser.ModBalance);
        }

        // funzione che permette di ritirare soldi all'utente
        static void withdraw(CardHolder currentUser)
        {
            Console.WriteLine("Quanti soldi vorresti ritirare? ");
            double withdrawl = double.Parse(Console.ReadLine());
            // check if user has enough money
            if(withdrawl > currentUser.ModBalance)
                Console.WriteLine("Soldi insufficienti! ");
            else
            {
                currentUser.ModBalance -= withdrawl;
                Console.WriteLine("Adesso hai {0}$ nel tuo conto", currentUser.ModBalance);
            }
        }

        // funzione che permette di vedere il bilancio dell'account
        static void balance(CardHolder currentUser)
        {
            Console.WriteLine("Hai {0}$ nel tuo conto", currentUser.ModBalance);
        }

        // funzione che permette di creare un nuovo account
        static void createUser(List<CardHolder> users)
        {
            Console.Write("Inserisci il numero di carta (10 numeri): ");
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
                        Console.WriteLine("Devi inserire 10 numeri!");
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); }
            } while(true);

            Console.Write("Inserisci il pin della carta (4 numeri): ");
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
                        Console.WriteLine("Devi inserire 4 numeri!");
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); }
            } while(true);

            Console.Write("Inserisci il nome: ");
            string newFirstName = Console.ReadLine();

            Console.Write("Inserisci il cognome: ");
            string newLastName = Console.ReadLine();

            Console.Write("inserisci il bilancio dell'account: ");
            double newBalance;

            do
            {
                try
                {
                    newBalance = double.Parse(Console.ReadLine());
                    break;
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); }
            } while(true);

            users.Add(new CardHolder(newPin, newCode, newFirstName, newLastName, newBalance));
            Console.WriteLine("Account creato senza problemi!");
        }

        // funzione che permette di cambiare account su cui si operano le operazioni
        static CardHolder changeUser(List<CardHolder> users)
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
                currentUser = users.FirstOrDefault(b => b.ModCardNum == debitCardNum);
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
                    if(currentUser.ModPin == userPin)
                        break;
                    else
                        Console.WriteLine("You insert the wrong pin!");
                }
                catch { Console.WriteLine("You insert the wrong pin!"); }
            }
            return currentUser;
        }

        static void Main(string[] args)
        {
            // creo il data base che conterra' gli utenti iscritti nella banca
            // per farlo utilizzo una lista di CardHolder, ogni elemento della lista conterra' un'elemento diverso
            List<CardHolder> users = new List<CardHolder>();

            // per comodita' pre-creo 3 account con tutte le informazioni del caso
            users.Add(new CardHolder("4710368259", 1234, "Emma", "Piccoli", 6969.69));
            users.Add(new CardHolder("2851306947", 9988, "Tiziano", "Blue", 10000.01));
            users.Add(new CardHolder("9316504827", 3516, "Capra", "Verdi", 5.50));

            // stampo all'utente
            Console.WriteLine("------------------------");
            Console.WriteLine("|Benvenut* in simpleATM|");
            Console.WriteLine("------------------------");

            CardHolder currentUser = changeUser(users);

            // dentro questo do while c'e' tutto il programma finale!
            // dentro la variabile opt contengo la scelta dell'utente fra quelle proposte
            int opt = 0;
            do
            {
                printOptions(currentUser);
                // uso un while infinito finche' l'utente non digita un'opzione possibile tra quelle proposte
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

                // uso lo switch per decidere quale funzione far partire in base alla scelta dell' utente
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
                        // non c'e' bisogno del ramo default perche' controllo gia' prima che l'utente scelta solo numeri contemplati

                }
                // quando l'utente digita '6', cioe' il numero che corrisponde all'uscita del programma fermo il loop
            } while(opt != 6);
        }
    }
}
