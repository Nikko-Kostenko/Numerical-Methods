// Numerical_Methods lab 4.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//
#include<iostream>
#include<iomanip>
#include<cmath>
#include<vector>
#include <string>
#include <cstring>
#include <iostream>
#include <fstream>
#include "gnuplot.h"
#define PI 3.14159265358979323846

using namespace std;

void drawGnuplot()
{
    GnuplotPipe gp;
    gp.sendLine("set border linewidth 1.5");
    gp.sendLine("set style line 1 lc rgb '#0060ad' pt 7 ps 1.5 lt 1 lw 2 # -- - blue");
    gp.sendLine("unset key");
    gp.sendLine("set style line 11 lc rgb '#808080' lt 1");
    gp.sendLine("set border 3 back ls 1");
    gp.sendLine("set border 3 back ls 1");
    gp.sendLine("set border 3 back ls 1");
    gp.sendLine("set tics nomirror out scale 0.75");
    gp.sendLine("set style line 12 lc rgb'#808080' lt 0 lw 1");
    gp.sendLine("set grid back ls 12");
    gp.sendLine("set xrange[-10:20]");
    gp.sendLine("set yrange[-10:30]");
    gp.sendLine("set key invert");
    gp.sendLine("set key right top");
    gp.sendLine("load 'func_values.txt'");
    
}

double func(double x)
{
    return pow(x, 2) * log10(x) - 1;
}

vector<double> Newton(const vector<double>& x, vector<double> f)
{
    int N = x.size();
    vector<double> c(N), temp(N);

    c[0] = f[0];
    for (int i = 1; i < N; i++)       
    {
        for (int j = 0; j < N - i; j++) temp[j] = (f[j + 1] - f[j]) / (x[j + i] - x[j]);
        f = temp;
        c[i] = f[0];
    }



    vector<double> a(N, 0.0);                   
    vector<double> p(N), prev(N);                 

    p[0] = 1;
    a[0] = c[0] * p[0];
    for (int i = 1; i < N; i++)
    {
        prev = p;
        p[0] = -x[i - 1] * prev[0];
        a[0] += c[i] * p[0];
        for (int j = 1; j <= i; j++)
        {
            p[j] = prev[j - 1] - x[i - 1] * prev[j];
            a[j] += c[i] * p[j];
        }
    }

    cout << "Newton Polinom: " << endl;

    std::ofstream out("func_values.txt", std::ios::out);
    if (out.is_open())
    {
        out << "plot 'nodes.txt' w p ls 1 title 'data', ";
        out << "(x**2)*log10(x)-1 title 'func', ";
        for (int i = 0; i < x.size(); i++)
        {
            if (i == x.size() - 1 && i != 0)
            {
                out << a[i] << "*x**" << i;
            }
            else if (i == 0 && i != x.size() - 1)
            {

                out << a[i] << "+";
            }
            else if (i == 0 && i == a.size() - 1) {
                out << a[i] << endl;
            }

            else {

                out << a[i] << "*x**" << i << "+";
            }
        }
        out << " title 'Newton'";
    }
    out.close();
    

    for (int i = 0; i < x.size(); i++)
    {
        if (i == x.size() - 1)
        {
            cout << "(" << a[i] << ")x^" << i << endl;
        }
        else {
            cout << "(" << a[i] << ")x^" << i << "+";
        }
    }

    return a;
}

double Func(vector<double> a, double x)
{
    double ans = 0;
	for(int i = 0; i < a.size(); i++)
	{
        ans += pow(x, i) * a[i];
	}
    return ans;
}

double dFunc(vector<double> a, double x)
{
    double ans = 0;
	for (int i = 1; i < a.size(); i++)
	{
        ans += pow(x, i - 1) * i * a[i];
	}
    return ans;
}

double NutonPol(vector<double> a)
{
    double x = 10;
    double temp_x = 0;
	for(int i = 0; 0.001 < Func(a, x); i++)
	{
        temp_x = x - Func(a, x) / dFunc(a, x);
        x = temp_x;
	}
    return  x;
}

vector<double> Chebushev_nodes(double a, double b, double n)
{
    vector<double> nodes;
	for (int i = 1; i <= n; i++)
	{
        nodes.push_back(0.5 * (a + b) + 0.5 * (b - a) * cos((2 * i - 1) * PI / (2*n)));
	}
    return nodes;
}

int main()
{
    double a, b;
    cout << "Enter first num:" << endl;
    cin >> a;
    cout << "Enter second num:" << endl;
    cin >> b;
    double num_points;
    cout << "Enter num points:" << endl;
    cin >> num_points;
    vector<double> nodes = Chebushev_nodes(a, b, num_points);
    vector<double> func_values;
    for (int i = 0; i < num_points; i++)
    {
        func_values.push_back(func(nodes[i]));
    }
    Newton(nodes, func_values);

    std::ofstream out("nodes.txt");
    if (out.is_open())
    {
        for (int i = 0; i < nodes.size(); i++)
        {
            out << nodes[i] << " " << func_values[i] << std::endl;
        }
    }
    out.close();
    double ans = NutonPol(Newton(nodes, func_values));
    cout <<"The root of Nuton polynom is:" << ans << endl;

   
	
    drawGnuplot();

    
    return 0;
}


