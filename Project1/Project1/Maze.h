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
		//调用者应保证maze指向一个长度至少为Height*Width的bool型数组。
	}

	std::list<index> FindPath(index startIndex, index endIndex);
	Wall operator[](index i);	//Pass
//private:
	inline static index getNeighborIndex(const index& i, Orientation ori)
	{
		//应当注意的是，GetNeighborIndex不做关于索引是否有效的检查，也因此，它是一个静态方法。
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
			throw std::invalid_argument("无效的方向参数");
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