using UnityEngine;
using System.Collections.Generic;

public class randomHelper
{
	private List<int> _valuesList = null;
	private List<int> _sortedoutList = null;

	public void clear()
	{
		if(_valuesList == null || _sortedoutList == null)
		{
			return;
		}
		
		_valuesList.Clear ();
		_sortedoutList.Clear ();
		_valuesList = null;
		_sortedoutList = null;
	}

	public void setupValues(List<int> values)
	{
		clear ();
		
		_valuesList = values;
		_sortedoutList = new List<int> (_valuesList.Count);
	}

	public int getRandom()
	{	
		if(_valuesList.Count >= 1)
		{
			return _valuesList[Random.Range(0,_valuesList.Count)];
		}
		
		return -1;
	}

	public bool isEmpty()
	{
		if(_valuesList.Count < 1)
		{
			return true;
		}
		
		return false;
	}

	public void sortoutRandomValue(int value)
	{
		if(_valuesList.Contains(value))
		{
			for(int index = 0; index < _valuesList.Count; index++)
			{
				if(_valuesList[index] == value)
				{
					_sortedoutList.Add(_valuesList[index]);
					_valuesList.RemoveAt(index);
					break;
				}
			}
		}
	}

	public void remergeRandomValues()
	{
		foreach(int value in _sortedoutList)
		{
			_valuesList.Add (value);
		}
		
		_sortedoutList.Clear ();
	}
}
