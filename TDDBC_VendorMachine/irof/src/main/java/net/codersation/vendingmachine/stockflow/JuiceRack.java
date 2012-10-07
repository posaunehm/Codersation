package net.codersation.vendingmachine.stockflow;

import net.codersation.vendingmachine.Juice;

public class JuiceRack {

	private int count;
	private final Juice juice;

	public JuiceRack(Juice juice, int count) {
		this.juice = juice;
		this.count = count;
	}

	public Juice getJuice() {
		return juice;
	}

	public int getCount() {
		return count;
	}

	public void remove() {
		count--;
	}

	public boolean canPurchase(int amount) {
		if (getCount() <= 0) {
			return false;
		}
		return amount >= getJuice().getPrice();
	}
}
