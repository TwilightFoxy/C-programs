#include <iostream>
#include <fstream>
using namespace std;

void main() {
	ifstream fin;
	fin.open("C:\\Users\\User\\Desktop\\prov.txt");
	if (fin.is_open())cout<<"Nice\n";
	double x1 = 0.0, x2 = 0.0, x3 = 0.0, y1 = 0.0, y2 = 0.0, y3 = 0.0, z = 0;
	string s1, s2, s3;
	while (z<70) {
		fin >> x1;
		y1 += x1;
		fin >> x2;
		y2 += x2;
		fin >> x3;
		y3 += x3;
		z++;
	}
	y1 /= z;
	y2 /= z;
	y3 /= z;
	cout << z << " " << y1 << " " << y2 << " " << y3;
}