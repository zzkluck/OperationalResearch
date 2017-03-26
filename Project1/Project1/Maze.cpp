#include "Maze.h"
#include<stack>
#include<set>
#include<list>
using namespace std;
typedef pair<index, index> edge;

list<index> Maze::FindPath(index startIndex, index endIndex)
{
	if (!(checkIndexIn(startIndex) && checkIndexIn(endIndex)))
		throw invalid_argument("��������Ч����ʼ����ֹ������");
	if (((*this)[startIndex] && (*this)[endIndex]))
		throw invalid_argument("�������޷��������ʼ����ֹ������");

	set<index> whereHaveChecked;
	//ʹ��first����¼second��parent
	stack<edge> whereCanMove;
	stack<edge> possiblePath;

	whereCanMove.push(edge(ErrorIndex,startIndex));
	whereHaveChecked.insert(startIndex);
	while (!whereCanMove.empty())
	{
		edge temp = whereCanMove.top();
		index nowPosition = temp.second;
		possiblePath.push(temp);
		whereCanMove.pop();
		for (int i = Orientation::up; i < Orientation::left; i++)
		{
			index neighbor = checkCanMove(nowPosition, (Orientation)i);
			if (neighbor != ErrorIndex&&whereHaveChecked.find(neighbor) == whereHaveChecked.end()) 
			{
				whereCanMove.push(edge(nowPosition, neighbor));
				whereHaveChecked.insert(neighbor);
			}
			if (neighbor == endIndex)
			{
				possiblePath.push(edge(nowPosition, neighbor));
				list<index> result;
				result.push_front(endIndex);
				while (!possiblePath.empty())
				{
					edge temp = possiblePath.top();
					possiblePath.pop();
					if (temp.second == *(result.begin()))
					{
						result.push_front(temp.first);
					}
				}
				result.remove(ErrorIndex);
				return result;
			}
		}
	}
	return list<index>();
}

Wall Maze::operator[](index i)
{
	if (!checkIndexIn(i))
		throw invalid_argument("��Ч������");
	return *(_maze + i.first*_width + i.second);
}
