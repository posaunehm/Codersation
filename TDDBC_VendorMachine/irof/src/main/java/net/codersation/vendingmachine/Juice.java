package net.codersation.vendingmachine;


public class Juice {
	public static Juice Coke = new Juice("コーラ", 120);
	public static Juice Water = new Juice("水", 100);
	public static Juice RedBull = new Juice("レッドブル", 200);

	private final String juiceName;
	private final int price;

	Juice(String name, int price) {
		this.juiceName = name;
		this.price = price;
	}

	public String getName() {
		return juiceName;
	}

	public int getPrice() {
		return price;
	}

	public boolean isEnough(int amount) {
		return amount >= this.getPrice();
	}

	public static Juice[] values() {
		return new Juice[] { Coke, Water, RedBull };
	}
}
