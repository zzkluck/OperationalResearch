#pragma once
#include<iostream>
#include<list>
#include<stdexcept>
#define ErrorIndex index(-1,-1)
typedef std::pair<int, int> index;
typedef bool Wall;

enum Orientation
{
	up,
	right,
	down,
	left
};

class Maze
{
public:
	Maze(Wall* maze, int height, int width) :_maze(maze), _height(height), _width(width)
	{
		//������Ӧ��֤mazeָ��һ����������ΪHeight*Width��bool�����顣
	}

	std::list<index> FindPath(index startIndex, index endIndex);
	Wall operator[](index i);	//Pass
//private:
	inline static index getNeighborIndex(const index& i, Orientation ori)
	{
		//Ӧ��ע����ǣ�GetNeighborIndex�������������Ƿ���Ч�ļ�飬Ҳ��ˣ�����һ����̬������
		switch (ori)
		{
		case Orientation::up:
			return index(i.first + 1, i.second);
		case Orientation::right:
			return index(i.first, i.second + 1);
		case Orientation::down:
			return index(i.first - 1, i.second);
		case Orientation::left:
			return index(i.first, i.second - 1);
		default:
			throw std::invalid_argument("��Ч�ķ������");
		}
	}

	inline index checkCanMove(const index& i, Orientation ori)
	{
		index neighbor = getNeighborIndex(i, ori);
		if (checkIndexIn(neighbor) && (*this)[neighbor] == 0)
			return neighbor;
		else
			return ErrorIndex;
	}
	inline bool checkIndexIn(const index& i)	//Pass
	{
		return i.first < _height&&i.second < _width;
	}


	bool* _maze;
	int _height;
	int _width;

	bool* nowPosition;

};

static std::ostream& operator<<(std::ostream& os, const index& i) {
	return os << "( " << i.first << " , " << i.second << " )";
}