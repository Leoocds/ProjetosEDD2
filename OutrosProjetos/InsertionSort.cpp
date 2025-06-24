#include <iostream>
using namespace std;

void insertion_sort(int t, int *a)
{
    int key, j;
    for (int i = 1; i < t; i++)
    {
        key = a[i];
        j = i - 1;
        
        while (j >= 0 && a[j] > key)
        {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = key;

        for (int k = 0; k < t; k++)
        {
            cout << a[k] << " ";
        }
        cout << endl;
    }
}

void print(int t, int *a)
{
    for (int i = 0; i < t; i++)
    {
        cout << "Elemento " << i << " = " << a[i] << endl;
    }
    cout << "----------------" << endl;
}

int main(int argc, char** argv)
{
    int v[8] = {49, 38, 58, 87, 34, 93, 26, 13};
    print(8, v);
    insertion_sort(8, v);
    print(8, v);
    
    return 0;
}
