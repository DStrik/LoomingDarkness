using System;

public class LightSystem {

	public event EventHandler OnLightChange;
	private float light;
	private float maxlight;
	// Use this for initialization

	public LightSystem(float maxlight){
		this.maxlight = maxlight;
		light = this.maxlight;
	}

	public float getLight() {
		return light;
	}

	public void updateLight(float status) {
		
		this.light = status;
	}

	public void deplete(float deplete) {
		light -= deplete;

		if(light < 0) {
			light = 0;
		}

		if(OnLightChange != null) {
			OnLightChange(this, EventArgs.Empty);
		}
	}
}
