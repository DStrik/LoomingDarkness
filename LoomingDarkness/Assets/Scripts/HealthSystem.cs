using System;
public class HealthSystem {

	public event EventHandler OnHealthChange;
	private float health;
	private float healthMax;

	public HealthSystem(float healthMax) {
		this.healthMax = healthMax;
		health = healthMax;
	}

	public float getHealth() {
		return health;
	}

	public float getHealthPercentage() {
		return health / healthMax;
	}

	public void damage(float damageAmount) {
		health -= damageAmount;

		if (health < 0) {
			health = 0;
		}

		if (OnHealthChange != null) {
			OnHealthChange(this, EventArgs.Empty);
		}
	}

	public void heal(float healAmount) {
		health += healAmount;

		if (health > healthMax) {
			health = healthMax;
		}

		if (OnHealthChange != null) {
			OnHealthChange(this, EventArgs.Empty);
		}
	}
}