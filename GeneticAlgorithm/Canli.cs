using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class Canli
    {
        public Canli TurnuvaCifti { get; set; }
        public Gen Gen { get; set; }

        public List<Canli> Olustur(int populasyon)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());  //Hash int tipinde sayısal ve unique'tir. Unique olduğundan nesnelerin ve değişkenlerin anahtarıdır.
            List<Canli> pop = new List<Canli>();
            for (int i = 0; i < populasyon; i++)
            {
                pop.Add(new Canli() { Gen = new Gen() });
            }
            return pop;
        }
    }
}
