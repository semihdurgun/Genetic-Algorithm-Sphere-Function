using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class GenetikHesaplamalar
    {
        public List<Canli> canliList { get; set; }
        public List<Canli> elitList { get; set; }
        public int elitPop { get; set; }

        public List<Canli> populasyonList
        {
            get
            {
                List<Canli> list = new List<Canli>();
                list.AddRange(canliList);
                if (elitList != null)
                    list.AddRange(elitList);
                return list;
            }
        }
        public GenetikHesaplamalar(int pop)
        {
            PopulasyonOlustur(pop);
        }

        private Canli Kiyasla(Canli c1, Canli c2)
        {
            Canli c = new Canli();
            c = c1.Gen.SphereFormul > c2.Gen.SphereFormul ? c2 : c1;
            return c;
        }
        public List<Canli> PopulasyonOlustur(int pop)
        {
            List<Canli> liste = new Canli().Olustur(pop);
            canliList = liste;
            return liste;
        }
        public List<Canli> TurnuvaCiftiOlustur()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> TurnuvaList = new List<Canli>();
            for (int i = 0; i < canliList.Count; i++)
            {
                int rndIndis1, rndIndis2;
                rndIndis1 = rnd.Next(0, canliList.Count);
                rndIndis2 = rnd.Next(0, canliList.Count);
                var v1 = canliList[rndIndis1];
                var v2 = canliList[rndIndis2];
                TurnuvaList.Add(Kiyasla(v1, v2));


                rndIndis1 = rnd.Next(0, canliList.Count);
                rndIndis2 = rnd.Next(0, canliList.Count);
                v1 = canliList[rndIndis1];
                v2 = canliList[rndIndis2];
                TurnuvaList[i].TurnuvaCifti = Kiyasla(v1, v2);
            }
            canliList = TurnuvaList;
            return TurnuvaList;
        }
        public List<Canli> Caprazla(double şanslımı)
        {
            
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> caprazlanmisList = new List<Canli>();

            foreach (var canli in canliList)
            {
                if (rnd2.NextDouble() > şanslımı)
                {
                    caprazlanmisList.Add(canli);
                    continue;
                }

                double rndDouble = rnd.NextDouble();
                double x1x = rndDouble * canli.Gen.x1 + (1-rndDouble) * canli.TurnuvaCifti.Gen.x1;
                double x1y = rndDouble * canli.Gen.x2 + (1-rndDouble)* canli.TurnuvaCifti.Gen.x2;

               
                double x2x = (1-rndDouble) * canli.Gen.x1 + rndDouble * canli.TurnuvaCifti.Gen.x1;
                double x2y = (1-rndDouble) * canli.Gen.x2 + rndDouble * canli.TurnuvaCifti.Gen.x2;

                caprazlanmisList.Add(new Canli()
                {
                    Gen = new Gen()
                    {
                        x1=x1x,
                        x2=x1y
                    },
                    TurnuvaCifti = new Canli()
                    {
                        Gen = new Gen()
                        {
                            x1=x2x,
                            x2=x2y
                        }
                    }
                });
            }
            canliList = caprazlanmisList;
            return caprazlanmisList;
        }
        public List<Canli> Mutasyon(double şanslımı)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> mutasyonList = new List<Canli>();

            foreach (var canli in canliList)
            {
                if (rnd.NextDouble() > şanslımı)
                {
                    mutasyonList.Add(canli);
                    continue;
                }

                mutasyonList.Add(new Canli()
                {
                    Gen = new Gen(),
                    TurnuvaCifti = new Canli() { Gen = new Gen() }
                });
            }

            canliList = mutasyonList;
            return mutasyonList;
        }
        public Canli EniyiCanli()
        {
            var c = populasyonList.OrderBy(a => a.Gen.SphereFormul).FirstOrDefault();
            Console.WriteLine("En iyisi Canlı :" + c.Gen.SphereFormul);
            return c;
        }
        public List<Canli> Elitizm(int elitPop)
        {
            List<Canli> elitizm = populasyonList.OrderBy(a => a.Gen.SphereFormul).Take(elitPop).ToList();
            canliList = populasyonList.OrderBy(a => a.Gen.SphereFormul).Reverse().Take(populasyonList.Count() - elitPop).ToList();
            elitList = elitizm;
            Console.WriteLine("En iyisi Fonksiyon :" + populasyonList.OrderBy(a => a.Gen.SphereFormul).FirstOrDefault().Gen.SphereFormul);
            return elitizm;
        }
        public List<Canli> Elitizm()
        {
            return Elitizm(elitPop);
        }
    }
}
