package net.codersation.vendingmachine;


public class Juice {
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
}
