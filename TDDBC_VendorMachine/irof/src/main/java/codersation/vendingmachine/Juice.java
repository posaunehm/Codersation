package codersation.vendingmachine;

public enum Juice {
	Coke("コーラ", 120, 5), Water("水", 100, 5), RedBull("レッドブル", 200, 5);

	private final String juiceName;
	private final int price;
	private final int initialStock;

	Juice(String name, int price, int initialStock) {
		this.juiceName = name;
		this.price = price;
		this.initialStock = initialStock;
	}

	public String getName() {
		return juiceName;
	}

	public int getPrice() {
		return price;
	}

	public int getInitialStock() {
		return initialStock;
	}
}
