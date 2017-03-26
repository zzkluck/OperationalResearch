#include<iostream>
#include<string>
#include<queue>
using namespace std;




#pragma region 一些暂时要藏起来的东西
#if 0
class Fruit
{
public:
	//Fruit() = default;
	//Fruit(const Fruit&) = delete;
	//Fruit(Fruit&&) = delete;
	virtual string GetColor()
	{
		return "A abstract fruit has no color";
	}
};

class Banna :public Fruit
{
public:
	string GetColor()override
	{
		return "Banna is yellow";
	}
};

class Apple :public Fruit
{
public:
	string GetColor()override
	{
		return "Apple is white or black, sometimes gold, and nowdays it has a red version.";
	}
};

class Person {};


int main() {
	Fruit f;
	cout << f.GetColor() << endl;

	Banna banana;
	Fruit fruit_Init_By_Banana = banana;
	cout << fruit_Init_By_Banana.GetColor() << endl;

	Fruit* pointerToFruit = &banana;
	cout << pointerToFruit->GetColor() << endl;

	Fruit& referenceToFruit = banana;
	cout << referenceToFruit.GetColor() << endl;

	getchar();
}
#endif // 0


#pragma endregion

#pragma region 另一些暂时需要藏起来的东西
#if 0
class Fruit
{
public:
	virtual string GetColor()
	{
		return "no color";
	}
};

class Banana :public Fruit
{
	string GetColor()override
	{
		return "Yellow";
	}
};
class Apple :public Fruit
{
	string GetColor()override
	{
		return "Red";
	}
};

class Person
{
public:
	Person(string loveColor) :LoveColor(loveColor) {};
	string LoveColor;
};

bool isSomeoneLoveTheFurit(Person& someone, Fruit& theFurit)
{
	if (someone.LoveColor == theFurit.GetColor())
		return true;
	else
		return false;
}

#endif // demo
#pragma endregion

class A
{
	//virtual void someFunction(){}
};

//int main()
//{
//	queue<int> myQueue;
//	
//
//	cout << sizeof(A)<< endl;
//	getchar();
//}
