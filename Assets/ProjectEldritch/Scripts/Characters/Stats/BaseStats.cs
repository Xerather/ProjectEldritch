public class BaseStats
{
	public float maxHp;
	public float hp;
	public float atk;
	public float speed;

	public BaseStats(BaseStats stats)
	{
		maxHp = stats.maxHp;
		hp = stats.hp;
		atk = stats.atk;
		speed = stats.speed;
	}
}
