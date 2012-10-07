package net.codersation.vendingmachine.moneyflow;

public class MoneyFlow {

	private int saleAmount = 0;

	public int getSaleAmount() {
		return saleAmount;
	}

	public void addSale(int price) {
		saleAmount += price;
	}

}
