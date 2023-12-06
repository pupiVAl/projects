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
            Console.WriteLine("Per favore scegli dalle opzioni disponibili...\n");
            Console.WriteLine("1. Deposita                      4. Crea nuovo account");
            Console.WriteLine("2. Ritira                        5. Cambia account");
            Console.WriteLine("3. Mostra bilancio               6. Esci");
        }

        // funzione che permette all'utente di depositare
        static void deposit(CardHolder currentUser)
        {
            double deposit;
            while(true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("DEPOSITA");
                    Console.WriteLine("Quanti soldi vorresti depositare?");
                    deposit = double.Parse(Console.ReadLine());
                    if(deposit > 0)
                        break;
                    else
                    {
                        Console.WriteLine("Devi inserire solo numeri positivi");
                        Thread.Sleep(2000);
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Devi inserire solo numeri!");
                    Thread.Sleep(1500);
                }
            }
            currentUser.ModBalance += deposit;
            Console.WriteLine("Adesso ci sono {0}$ nell'account", currentUser.ModBalance);
        }

        // funzione che permette di ritirare soldi all'utente
        static void withdraw(CardHolder currentUser)
        {
            double withdraw;
            while(true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("RITIRA");
                    Console.WriteLine("Quanti soldi vuoi ritirare?");
                    withdraw = double.Parse(Console.ReadLine());
                    if(withdraw > 0)
                    {
                        if(withdraw > currentUser.ModBalance)
                            Console.WriteLine("Soldi insufficienti!");
                        else
                        {
                            currentUser.ModBalance -= withdraw;
                            Console.WriteLine("Adesso ci sono {0}$ nell'account", currentUser.ModBalance);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Devi inserire un numero positivo!");
                        Thread.Sleep(2000);
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Devi inserire solo numeri!");
                    Thread.Sleep(1500);
                }
            }
        }

        // funzione che permette di vedere il bilancio dell'account
        static void balance(CardHolder currentUser)
        {
            Console.Clear();
            Console.WriteLine("BILANCIO\nAdesso ci sono {0}$ nell'account", currentUser.ModBalance);
        }

        // funzione che permette di creare un nuovo account
        static CardHolder createUser(List<CardHolder> users)
        {
            string newCardNum;
            do
            {
                Console.Clear();
                Console.WriteLine("CREA NUOVO ACCOUNT");
                Console.Write("Inserisci il numero della carta (10 numeri): ");
                try
                {
                    newCardNum = Console.ReadLine();
                    long.Parse(newCardNum);

                    if(newCardNum.Length == 10)
                        if(users.FirstOrDefault(b => b.ModCardNum == newCardNum) == null)
                            break;
                        else
                        {
                            Console.WriteLine("Questo numeri di carta esiste gia'!");
                            Thread.Sleep(1500);
                        }
                    else
                    {
                        Console.WriteLine("Devi inserire 10 numeri per il numero di carta!");
                        Thread.Sleep(1500);
                    }
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); Thread.Sleep(1500); }
            } while(true);

            int newPin;
            do
            {
                Console.Write("Inserisci il pin della carta (4 numeri): ");
                try
                {
                    newPin = int.Parse(Console.ReadLine());
                    if(newPin > 0)
                    {
                        if(newPin.ToString().Length == 4)
                            break;
                        else
                            Console.WriteLine("Devi inserire 4 numeri per il pin!");
                    }
                    else
                        Console.WriteLine("Devi inserire un numerpo positivo!");
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); }
            } while(true);

            Console.Write("Inserisci il nome: ");
            string newFirstName = Console.ReadLine();

            Console.Write("Inserisci il cognome: ");
            string newLastName = Console.ReadLine();

            double newBalance;
            do
            {
                Console.Write("Inserisci il bilancio dell'account: ");
                try
                {
                    newBalance = double.Parse(Console.ReadLine());
                    if(newBalance > 0)
                        break;
                    else
                    {
                        Console.WriteLine("Devi inserire un numero positivo!");
                        Thread.Sleep(1500);
                    }
                }
                catch(Exception) { Console.WriteLine("Devi inserire solo numeri!"); Thread.Sleep(1500); }
            } while(true);

            Console.WriteLine("Account creato con successo!");
            Thread.Sleep(1500);
            return (new CardHolder(newCardNum, newPin, newFirstName, newLastName, newBalance));
        }

        // funzione che permette di cambiare account su cui si operano le operazioni
        static CardHolder changeUser(List<CardHolder> users)
        {
            string debitCardNum;
            // creo la variabile currentUser di tipo CardHolder che conterra' l'utente che corrisponde al numero di carta inserito
            CardHolder currentUser;
            // creo un ciclo infinito finche' l'utente non inserisce un numero di carta valido
            while(true)
            {
                Console.Clear();
                Console.WriteLine("CAMBIA ACCOUNT");
                Console.Write("Per favore inserisci il numero della carta: ");

                debitCardNum = Console.ReadLine();
                // check in our db (Data Base) --> our list of users
                // letteralmente la funzione migliore del mondo, cicla in modo autonomo tutta la lista (db) finche' non trova un corrispondente al valore
                // dentro le parentesi e quando lo trova assrga a currentUser tutti i dati che corrispondo a quell'oggetto
                currentUser = users.FirstOrDefault(b => b.ModCardNum == debitCardNum);
                if(currentUser != null)
                    break;
                else
                {
                    Console.WriteLine("Numero di carta non riconosciuto nel sistema!");
                    Thread.Sleep(2000);
                }
            }

            int userPin;
            // creo un ciclo infinito finche' l'utente non inserisce il pin che corrisponde al numero di carta che ha inserito prima
            while(true)
            {
                Console.Write("per favore inserisci il pin della carta: ");
                // utilizzo try per non incorrere in errori in caso l'utente non inserisca solo numeri
                try
                {
                    userPin = int.Parse(Console.ReadLine());
                    if(currentUser.ModPin == userPin)
                        break;
                    else
                        Console.WriteLine("Hai inserito il pin sbagliato!");
                }
                catch { Console.WriteLine("Devi inserire solo numeri!"); }
            }
            return currentUser;
        }

        static void Main(string[] args)
        {

            // creo il data base che conterra' gli utenti iscritti nella banca
            // per farlo utilizzo una lista di CardHolder, ogni elemento della lista conterra' un'elemento diverso
            List<CardHolder> users = new List<CardHolder>();
            // per comodita' pre-creo 3 account con tutte le informazioni del caso
            users.Add(new CardHolder("4710368259", 1234, "Emma", "Piccoli", 1024.50));
            users.Add(new CardHolder("0123456789", 1111, "Lavarris", "Steefhaanooo", 9000.90));
            users.Add(new CardHolder("2851306947", 9988, "Tiziano", "Blue", 960.50));
            users.Add(new CardHolder("9316504827", 3516, "Capra", "Verdi", 5.50));

            // chiedo all'utente se vuole loggare o creare un nuovo account
            char first_choice;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("------------------------");
                Console.WriteLine("|Benvenut* in simpleATM|");
                Console.WriteLine("------------------------");

                Console.Write("Vuoi fare un log in (L) oppure un sign in (S)? ");
                try
                {
                    first_choice = char.ToUpper(char.Parse(Console.ReadLine()));
                    if(first_choice == 'L' || first_choice == 'S')
                        break;
                    else
                    {
                        Console.WriteLine("Devi inserire 'L' oppure 'S'");
                        Thread.Sleep(1500);
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Devi inserire un solo carattere!");
                    Thread.Sleep(1500);
                }
            }
            CardHolder currentUser;

            if(first_choice == 'S')
            {
                Console.Clear();
                Console.WriteLine("SIGN IN");
                currentUser = createUser(users);
                users.Add(currentUser);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("LOG IN");
                currentUser = changeUser(users);
            }

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
                            Console.WriteLine("Devi inserire un numero valido (1, 2, 3, 4, 5, 6)!");
                    }
                    catch { Console.WriteLine("Devi inserire solo numeri!"); }

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
