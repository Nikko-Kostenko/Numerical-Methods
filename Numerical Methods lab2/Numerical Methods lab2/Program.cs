using System;
using System.ComponentModel.DataAnnotations;


namespace Numerical_Methods_lab2
{
    class Program
    {

        static void CofficientsProg(int[,] arr, ref double[] ai, ref double[] bi)
        {
            for (int i = 0; i < 4; i++)
            {
                double y = i == 0 ? arr[0, 0] : arr[i, i] + arr[i, i - 1] * ai[i - 1];
                ai[i] = -1 * arr[i, i + 1] / y;
                bi[i] = i == 0 ? arr[i, 4] / y : (arr[i, 4] - arr[i, i - 1] * bi[i - 1]) / y;
            }
        }

        static double ExecProg(int i, double[] ai, double[] bi)
        {
            double ans =  i != 3 ? ai[i] * ExecProg(i + 1, ai, bi) + bi[i] : bi[i];
            
            Console.WriteLine(ans + " <-x" + (4 - i));
            return ans;
        }

        static bool JacobiCheck(int[,] arr)
        {
            for (int i = 0; i < 4; i++)
            {
                int sum = 0;
                for (int j = 0; j < 4; j++)
                {
                    sum += j == i ? 0 : arr[i, j];
                }

                if (arr[i, i] < sum)
                {
                    return false;
                }
            }

            return true;
        }

        static void Jacobi( int[,] arr, double err)
        {
            double IterErr = 10;
            double[] temp_x = new double[4];
            double[] x = new double[4];
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    temp_x[i] = arr[i, 4];
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i != j)
                        {
                            temp_x[i] -= Convert.ToDouble(arr[i, j] * x[j]);
                        }
                    }

                    temp_x[i] = temp_x[i] / Convert.ToDouble(arr[i, i]);
                    double e = Math.Abs(x[i] - temp_x[i]);
                    IterErr = e < IterErr ? e : IterErr;
                    x[i] = temp_x[i];
                }
            } while (IterErr > err);


            Console.WriteLine();
            foreach (var VARIABLE in x)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        static void Main(string[] args)
        {
           Console.WriteLine("enter matrix");
           int[,] array = new int[4, 5];
           double[] a = new double[4];
           double[] b = new double[4];
           
 

            for (int i = 0; i < 4; i++)
            {
               string[] input = Console.ReadLine().Split(' ');
               for (int j = 0; j < 5; j++)
               {
                   array[i, j] = int.Parse(input[j]);
               }
            }

            CofficientsProg(array, ref a, ref b);
            Console.WriteLine("answers progonka:");
            ExecProg(0, a, b);


            if (JacobiCheck(array))
            {
                Console.WriteLine("results jacobi");
                Jacobi(array, 0.001);
            }
            else
            {
                Console.WriteLine("matrix cannot be solved with jacobi");
            }

        }
    }
}
