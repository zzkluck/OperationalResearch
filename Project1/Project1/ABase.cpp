#include<iostream>
#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<time.h>
using namespace std;

class ABase {
public:
	virtual void print() {
		cout << "this is base" << endl;
	}
};

class A : public ABase {
public:
	void print() {
		cout << "this is not base" << endl;
	}
};

//int main() {
//	A a;
//	ABase base1 = a;
//	ABase base2 = (ABase)a;
//	ABase base3 = (ABase&)a;
//	ABase* pBase = &a;
//
//	a.print();
//
//	base1.print();
//	base2.print();
//	base3.print();
//
//	pBase->print();
//
//	getchar();
//}


int main()
{
	int i, a[6];
	printf("每个人的密码依次是：\n");
	srand((unsigned)time(NULL));
	for (i = 0; i<6; i++)
	{
		a[i] = rand() % 5 + 1;
		printf("%2d", a[i]);
	}
	getchar();
	//return a[i];
}

