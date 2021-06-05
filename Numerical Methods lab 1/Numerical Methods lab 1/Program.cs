using System;

namespace Numerical_Methods_lab_1
{
    class Program
    {
       static  double F(double x)
       {
            return Math.Pow(x, 2) + Math.Sin(x) - 12 * x - 0.25;
       }

       static double dF(double x) //min in left point
       {
            return 2 * x + Math.Cos(x) - 12;
       }

       static  double dF2(double x) //max = 2
       {
            return 2 - Math.Cos(x);
       }

       static void Nuton(double a, double b)
       {
           double x0 = b;
           double q = qF(a, b, b);
            analyzeNuton(a,b,q);
            int i = 1;
            double xn = x0 - (F(x0) / dF(x0));//first iteration
            while(Math.Abs(F(xn))>Math.Pow(10, -4))
            {
                Console.WriteLine(xn + " <- the x" + i + " value ");
                var temp = xn;
                xn = temp - F(temp) / dF(temp);
                i++;
            }
            Console.WriteLine(xn + " <- the x" + i + " value "); 
       }

       static void ModifiedNuton(double a, double b)
       {
           double x0 = b;
           double q = qF(a, b, b);
           int i = 1;
           double scoreddf = dF(x0);
           double xn = x0 - (F(x0) / scoreddf);//first iteration
           while (Math.Abs(F(xn)) > Math.Pow(10, -4))
           {
               
               var temp = xn;
               xn = temp - F(temp) / scoreddf;
               i++;
           }
           Console.WriteLine(xn + " <- the x  value ");
       }

       static double qF(double a, double b, double x)
       {
           double M = Math.Max(dF2(a), dF2(b));
           double m = a < b ? dF(a) : dF(b);
           double xa = -0.0226803;
           return Math.Abs( M * (x - xa) / (2 * m));
       }
       

       static void analyzeNuton(double a, double b, double q)
       {
           
           double k = Math.Log((Math.Pow(10, -4) / (b - a)), Math.E);
           double m = Math.Log(q, Math.E);
           double i = Math.Log2(1 + k / m);
           int ans = Convert.ToInt32(i) + 1;
           Console.WriteLine("The assumption number of iterations :" + ans);

       }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter first and second nums:");
            Program Lab = new Program();
            double a, b;
            string[] input = Console.ReadLine().Split(' ');
            a = double.Parse(input[0]);
            b = double.Parse(input[1]);
            
            Nuton(a, b);
            Console.WriteLine();
            ModifiedNuton(a,b);
        }
    }
}
