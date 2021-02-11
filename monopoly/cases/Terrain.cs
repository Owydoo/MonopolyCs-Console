using System;
using System.Threading;

namespace monopoly
{
    public class Terrain : Case
    {
        public uint prixDepart;
        private uint loyer;
        private Couleur couleur;
        public Joueur proprietaire;

        public EtatTerrain etat;

        public Terrain(string nom, uint prixDepart, uint loyer, Couleur couleur, Plateau plateau)
        {
            this.nom = nom;
            this.prixDepart = prixDepart;
            this.loyer = loyer;
            this.couleur = couleur;
            this.etat = new EtatAchetable();
            //=========================== TEST
            //this.etat = new EtatConstructible();

            //this.proprietaire = new Joueur();
            //=============================

            this.plateau = plateau;
        }



        public override void PasserSur(Joueur j)
        {
            Console.WriteLine($"Passage sur : {nom}");
            Thread.Sleep(1000);
        }
        public override void StopperSur(Joueur j)
        {
            Console.WriteLine($"Arret sur : {nom}");
            etat.StopperSur(j, this);
        }
        public void Construire(Joueur j)
        {
            etat.Construire(j, this);
        }

        public void AcheterTerrain(Joueur j)
        {
            etat.AcheterTerrain(j, this);
        }

        public void PayerLoyer(Joueur j)
        {
            etat.PayerLoyer(j, this);
        }

        public Couleur GetCouleur()
        {
            return couleur;
        }

        public bool PossedeePar(Joueur j)
        {
            return proprietaire == j;
        }

        public int CalculerLoyer()
        {
            int nb = (int)loyer;

            //loyer doubl� si le joueur poss�de tous les terrains de la couleur
            if(plateau.VerifAutreTerrainPossedeGroupe(couleur, proprietaire, this)) {
                nb *= 2;
            }
            return nb;
        }

        /// <summary>
        /// Enregistre d'abord le joueur en tant que propri�taire.
        /// Si l'acheteur poss�de les autres terrains du groupe, alors
        /// l'�tat du terrain passe � constructible.
        /// </summary>
        /// <param name="j"></param>
        public void EnregistreAcheteur(Joueur j)
        {
            proprietaire = j;

            Console.WriteLine(plateau.VerifAutreTerrainPossedeGroupe(couleur, j, this));
            this.etat = new EtatAchete();
            
            plateau.SwitchConstructible(couleur);

        }

        public override string ToString()
        {
            string _propri = "";
            if (proprietaire == null)
            {
                _propri = "pas de propri�taire";
            }
            else
            {
                _propri = proprietaire.nom;
            }

            string result = $"nom : {nom}\n" +
                $"prix d'achat : {prixDepart}\n"+
                $"loyer de d�part : {loyer}\n" +
                $"couleur : {couleur}\n" +
                $"propri�taire : {_propri}\n" +
                $"�tat du terrain : {etat.ToString()}\n";
            return result;
        }
    }
}