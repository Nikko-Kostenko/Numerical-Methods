using System;

namespace Numerical_Methods_lab_3_2_
{
    class Program
    {
        static double FunkSys(double[] x, int i) => i switch //функции
        {
            0 => 3 * Math.Pow(x[0], 2) + 1.5 * Math.Pow(x[1], 2) + Math.Pow(x[2], 2) - 5,
            1 => 6 * x[0] * x[1] * x[2] - x[0] + 5 * x[1] + 3 * x[2],
            2 => 5 * x[0] * x[2] - x[1] * x[2] - 1,
            _ => throw new ArgumentOutOfRangeException(nameof(i), $"non expected value of func"),
        };

        static double JacobiFunkSys(double[] x, int i, int j) 
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

      static  double MatrixDeterminant3(double[,] m)
      {
            return m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) -
                   m[1, 0] * (m[0, 1] * m[2, 2] - m[0, 2] * m[2, 1]) +
                   m[2, 0] * (m[0, 1] * m[1, 2] - m[1, 1] * m[0, 2]);

      }

       static double[,] MatrixT(double[,] m)
        {
            double [,] mT = new double[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mT[i, j] = m[j, i];
                }
            }

            return mT;
       }

       static double MatrixDeterminant2(double a1, double a2, double b1, double b2)
       {
           return a1 * b2 - a2 * b1;
       }

       static double[,] IvertibleMatrix(double[,] m)
       { 
           double matrixDet = MatrixDeterminant3(m);
           double[,] mI = new double[3, 3];
           mI[0, 0] = MatrixDeterminant2(m[1,1], m[1,2],m[2,1], m[2,2]);
           mI[1, 0] = (-1) * MatrixDeterminant2(m[0, 1], m[0, 2], m[2, 1], m[2, 2]);
           mI[0, 1] = (-1) * MatrixDeterminant2(m[1, 0], m[2, 0], m[1, 2], m[2, 2]);
           mI[1, 1] = MatrixDeterminant2(m[0, 0], m[0, 2], m[2, 0], m[2, 2]);
           mI[1, 2] = (-1) * MatrixDeterminant2(m[0, 0], m[0, 1], m[2, 0], m[2, 1]);
           mI[2, 1] = (-1) * MatrixDeterminant2(m[0, 0], m[1, 0], m[0, 2], m[1, 2]);
           mI[2, 2] = MatrixDeterminant2(m[0, 0], m[0, 1], m[1, 0], m[1, 1]);
           mI[0, 2] = MatrixDeterminant2(m[1, 0], m[1, 1], m[2, 0], m[2, 1]);
           mI[2, 0] = MatrixDeterminant2(m[0, 1], m[0, 2], m[1, 1], m[1, 2]);

           for (int i = 0; i < 3; i++)
           {
               for (int j = 0; j < 3; j++)
               {
                   mI[i,j] = mI[i,j] / matrixDet;
               }
           }

           return mI;
       }


        static void Main(string[] args)
        {
            //nums enter
            double[] x = {0.12, 1.3, -1.57};
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(x[i] + " " + FunkSys(x, i));
            }
            Console.WriteLine();

            double[,] JacobiMatrix = new double[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    JacobiMatrix[i, j] = JacobiFunkSys(x, i, j);
                }
            }
            double[] temp_x = new double[3];
            double err = 10;
            double[,] invertedJacobi = IvertibleMatrix(MatrixT(JacobiMatrix));
            do
            {
                for (int i = 0; i < 3; i++)
                {
                    double delta = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        delta += invertedJacobi[i, j] * FunkSys(x, j);
                        temp_x[j] = x[j] - delta;
                        Console.WriteLine("delta = " + delta);
                        Console.WriteLine(temp_x[j]);
                        err = Math.Abs(delta) < err ? Math.Abs(delta) : err;
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        x[j] = temp_x[j];
                    }
                    Console.WriteLine();
                }
            } while (err > 0.00001);
            Console.WriteLine("results");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine( x[i] + " " + FunkSys(x, i));
            }

        }
    }
}
