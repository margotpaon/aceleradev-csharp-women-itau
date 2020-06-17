using System;
using System.Linq;

namespace Codenation.Challenge.Services
{
    public class RandomService: IRandomService
    {
        public int RandomInteger(int max)
        {
            Random random = new Random();    
            return random.Next(max);
        }
    }
}