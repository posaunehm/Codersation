package com.github.springaki.codersation;


public class VendingMachine {

//	private List<Money> amount = new ArrayList<Money>();
	private int totalAmount = 0;
	
	public void insertMoney(Money money) {
//		amount.add(money);
		this.totalAmount += money.toInt();
	}

	public int getTotalAmount() {
		return this.totalAmount;
	}

	public int calcel() {
		int cancelAmount = this.totalAmount;
		this.totalAmount = 0;
		return cancelAmount;
	}
}
