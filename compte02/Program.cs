using System;

namespace GestionFinanciere
{
    public abstract class Compte
    {
        public decimal Solde { get; protected set; }
        public string NumeroCompte { get; set; }

        public void Crediter(decimal montant)
        {
            Solde += montant;
        }

        public void Debiter(decimal montant)
        {
            if (Solde - montant >= 0)
                Solde -= montant;
            // Gestion d'erreurs pour le cas contraire pourrait être ajoutée ici.
        }

        public abstract decimal CalculerInteret();

        public abstract void AppliquerInteret();

        public string AfficherDetails()
        {
            return $"Numéro de compte: {NumeroCompte}, Solde: {Solde:C}";
        }
    }

    public class CompteCourant : Compte
    {
        public decimal TauxInteret { get; set; }

        public override decimal CalculerInteret()
        {
            return Solde * TauxInteret;
        }

        public override void AppliquerInteret()
        {
            Solde += CalculerInteret();
        }

        public void DecouvertAutorise(decimal montant)
        {
            // Cette méthode pourrait augmenter une propriété "DecouvertAutorise"
            // et influencer comment le solde est débité (i.e., permettre un solde négatif jusqu'à un certain montant)
        }
    }

    public class CompteEpargne : Compte
    {
        public decimal TauxInteret { get; set; }

        public override decimal CalculerInteret()
        {
            return Solde * TauxInteret;
        }

        public override void AppliquerInteret()
        {
            Solde += CalculerInteret();
        }

        public void RetirerFonds(decimal montant)
        {
            if (Solde - montant >= 0)  Solde -= montant;
            // Gestion d'erreurs pour le cas contraire pourrait être ajoutée ici.
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CompteCourant cc = new CompteCourant
            {
                NumeroCompte = "CC12345",
                TauxInteret = 0.01m
            };
            cc.Crediter(1000);
            cc.Debiter(200);
            cc.AppliquerInteret();
            Console.WriteLine(cc.AfficherDetails());

            CompteEpargne ce = new CompteEpargne
            {
                NumeroCompte = "CE12345",
                TauxInteret = 0.03m
            };
            ce.Crediter(3000);
            ce.RetirerFonds(1000);
            ce.AppliquerInteret();
            Console.WriteLine(ce.AfficherDetails());
        }
    }
}