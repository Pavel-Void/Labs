using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class MeowTracker : IMeowing
    {
        IMeowing meowing;
        int meowCount;

        public MeowTracker(IMeowing meowing)
        {
            this.meowing = meowing;
            this.meowCount = 0;
        }

        public void Meow()
        {
            meowCount += 1;
            meowing.Meow();
        }

        public void Meow(int n)
        {
            meowCount += n;
            meowing.Meow(n);
        }

        public int GetMeowCount()
        {
            return meowCount;
        }
    }
}
