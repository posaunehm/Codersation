package codersation.vendingmachine;

public class JuiceStock {

	int count;
	private final Juice juice;

	public JuiceStock(Juice juice, int count) {
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

	boolean canPurchase(int amount) {
		if (getCount() <= 0) {
			return false;
		}
		return amount >= getJuice().getPrice();
	}
}
