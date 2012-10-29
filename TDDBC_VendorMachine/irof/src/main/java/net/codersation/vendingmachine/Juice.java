package net.codersation.vendingmachine;


public class Juice {
	public static Juice Coke = JuiceFactory.create("コーラ");
	public static Juice Water = JuiceFactory.create("水");
	public static Juice RedBull = JuiceFactory.create("レッドブル");

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
