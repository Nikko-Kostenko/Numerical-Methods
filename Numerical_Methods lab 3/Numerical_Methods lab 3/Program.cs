using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Numerical_Methods_lab_3
{
    class Program
    {

       static double FunkSys(double[] x, int i) => i switch //функции
        {
            0 => 3 * Math.Pow(x[0], 2) + 1.5 * Math.Pow(x[1], 2) + Math.Pow(x[2], 2) - 5,
            1 => 6 * x[0] * x[1] * x[2] - x[0] + 5 * x[1] + 3 * x[2],
            2 => 5 * x[0] * x[1] - x[1] * x[2] - 1,
            _ => throw new ArgumentOutOfRangeException(nameof(i), $"non expected value of func"),
        };

       static double XiFunctSys(double[] x, int i) => i switch
       {
           0 => Math.Pow((-1)*(Math.Pow(x[1], 2) + Math.Pow(x[2], 2) - 5), 0.5)/3,
           1 => (-1)*(6 * x[0] * x[1] * x[2] - x[0] + 3 * x[2])/5,
           2 => 1/(5*x[0]-x[1]),
           _ => throw new ArgumentOutOfRangeException(nameof(i), $"non expected value of xi"),
       };

      static double JacobiFunkSys(double[] x, int i, int j) //функции частных производных
      {
            switch (i)
            {
                case 0:
                    switch (j)
                    {
                        case 0:
                            return 6 * x[0];
                        break;
                        case 1:
                            return 3 * x[1];
                        break;
                        case 2:
                            return 2 * x[2];
                        break;
                    }
                    break;
                case 1:
                    switch (j)
                    {
                        case 0:
                            return 6 * x[1] * x[2] - 1;
                        break;
                        case 1:
                            return 6 * x[0] * x[2] + 5;
                        break;
                        case 2:
                            return 6 * x[0] * x[1] + 3;
                        break;
                    }
                    break;
                case 2:
                    switch (j)
                    {
                        case 0:
                            return 5 * x[2];
                        break;
                        case 1:
                            return (-1) * x[2];
                        break;
                        case 2:
                            return 5 * x[0] - x[1];
                        break;
                    }
                    break;
            }
            throw new ArgumentOutOfRangeException(nameof(i), $"non expected num of func");
      }

      static bool SimpleIterCheck(double[] x)
      {
          for (int i = 0; i < 3; i++)
          {
              double sum = 0;
              for (int j = 0; j < 3; j++)
              {
                  sum += Math.Abs(JacobiFunkSys(x, i, j));
              }

              if (sum >= 1) return false;
          }
          return true;
      }

     static void SimpleIter(ref double[] x)
      {
          double[] temp_x = x;
          double err = 0;
          do
          {
              for (int i = 0; i < x.Length; i++)
              {
                  temp_x[i] = XiFunctSys(x, i);
                  double e = Math.Abs(temp_x[i] - x[i]);
                  err = e > err ? e : err;
                  x[i] = temp_x[i];
              }
          } while (err > 0.001);
      }

       

        static void Main(string[] args)
        {
            double[] x = {  };

            if (SimpleIterCheck(x))
            {
                SimpleIter(ref x);
            }


            Console.WriteLine("results:");
            foreach (var VARIABLE in x)
            {
                Console.WriteLine(VARIABLE);
            }
        }   
    }
}
