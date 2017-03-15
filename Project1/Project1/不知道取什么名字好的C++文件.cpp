#include<iostream>
#include<exception>

using namespace std;

class EuclideanAlgorithm {
public:
	static int Start(int n1, int n2) {
		Euclidean_Init(n1, n2);
		return Euclidean_Run(n1, n2);
	}

private:
	static inline void Euclidean_Init(int& n1, int& n2) {
		if (n1 <= 0 || n2 <= 0)
			throw invalid_argument("n1, n2中包含负数或零");
		if (n2 > n1) swap(n1, n2);
	}

	static int Euclidean_Run(int large, int small) {
		return large % small == 0 ? small : Euclidean_Run(small, large%small);
	}
};

int main()
{
	try
	{
		cout << EuclideanAlgorithm::Start(1242, 2511) << endl;
		cout << EuclideanAlgorithm::Start(23, 101) << endl;
		cout << EuclideanAlgorithm::Start(14, 49) << endl;
		cout << EuclideanAlgorithm::Start(49, 14) << endl;
		cout << EuclideanAlgorithm::Start(-1242, 2511) << endl;
	}
	catch (const std::exception& e)
	{
		cout << e.what() << endl;
	}
	getchar();
}
