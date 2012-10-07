package net.codersation.vendingmachine.stockflow;

import net.codersation.vendingmachine.Juice;

public class JuiceRack {

	private int count;
	private final Juice juice;

	public JuiceRack(Juice juice, int count) {
		this.juice = juice;
		this.count = count;
	}

	/**
	 * @deprecated テストのために残してるだけです
	 */
	@Deprecated
	public Juice getJuice() {
		return juice;
	}

	public int getCount() {
		return count;
	}

	public void remove() {
		count--;
	}

	public boolean isInStock() {
		return getCount() > 0;
	}

	boolean of(Juice juice) {
		return getJuice().equals(juice);
	}
}
