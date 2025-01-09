using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class HandlerMeow
    {
        public static void MakeThemMeow(IEnumerable<IMeowing> meowingObjects)
        {
            foreach (var meow in meowingObjects)
            {
                meow.Meow();
            }
        }
    }
}
