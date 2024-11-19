using System;
using System.Collections.Generic;

namespace DotNetCleanCode
{
    static class Program
    {
        static void Main(string[] args)
        {
            User firstUser = new User();
            BankAccount compte = new BankAccount();

            firstUser.SetLastname("Ziadi");
            firstUser.SetFirstname("Noé");
            firstUser.SetAge(15);
            firstUser.SetGenre(false);

            Console.WriteLine("L'age du client est de " + firstUser.GetAge());
            Console.WriteLine("L'age du client est de " + firstUser.GetGenre());
            
            if(compte.isAllowToOwnAccount(firstUser)){
                compte.GenerateIban();
                compte.AddSold(200);
                compte.RemoveSold(50);
                compte.SetOwner(firstUser);
                compte.DisplayDetails();
            }else{
                Console.WriteLine("Les caractèristiques de votre profil ne vous permettes pas de créer un compte");
            }
        }
    }

    public class User
    {
        private string _lastname;
        private string _firstname;
        private int _age = 16;
        private string _genre;

        // Constructeur par défaut
        public User()
        {
            _lastname = "Inconnue";
            _firstname = "Inconnue";
            _age = 16;
            _genre = "Homme";
        }

        // Constructeur avec paramètres
        public User(string lastname, string firstname, int age, string genre)
        {
            _lastname = lastname;
            _firstname = firstname;
            _age = age >= 0 ? age : throw new ArgumentException("L'âge ne peut pas être négatif.");
            _genre = genre;
        }

        // Setters
        public void SetLastname(string lastname) => _lastname = lastname;
        public void SetFirstname(string firstname) => _firstname = firstname;

        public void SetAge(int age)
        {
            if (age < 0) throw new ArgumentException("L'âge ne peut pas être négatif.");
            _age = age;
        }

       

        public void SetGenre(bool isMale)
        {
            _genre = isMale ? "Homme" : "Femme";
        }

        // Getters
        public string GetLastname()
        {
            return _lastname;
        }

        public string GetFirstname()
        {
            return _firstname;
        }

        public int GetAge()
        {
            return _age;
        }

        public string GetGenre()
        {
            return _genre;
        }
    }

    public class BankAccount
    {
        private User _owner;
        private int _sold;
        private string _iban;

        public BankAccount()
        {
            _owner = new User();
            _iban = "FRXXXXXXXX";
            _sold = 100;
        }

        public BankAccount(User owner, string iban, int sold)
        {
            _owner = owner;
            _iban = iban;
            _sold = sold;
        }

        public void SetOwner(User owner) => _owner = owner;
        public void SetIban(string code) => _iban = code;

        public string GenerateIban()
        {
            int charNumber = 10;
            List<string> alphabet = new List<string>
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            Random rdn = new Random();
            _iban = "FR";

            for (int i = 0; i < charNumber; i++)
            {
                _iban += alphabet[rdn.Next(alphabet.Count)];
            }

            return _iban;
        }

         public static bool isAllowToOwnAccount(User user)
        {
            return (user.GetAge() >= 16 || user.GetGenre() == "Homme");
        }

        public void AddSold(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Le montant ajouté doit être positif.");
            _sold += amount;
        }

        public void RemoveSold(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Le montant retiré doit être positif.");
            if (amount > _sold) throw new InvalidOperationException("Fonds insuffisants.");
            _sold -= amount;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Propriétaire : {_owner.GetFirstname()} {_owner.GetLastname()}");
            Console.WriteLine($"IBAN : {_iban}");
            Console.WriteLine($"Solde : {_sold} €");
        }
    }
}
