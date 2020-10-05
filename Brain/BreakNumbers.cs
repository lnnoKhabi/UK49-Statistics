using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain
{
    class BreakNumbers
    {
        public static int InSeven(List<string> n)
        {
            int Result = 0;

            n = n.OrderBy(x => Convert.ToInt32(x)).ToList();
            for (int a = 1; a < 44; a++)
            {
                for (int b = a + 1; b < 45; b++)
                {
                    for (int c = b + 1; c < 46; c++)
                    {
                        for (int d = c + 1; d < 47; d++)
                        {
                            for (int e = d + 1; e < 48; e++)
                            {
                                for (int f = e + 1; f < 49; f++)
                                {
                                    for ( int g = f + 1; g < 50; g++ )
                                    {
                                        Result++;
                                        if($"{a}{b}{c}{d}{e}{f}{g}" == $"{n[0]}{n[ 1 ]}{n[ 2 ]}{n[ 3 ]}{n[ 4 ]}{n[ 5 ]}{n[ 6 ]}" )
                                        {
                                            return Result;
                                        }
                                    }
                                }

                            }

                        }

                    }
                }
            }

            return Result;
        }
    }
}
