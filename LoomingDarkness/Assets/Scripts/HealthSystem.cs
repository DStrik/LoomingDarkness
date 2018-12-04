using System;
public class HealthSystem {

	public event EventHandler OnHealthChange;
	private int health;
	private int healthMax;

	public HealthSystem(int healthMax) {
		this.healthMax = healthMax;
		health = healthMax;
	}

	public int getHealth() {
		return health;
	}

	public float getHealthPercentage() {
		return (float) health / healthMax;
	}

	public void damage(int damageAmount) {
		health -= damageAmount;

		if (health < 0) {
			health = 0;
		}

		if (OnHealthChange != null) {
			OnHealthChange(this, EventArgs.Empty);
		}
	}

	public void heal(int healAmount) {
		health += healAmount;

		if (health > healthMax) {
			health = healthMax;
		}

		if (OnHealthChange != null) {
			OnHealthChange(this, EventArgs.Empty);
		}
	}
}