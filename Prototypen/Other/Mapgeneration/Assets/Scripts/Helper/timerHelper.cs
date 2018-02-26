public class timerHelper
{
	#region private member
	private float _maximumTime;
	private float _currentTime;
	#endregion

	#region public void update(float deltaTime)
	public void update(float deltaTime)
	{
		_currentTime -= deltaTime;
	}
	#endregion

	#region public void reset()
	public void reset()
	{
		_currentTime = _maximumTime;
	}
	#endregion

	#region public float maxTime
	public float maxTime
	{
		set
		{
			_maximumTime = value;
		}
	}
	#endregion

	#region public float currentTime
	public float currentTime
	{
		get
		{
			return _currentTime;
		}
	}
	#endregion
}
